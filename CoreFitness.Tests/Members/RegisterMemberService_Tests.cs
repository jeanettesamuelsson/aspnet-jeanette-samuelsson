
using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Members.Services;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using NSubstitute;


namespace CoreFitness.UnitTests.Application.Members;

public class RegisterMemberService_Tests
{
    private readonly IIdentityService _identityService;
    private readonly IMemberRepository _memberRepository;
    private readonly RegisterMemberService _sut; // system under test

    public RegisterMemberService_Tests()
    {
        // create mocks with NSubstitute
        _identityService = Substitute.For<IIdentityService>();
        _memberRepository = Substitute.For<IMemberRepository>();

        // inject in service/system under test
        _sut = new RegisterMemberService(_identityService, _memberRepository);
    }


    //HAPPY PATH

    [Fact]
    public async Task ExecuteAsync_ShouldReturnOk_WhenUserAndMemberCreatedSuccessfully()
    {
        // ARRANGE
        var input = new RegisterMemberInput("jeanette@jeanette.com", "Password123!");
        var userId = "generated-guid-123";

        // create user with identityService and return OK result
        _identityService.CreateUserAsync(input.Email, input.Password, Arg.Any<CancellationToken>())
            .Returns(Result<string?>.Ok(userId));

        // ACT
        var result = await _sut.ExecuteAsync(input);

        // ASSERT 
        Assert.True(result.Success);
        Assert.Equal(userId, result.Value);

        // check that repo was called once and that the members user id == userId
        await _memberRepository.Received(1).AddAsync(
            Arg.Is<Member>(m => m.UserId == userId),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnError_WhenIdentityCreationFails()
    {
        // ARRANGE 
        var input = new RegisterMemberInput("jeanette@jeanette.com", "Password123!");

        // create user and return error
        _identityService.CreateUserAsync(input.Email, input.Password, Arg.Any<CancellationToken>())
            .Returns(Result<string?>.Error("Failed creating user"));

        // ACT
        var result = await _sut.ExecuteAsync(input);

        // ASSERT
        Assert.False(result.Success);

        // check that repo was never called 
        await _memberRepository.DidNotReceive().AddAsync(
            Arg.Any<Member>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnConflict_WhenEmailAlreadyExists()
    {
        // ARRANGE
        var input = new RegisterMemberInput("jeanette@jeanette.com", "Password123!");

        // create user and return conflict
        _identityService.CreateUserAsync(input.Email, input.Password, Arg.Any<CancellationToken>())
            .Returns(Result<string?>.Conflict("Email already exists."));

        // ACT
        var result = await _sut.ExecuteAsync(input);

        // ASSERT
        Assert.False(result.Success);

        // check that repo was never called
        await _memberRepository.DidNotReceive().AddAsync(
            Arg.Any<Member>(),
            Arg.Any<CancellationToken>()
        );
    }
}