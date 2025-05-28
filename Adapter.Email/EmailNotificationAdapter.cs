using System.Net.Mail;
using System.Net;
using Adapter.Email.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Web;
using Adapter.Email.Model.Enums;
using Adapter.Email.Common;

namespace Adapter.Email
{
    public class EmailNotificationAdapter : IEmailNotificationAdapter
    {
        private readonly IConfiguration _configuration;
        private const string displayName = "PetShoes";
        public EmailNotificationAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendPurchaseOrderCreatedMail(string name, string email)
        {
            var parameters = new Dictionary<string, string>();
            var subject = $"PetShoes - {name} parabéns pela compra!";

            parameters.Add("{CustomerName}", name);
            parameters.Add("{Email}", email);
          
            var emailBody = GenerateEmail(EmailType.PurchaseOrderCreated, parameters);
            SendEmail(subject, email, emailBody);
        }
        public void SendEmail(string subject, string emailTo, string message)
        {
            var from = _configuration.GetSection("EmailConfig:From").Value;
            var smtp = _configuration.GetSection("EmailConfig:Smtp").Value;
            var port = int.Parse(_configuration.GetSection("EmailConfig:Port").Value!);
            var user = _configuration.GetSection("EmailConfig:User").Value;
            var pass = _configuration.GetSection("EmailConfig:Pass").Value;

            var mail = new MailMessage
            {
                From = new MailAddress(from!, displayName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };

            mail.To.Add(emailTo);

            using SmtpClient smtpClient = new SmtpClient(smtp!, port);
            smtpClient.Credentials = new NetworkCredential(user!, pass!);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mail);
        }

        #region Métodos privados
        private string GenerateEmail(EmailType type, Dictionary<string, string> parameters)
        {
            string[] extensions = { ".txt" };
            string pathTemplate = $"{AppDomain.CurrentDomain.BaseDirectory}//Template";
            string contentFile = string.Empty;

            var files = Directory.EnumerateFiles(pathTemplate, "*", SearchOption.AllDirectories)
                                 .Where(extensionFile => extensions.Any(ext => ext == Path.GetExtension(extensionFile)));
            var file = files
                        .Where(item => item.Contains(type.ToString()))
                        .FirstOrDefault();

            if (file != null)
            {
                contentFile = File.ReadAllText(file);

                foreach (var item in parameters)
                {
                    contentFile = contentFile.Replace(item.Key, item.Value);
                }
            }

            return contentFile;
        }
        private string CryptographyInfoUser(string email, Guid id)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Security:Key").Value!);
            var vector = Encoding.ASCII.GetBytes(_configuration.GetSection("Security:Base").Value!);
            var criptInfo = Criptography.Encrypt($"{email}:{id}", key, vector);

            var encode = Convert.ToBase64String(criptInfo);

            return HttpUtility.UrlEncode(encode);
        }
        #endregion

    }
}
