namespace Domain.Common;

public class BaseEntity
{
    public int Version { get; set; }
    public bool? IsActive { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}