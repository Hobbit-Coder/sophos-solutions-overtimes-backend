using SophosSolutions.Overtimes.Models.Common;
using SophosSolutions.Overtimes.Models.Exceptions;

namespace SophosSolutions.Overtimes.Models.ValueObjects;

public class RoleName : ValueObject
{
    static RoleName()
    {
    }

    private RoleName()
    {
    }

    private RoleName(string name)
    {
        Name = name;
    }

    public static RoleName From(string name)
    {
        var roleName = new RoleName { Name = name };

        if (!SupportedRoles.Contains(roleName))
        {
            throw new UnsupportedRoleException(name);
        }

        return roleName;
    }

    public static RoleName AreaManager => new RoleName("AreaManager");
    public static RoleName Collaborator => new RoleName("Collaborator");
    public static RoleName GeneralManager => new RoleName("GeneralManager");
    public static RoleName HumanTalentManager => new RoleName("HumanTalentManager");

    public string Name { get; private set; } = "Collaborator";

    public static implicit operator string(RoleName roleName)
    {
        return roleName.ToString();
    }

    public static explicit operator RoleName(string name)
    {
        return From(name);
    }

    public override string ToString()
    {
        return Name;
    }

    protected static IEnumerable<RoleName> SupportedRoles
    {
        get
        {
            yield return AreaManager;
            yield return Collaborator;
            yield return GeneralManager;
            yield return HumanTalentManager;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

}
