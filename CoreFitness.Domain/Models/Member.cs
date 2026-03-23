using System;

namespace CoreFitness.Domain.Models;

public sealed class Member
{
    // private constructor
    private Member(string id, string userId, string firstName, string lastName, string? phoneNumber, string? profileImageUri, DateTime createdAt, DateTime? updatedAt)
    {
        Id = Required(id, nameof(id));
        UserId = Required(userId, nameof(userId));
        FirstName = Required(firstName, nameof(firstName));
        LastName = Required(lastName, nameof(lastName));
        PhoneNumber = phoneNumber;
        ProfileImageUri = profileImageUri;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }




     public string Id { get; private set; } = null!;
     public string UserId { get; private set;  } = null!;
     public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? ProfileImageUri { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

   
    //validation methods
    
    public void UpdateInformation(string firstName, string lastName, string? phoneNumber, string? profileImageUri)
    {
        FirstName = Required(firstName, nameof(firstName));
        LastName = Required(lastName, nameof(lastName));
        PhoneNumber = phoneNumber;
        ProfileImageUri = profileImageUri;
        UpdatedAt = DateTime.UtcNow;
    }


    private static string Required(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} must be provided ");

        return value.Trim();
    }


    // create and rehydrate methods
    public static Member Create(string userId, string firstName, string lastName, string? phoneNumber = null, string? profileImageUri = null)
    {
        return new Member(
            Guid.NewGuid().ToString(),
            userId,
            firstName,
            lastName,
            phoneNumber,
            profileImageUri,
            DateTime.UtcNow,
            null);
    }


    public static Member Rehydrate(string id, string userId, string firstName, string lastName, string? phoneNumber, string? profileImageUri, DateTime createdAt, DateTime? updatedAt)
    {
        return new Member(id, userId, firstName, lastName, phoneNumber, profileImageUri, createdAt, updatedAt);
    }
}