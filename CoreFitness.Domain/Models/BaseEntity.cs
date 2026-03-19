
namespace CoreFitness.Domain.Models;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Modified { get; set; }
}
