using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Tests.Members;

// got help from AI to write and explain how to run tests on methods in the domain models 
public class Member_Tests
{
    [Fact] 
    // test to check that the method Member.Create creates a new member with user ID from identiy user
    public void Create_ShouldInitializeMember_WithCorrectValues()
    {
        // ARRANGE
        var userId = "user-123";

        // ACT
        var member = Member.Create(userId);

        // ASSERT
        Assert.NotNull(member.Id);
        Assert.False(string.IsNullOrWhiteSpace(member.Id));
        Assert.Equal(userId, member.UserId);
        Assert.True(member.CreatedAt <= DateTime.UtcNow);
        Assert.Null(member.UpdatedAt);
    }

    [Fact]
    //test to check that initial membership is set (if provided) when user is created
    public void Create_ShouldSetInitialMembership_WhenProvided()
    {
        // ARRANGE
        var userId = "user-123";
        var membershipId = "test-plan";

        // ACT
        var member = Member.Create(userId, membershipId);

        // ASSERT
        Assert.Equal(membershipId, member.CurrentMembershipId);
    }

    [Fact]
    // test to check method member.UpdateInformation, should update values and set the updatedAt time
    public void UpdateInformation_ShouldUpdateAllFieldsAndSetUpdatedAt()
    {
        // ARRANGE
        var member = Member.Create("user-123");
        var firstName = "Jeanette";
        var lastName = "Samuelsson";
        var phone = "070707070";
        var uri = "/images/profile.png";

        // ACT
        member.UpdateInformation(firstName, lastName, phone, uri);

        // ASSERT
        Assert.Equal(firstName, member.FirstName);
        Assert.Equal(lastName, member.LastName);
        Assert.Equal(phone, member.PhoneNumber);
        Assert.Equal(uri, member.ProfileImageUri);
        Assert.NotNull(member.UpdatedAt);
        Assert.True(member.UpdatedAt <= DateTime.UtcNow);
    }

    [Theory]
 
    // Theory -> test multiple unvalid/empty inputs , will run the test 3 times with different inlineData
    [InlineData("", "LastName")] 
    [InlineData("FirstName", "")] 
    [InlineData("   ", "LastName")] 
    public void UpdateInformation_ShouldThrowArgumentException_WhenRequiredFieldsAreMissing(string fName, string lName)
    {
        // ARRANGE
        var member = Member.Create("user-123");

        // ACT & ASSERT
        var exception = Assert.Throws<ArgumentException>(() =>
            member.UpdateInformation(fName, lName, "123", null));

        Assert.Contains("must be provided", exception.Message);
    }

    [Fact]
    public void SetMembership_ShouldUpdateMembershipIdAndTimestamp()
    {
        // ARRANGE
        var member = Member.Create("user-123");
        var newMembershipId = "membership-123";

        // ACT
        member.SetMembership(newMembershipId);

        // ASSERT
        Assert.Equal(newMembershipId, member.CurrentMembershipId);
        Assert.NotNull(member.UpdatedAt);
    }

    [Fact]
    // test that Rehydrate method only returns the member from database without changing timnestamps och ID:s
    public void Rehydrate_ShouldRestoreState_WithoutChangingTimestampsOrIds()
    {
        // ARRANGE
        var id = "existing-id";
        var userId = "user-id";
        var createdAt = DateTime.UtcNow.AddDays(-10);
        var firstName = "Nellie";

        // ACT
        var member = Member.Rehydrate(
            id,
            userId,
            firstName,
            "Samuelsson",
            "555",
            null,
            createdAt,
            null,
            null,
            "mem-1");

        // ASSERT
        Assert.Equal(id, member.Id);
        Assert.Equal(userId, member.UserId);
        Assert.Equal(createdAt, member.CreatedAt);
        Assert.Equal(firstName, member.FirstName);
    }
}