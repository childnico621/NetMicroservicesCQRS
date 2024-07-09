using TiendaServicios.Messenger.Email.SendGrid.Interface;
using TiendaServicios.Messenger.Email.SendGrid.Model;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.QueueEvent;

namespace TiendaServicios.Api.Autor.RabbitHandler
{
    public class EmailEventHandler : IEventHandler<EmailQueueEvent>
    {
        private readonly ILogger<EmailEventHandler> _logger;
        private readonly ISendGridDispatch _emailDispatch;
        private readonly IConfiguration _configuration;
        public EmailEventHandler(ILogger<EmailEventHandler> logger, ISendGridDispatch emailDispatch, IConfiguration configuration)
        {
            _logger = logger;
            _emailDispatch = emailDispatch;
            _configuration = configuration;
        }

        public async Task Handle(EmailQueueEvent @event)
        {
            var data = new SendGridData()
            {
                SendGridAPIKey = _configuration["sendgrid-api-key"] ?? "",
                RecipientEmail = @event.Receiver,
                RecipientName = @event.Receiver,
                Title = @event.Title,
                Content = @event.Content
            };
           var result = await _emailDispatch.SendEmailAsync(data);
            if (result.result) 
                await Task.CompletedTask;

            
        }
    }
}
