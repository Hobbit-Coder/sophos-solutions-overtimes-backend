namespace SophosSolutions.Overtimes.Models.Exceptions;

public class UnsupportedRoleException : Exception
{
    public UnsupportedRoleException(string name)
        : base($"Role {name} is unsupported.")
    {

    }
}
