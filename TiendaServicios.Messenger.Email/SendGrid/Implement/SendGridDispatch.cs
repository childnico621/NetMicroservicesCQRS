using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Messenger.Email.SendGrid.Interface;
using TiendaServicios.Messenger.Email.SendGrid.Model;

namespace TiendaServicios.Messenger.Email.SendGrid.Implement
{
    public class SendGridDispatch : ISendGridDispatch
    {
        public SendGridDispatch()
        {

        }
        public async Task<(bool result, string errorMessage)> SendEmailAsync(SendGridData data)
        {
            try
            {
                var sendGridClient = new SendGridClient(data.SendGridAPIKey);
                var recipientEmail = new EmailAddress(data.RecipientEmail);
                var title = data.Title;
                var sender = new EmailAddress("childnico621@gmail.com");
                var message = data.Content;
                var mailObj = MailHelper.CreateSingleEmail(sender, recipientEmail, title, message, message);

                await sendGridClient.SendEmailAsync(mailObj);

                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }


        }
    }
}
