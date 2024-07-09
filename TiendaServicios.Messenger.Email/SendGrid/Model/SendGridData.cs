namespace TiendaServicios.Messenger.Email.SendGrid.Model
{
    public class SendGridData
    {
        public string SendGridAPIKey { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public string Title { get; set; }= string.Empty;
        public string Content { get; set; } = string.Empty;


    }
}
