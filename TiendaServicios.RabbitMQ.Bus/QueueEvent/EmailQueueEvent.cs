using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.Events;

namespace TiendaServicios.RabbitMQ.Bus.QueueEvent
{
    public class EmailQueueEvent(string receiver, string title, string content) : Event
    {
        public string Receiver { get; set; }= receiver;
        public string Title { get; set; }= title;
        public string  Content { get; set; }= content;

    }
}
