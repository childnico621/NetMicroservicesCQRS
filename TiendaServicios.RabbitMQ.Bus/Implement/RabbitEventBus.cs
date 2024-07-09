using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Commands;
using TiendaServicios.RabbitMQ.Bus.Events;

namespace TiendaServicios.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitEventBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "nagudelo", Password = "Nick.621" };
            using (var conn = factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                var eventName = @event.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);
            }
        }

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {

            var eventName = typeof(T).Name;
            var handlerEventType = typeof(TH);
            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Contains(handlerEventType))
            {
                throw new ArgumentException($"the handler {handlerEventType.Name} was registered previously by {eventName}.");
            }

            _handlers[eventName].Add(handlerEventType);

            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "nagudelo", Password = "Nick.621", DispatchConsumersAsync=true };
            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            channel.QueueDeclare(eventName, false, false, false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Delegate;

            channel.BasicConsume(eventName, true, consumer);

        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs evt)
        {
            var eventName = evt.RoutingKey;
            var message = Encoding.UTF8.GetString(evt.Body.ToArray());

            try
            {
                if (_handlers.ContainsKey(eventName))
                {
                    using (var scope =_serviceScopeFactory.CreateScope())
                    {
                        var subscriptions = _handlers[eventName];
                        foreach (var sb in subscriptions)
                        {
                            var handler =scope.ServiceProvider.GetService(sb); //Activator.CreateInstance(sb);
                            if (handler == null) continue;

                            var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                            var eventDS = JsonConvert.DeserializeObject(message, eventType);
                            var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                            await (concreteType.GetMethod("Handle").Invoke(handler, [eventDS]) as Task);

                        } 
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
