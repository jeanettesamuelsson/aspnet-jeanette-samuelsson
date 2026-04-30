using CoreFitness.Infrastrcuture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Tests.Members;


public class Membership_Tests
{
    [Fact]
    public void Create_ShouldInitializeMembership_WithCorrectValuesAndGuid()
    {
        // ARRANGE
        var title = "Premium";
        var description = "Premium membership with blablabla";
        var benefits = new List<string> { "Pool", "Gym" };
        var price = 500m;

        // ACT
        var membership = Membership.Create(title, description, benefits, price);

        // ASSERT
        Assert.NotNull(membership.Id);
        Assert.Equal(title, membership.Title);
        Assert.Equal(price, membership.Price);
        Assert.Equal(20, membership.MonthlyClasses); // check default value from method i Membership
    }

    [Fact]
    public void Create_ShouldThrowException_WhenPriceIsNegative()
    {
        // ARRANGE
        var price = -100m;

        // ACT & ASSERT
        var exception = Assert.Throws<ArgumentException>(() =>
            Membership.Create("Title", "Desc", new List<string>(), price));

        Assert.Contains("can not be a negative value", exception.Message);
    }


    [Fact]
    // test that Rehydrate method only returns the membership from database without changing timnestamps och ID:s
    public void Rehydrate_ShouldRestoreState_WithoutChangingIdOrValues()
    {
        // ARRANGE
        var existingId = "membership-123";
        var title = "Premium";
        var description = "Free access to everything";
        var benefits = new List<string> { "Gym", "Pool", "Yoga" };
        var price = 499m;
        var monthlyClasses = 30;

        // ACT
        var membership = Membership.Rehydrate(
            existingId,
            title,
            description,
            benefits,
            price,
            monthlyClasses);

        // ASSERT
        Assert.Equal(existingId, membership.Id);
        Assert.Equal(title, membership.Title);
        Assert.Equal(description, membership.Description);
        Assert.Equal(benefits, membership.Benefits);
        Assert.Equal(price, membership.Price);
        Assert.Equal(monthlyClasses, membership.MonthlyClasses);
    }
}
