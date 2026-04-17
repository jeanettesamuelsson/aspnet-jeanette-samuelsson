namespace CoreFitness.Infrastructure.Persistence.Entities;

public class GymClassEntity
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Instructor { get; set; }
    public string? Category { get; set; }
    public DateTime ScheduledTime { get; set; }
    public int Capacity { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}