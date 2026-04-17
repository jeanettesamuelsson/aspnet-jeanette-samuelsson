using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Models;

public sealed class Booking
{
    private Booking(string id, string memberId, string gymClassId, DateTime bookedAt)
    {
        Id = Required(id, nameof(id));
        MemberId = Required(memberId, nameof(memberId));
        GymClassId = Required(gymClassId, nameof(gymClassId));
        BookedAt = bookedAt;
    }

    public string Id { get; private set; } = null!;
    public string MemberId { get; private set; } = null!;
    public string GymClassId { get; private set; } = null!;
    public DateTime BookedAt { get; private set; }

    // Navigation properties 
    public Class? GymClass { get; private set; }

    public static Booking Create(string memberId, string gymClassId)
    {
        return new Booking(
            Guid.NewGuid().ToString(),
            memberId,
            gymClassId,
            DateTime.UtcNow
        );
    }

    public static Booking Rehydrate(string id, string memberId, string gymClassId, DateTime bookedAt, Class? gymClass = null)
    {
        return new Booking(id, memberId, gymClassId, bookedAt)
        {
            GymClass = gymClass
        };
    }

    private static string Required(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} must be provided");
        return value.Trim();
    }
}