using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.QueueEvent;

namespace TiendaServicios.Api.Autor.RabbitHandler
{
    public class EmailEventHandler : IEventHandler<EmailQueueEvent>
    {
        private readonly ILogger<EmailEventHandler> _logger;

        public EmailEventHandler(ILogger<EmailEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(EmailQueueEvent @event)
        {
            _logger.LogInformation("Message read from rabitMQ {Title}", @event.Title);
            return Task.CompletedTask;
        }
    }
}
