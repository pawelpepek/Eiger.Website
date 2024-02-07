using System.Net.Mail;
using System.Text;
using MailSender.Models;

namespace MailSender.Services;

public class SenderService
{
    private readonly IConfiguration _configuration;
    public SenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendToMe(SenderDto sender) => Send(sender, MailSettings.Email);

    private void Send(SenderDto sender, string mailTo)
    {
        var mailSettings = MailSettings;

        var msg = new MailMessage();
        var client = new SmtpClient();

        var body = new StringBuilder("<HTML><BODY>Napisał do Ciebie: ");
        body.Append(sender.Name);
        body.Append(" <br /><br />Email: ");
        body.Append(sender.Email);

        if (!string.IsNullOrEmpty(sender.Phone))
        {
            body.Append(" <br /><br />Telefon: ");
            body.Append(sender.Phone);
        }

        body.Append(" <br /><br />Wiadomość: <br />");
        body.Append(sender.Message);
        body.Append("</BODY></HTML>");

        msg.Subject = "Nowa wiadomość od " + sender.Name;
        msg.Body = body.ToString();
        msg.From = new MailAddress(mailSettings.Sender);
        msg.To.Add(mailTo);
        msg.IsBodyHtml = true;
        client.Timeout = 10000;
        client.Host = mailSettings.Host;

        var basicauthenticationinfo = new System.Net.NetworkCredential(mailSettings.Sender, mailSettings.Password);
        client.Port = int.Parse(mailSettings.Port);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicauthenticationinfo;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Send(msg);
    }

    private MailSettings MailSettings => _configuration.GetSection("ConnectionStrings").Get<MailSettings>();
}
