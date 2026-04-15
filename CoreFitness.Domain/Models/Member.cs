using CoreFitness.Infrastrcuture.Models;

namespace CoreFitness.Domain.Models;

public sealed class Member
{
    
    private Member(string id, string userId, DateTime createdAt)
    {
        Id = Required(id, nameof(id));
        UserId = Required(userId, nameof(userId));
        CreatedAt = createdAt;
     
    }




     public string Id { get; private set; } = null!;
     public string UserId { get; private set;  } = null!;
     public string? FirstName { get; private set; }
     public string? LastName { get; private set; }
     public string? PhoneNumber { get; private set; }
     public string? ProfileImageUri { get; private set; }
     public DateTime CreatedAt { get; private set; }
     public DateTime? UpdatedAt { get; private set; }
     public string? CurrentMembershipId { get; private set; }

    public Membership? CurrentMembership { get; private set; }





    private static string Required(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} must be provided ");

        return value.Trim();
    }

    public void UpdateInformation(string firstName, string lastName, string? phoneNumber, string? profileImageUri)
    {
        FirstName = Required(firstName, nameof(firstName));
        LastName = Required(lastName, nameof(lastName));
        PhoneNumber = phoneNumber;
        ProfileImageUri = profileImageUri;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetMembership(string membershipId)
    {
        CurrentMembershipId = Required(membershipId, nameof(membershipId));
        UpdatedAt = DateTime.UtcNow;
    }


    // create and rehydrate
    public static Member Create(string userId, string? initialMembershipId = null)
    {
        var member = new Member(
            Guid.NewGuid().ToString(),
            userId,
            DateTime.UtcNow
        );

        if (!string.IsNullOrEmpty(initialMembershipId))
        {
            member.SetMembership(initialMembershipId);
        }

        return member;
    }

    //rehydrate (existing from database) does not generate a new guid 
    public static Member Rehydrate(
        string id, 
        string userId, 
        string? firstName, 
        string? lastName, 
        string? phoneNumber, 
        string? profileImageUri, 
        DateTime createdAt, 
        DateTime? updatedAt,
        Membership? currentMembership,
        string? currentMembershipId)
    {
        var member = new Member(id, userId, createdAt)
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            ProfileImageUri = profileImageUri,
            UpdatedAt = updatedAt,
            CurrentMembershipId = currentMembershipId,
            CurrentMembership = currentMembership
        };

        return member;
    }
}