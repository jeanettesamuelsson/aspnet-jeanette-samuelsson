using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Models;

public sealed class GymClass
{
    private GymClass(string id, string name, DateTime scheduledTime)
    {
        Id = Required(id, nameof(id));
        Name = Required(name, nameof(name));
        ScheduledTime = scheduledTime;
    }

    public string Id { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string? Instructor { get; private set; }
    public string? Category { get; private set; }
    public DateTime ScheduledTime { get; private set; }
    public int Capacity { get; private set; }

    // admin function to create new classes, and rehydrate existing ones from the database
    public static GymClass Create(string name, string? instructor, string? category, DateTime scheduledTime, int Capacity)
    {
        return new GymClass(Guid.NewGuid().ToString(), name, scheduledTime)
        {
            Instructor = instructor,
            Category = category,
            Capacity = Capacity
        };
    }

    public static GymClass Rehydrate(string id, string name, string? instructor, string? category, DateTime scheduledTime, int Capacity)
    {
        return new GymClass(id, name, scheduledTime)
        {
            Instructor = instructor,
            Category = category,
            Capacity = Capacity
        };
    }

    private static string Required(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} must be provided");
        return value.Trim();
    }
}