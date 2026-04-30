using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Application.Members.Services;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using NSubstitute;

namespace CoreFitness.Tests.Members;

public class DeleteMemberService_Tests
{
    private readonly IMemberRepository _memberRepo;
    private readonly IIdentityService _identityService;
    private readonly IWebHostEnvironment _env;
    private readonly DeleteMemberService _sut;

    public DeleteMemberService_Tests()
    {
        _memberRepo = Substitute.For<IMemberRepository>();
        _identityService = Substitute.For<IIdentityService>();
        _env = Substitute.For<IWebHostEnvironment>();

        // simulates a web root path for ( var fullFilePath = Path.Combine(env.WebRootPath ... )
        _env.WebRootPath.Returns("C:/mockroot");

        _sut = new DeleteMemberService(_memberRepo, _identityService, _env);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteFileAndMember_WhenMemberHasProfileImage()
    {
        // ARRANGE
        var userId = "user-123";
        var member = Member.Create(userId);
        member.UpdateInformation("Test", "Testsson", "123", "/uploads/image.png");

        _memberRepo.GetMemberByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(member);

        _identityService.DeleteUserAsync(userId, Arg.Any<CancellationToken>())
            .Returns(Result.Ok());

        // ACT
        var result = await _sut.ExecuteAsync(userId);

        // ASSERT
        Assert.True(result.Success);

        // check that repo and Remove method was called with the right member 
        await _memberRepo.Received(1).RemoveAsync(member, Arg.Any<CancellationToken>());

        // check that identity service was called with deleteuser method
        await _identityService.Received(1).DeleteUserAsync(userId, Arg.Any<CancellationToken>());

     
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnError_IfIdentityDeletionFails()
    {
        // ARRANGE
        var userId = "user-123";

        _memberRepo.GetMemberByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns((Member)null!); 

        _identityService.DeleteUserAsync(userId, Arg.Any<CancellationToken>())
            .Returns(Result.Error("Could not delete user"));

        // ACT
        var result = await _sut.ExecuteAsync(userId);

        // ASSERT
        Assert.False(result.Success);
        Assert.Equal("Could not delete user", result.ErrorMessage);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteIdentity_WhenMemberProfileDoesNotExist()
    {
        // ARRANGE
        var userId = "user-123";

        // return null on member
        _memberRepo.GetMemberByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns((Member?)null);

        // return ok on identity
        _identityService.DeleteUserAsync(userId, Arg.Any<CancellationToken>())
            .Returns(Result.Ok());

        // ACT
        var result = await _sut.ExecuteAsync(userId);

        // ASSERT
        Assert.True(result.Success);

        // check that repo was never called - member was null
        await _memberRepo.DidNotReceive().RemoveAsync(Arg.Any<Member>(), Arg.Any<CancellationToken>());

        // check that identity service was called and delete method for the id
        await _identityService.Received(1).DeleteUserAsync(userId, Arg.Any<CancellationToken>());
    }
}
