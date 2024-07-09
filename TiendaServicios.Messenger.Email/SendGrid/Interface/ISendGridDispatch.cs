using TiendaServicios.Messenger.Email.SendGrid.Model;

namespace TiendaServicios.Messenger.Email.SendGrid.Interface
{
    public interface ISendGridDispatch
    {
        Task<(bool result, string errorMessage)> SendEmailAsync(SendGridData data);
    }
}
