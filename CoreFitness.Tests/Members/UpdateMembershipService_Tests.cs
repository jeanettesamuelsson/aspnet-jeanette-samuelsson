using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Members.Services;
using CoreFitness.Application.Memberships;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastrcuture.Models;
using CoreFitness.Infrastructure.Identity;
using CoreFitness.Infrastructure.Persistence.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Tests.Members;

public class UpdateMembershipService_Tests
{
    private readonly IMemberRepository _memberRepo;
    private readonly UpdateMembershipService _sut;

    public UpdateMembershipService_Tests()
    {
        // create mocks with NSubstitute
        _memberRepo = Substitute.For<IMemberRepository>();

        // inject in service/system under test
        _sut = new UpdateMembershipService(_memberRepo);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnOk_WhenUpdateIsSuccessful()
    {
        // ARRANGE
        var userId = "user-123";
        var membershipId = "new-membership-id";

        // create test member
        var member = Member.Create(userId);

        _memberRepo.GetMemberByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(member);

        _memberRepo.UpdateAsync(member, Arg.Any<CancellationToken>())
            .Returns(true);

        // ACT
        var result = await _sut.ExecuteAsync(userId, membershipId);

        // ASSERT
        Assert.True(result.Success);
        await _memberRepo.Received(1).UpdateAsync(Arg.Is<Member>(m => m.UserId == userId), Arg.Any<CancellationToken>());
    }

   
}
