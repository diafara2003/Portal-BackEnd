using System.Collections.Generic;
using System.Net.Mail;

namespace Code.Repository.Email.Interface
{
    public interface IEmailSender
    {
        string SendEmail(List<string> email, string subject, AlternateView body);
    }
}
