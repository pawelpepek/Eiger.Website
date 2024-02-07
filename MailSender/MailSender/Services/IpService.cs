using MailSender.Exceptions;
using MailSender.Models;
using System.Net;

namespace MailSender.Services;

public class IpService
{
    private List<IpConnection> _connections = new();

    private readonly object _lockedObject = new();
    public bool IsConnectionValid(IPAddress ipAddress)
    {
        lock (_lockedObject)
        {
            ClearOlConnections();

            IsPartAddressOk((IpConnection c) => c.IpAddress.GetAddressBytes()
                                                           .SequenceEqual(ipAddress.GetAddressBytes()), 5);

            IsPartAddressOk((IpConnection c) => true, 30);

            var ipConnection = new IpConnection()
            {
                IpAddress = ipAddress,
                Connected = DateTime.Now
            };

            _connections.Add(ipConnection);

            return true;
        }
    }

    private void ClearOlConnections() => _connections = _connections.Where(c => !c.IsOld).ToList();

    private bool IsPartAddressOk(Func<IpConnection, bool> condition, int quantityAllowed)
    {
        var lastConnectionsCount = _connections.Count(condition);

        if (lastConnectionsCount >= quantityAllowed)
        {
            throw new ToManyConnectionsException();
        }

        return true;
    }
}
