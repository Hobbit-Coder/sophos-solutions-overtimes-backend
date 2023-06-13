namespace SophosSolutions.Overtimes.Application.Common.Exceptions;

public class DomainEventException : Exception
{
    public DomainEventException(string message)
        : base(message)
    {
    }
}
