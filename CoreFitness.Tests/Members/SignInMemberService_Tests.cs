using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Members.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Tests.Members;

public class SignInMemberService_Tests
{
    private readonly IIdentityService _identityService;
    private readonly SignInMemberService _sut;

    public SignInMemberService_Tests()
    {
        _identityService = Substitute.For<IIdentityService>();
        _sut = new SignInMemberService(_identityService);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnOk_WhenCredentialsAreValid()
    {
        // ARRANGE
        var input = new SignInInput("test@corefitness.se", "Password123!");

        _identityService.PasswordSignInAsync(input.Email, input.Password, Arg.Any<CancellationToken>())
            .Returns(Result<bool>.Ok(true));

        // ACT
        var result = await _sut.ExecuteAsync(input);

        // ASSERT
        Assert.True(result.Success);
        
        await _identityService.Received(1).PasswordSignInAsync(input.Email, input.Password, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnError_WhenCredentialsAreInvalid()
    {
        // ARRANGE
        var input = new SignInInput("wrong@mail.se", "WrongPassword!");

        _identityService.PasswordSignInAsync(input.Email, input.Password, Arg.Any<CancellationToken>())
            .Returns(Result<bool>.Error("Invalid email or password"));

        // ACT
        var result = await _sut.ExecuteAsync(input);

        // ASSERT
        Assert.False(result.Success);
        Assert.Equal("Invalid email or password", result.ErrorMessage);
    }
}
