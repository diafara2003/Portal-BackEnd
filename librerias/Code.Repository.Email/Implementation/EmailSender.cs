
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Code.Repository.Email.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettingsDTO _emailSettings;


        public EmailSender(EmailSettingsDTO emailSettings)
        {
            _emailSettings = emailSettings;
        }


        public string SendEmail(List<string> correos, string subject, AlternateView body)
        {

            //Creando objeto MailMessage
            MailMessage email = new MailMessage();


            correos.ForEach(c => email.To.Add(new MailAddress(c)));


            email.From = new MailAddress(_emailSettings.SenderName);
            email.Subject = subject;//"Registro de padre";
            //  email.Body = CreaeBody(ruta, usuario);
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
            email.AlternateViews.Add(body);
            //Definir objeto SmtpClient
            SmtpClient smtp = IniciarSmtpClient();
          
            string output = null;


            // Enviar correo electronico
            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Correo enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.InnerException;
            }

            return output;
        }


        SmtpClient IniciarSmtpClient()
        {
            ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            SmtpClient ClienteSMTP = new SmtpClient
            {
                Host = _emailSettings.MailServer,
                Port = _emailSettings.MailPort,
                EnableSsl = _emailSettings.MailSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password),
                Timeout = 100000

            };

            return ClienteSMTP;

        }


    }
}
