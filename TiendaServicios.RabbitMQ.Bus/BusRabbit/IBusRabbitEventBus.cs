using TiendaServicios.RabbitMQ.Bus.Commands;
using TiendaServicios.RabbitMQ.Bus.Events;

namespace TiendaServicios.RabbitMQ.Bus.BusRabbit
{
    internal interface IBusRabbitEventBus
    {
        Task Send<T>(T command)where T:Command;
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>(T @event) where T : Event where TH: IEventHandler<T>;
    }
}
