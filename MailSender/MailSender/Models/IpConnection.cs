using System.Net;

namespace MailSender.Models;

public class IpConnection
{
    public IPAddress IpAddress { get; set; }
    public DateTime Connected { get; set; }

    public bool IsOld => Connected.AddDays(1) < DateTime.Now;
}
