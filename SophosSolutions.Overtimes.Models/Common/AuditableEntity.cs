namespace SophosSolutions.Overtimes.Models.Common;

public abstract class AuditableEntity
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
