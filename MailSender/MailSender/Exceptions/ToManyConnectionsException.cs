namespace MailSender.Exceptions;

public class ToManyConnectionsException:Exception
{
    public ToManyConnectionsException()
        :base("Limit wiadomości został już wykorzystany!") { }
}
