using CoreFitness.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Hosting;
using CoreFitness.Application.Identity;

namespace CoreFitness.Application.Members.Services;

public class DeleteMemberService(
    IMemberRepository memberRepo,
    IIdentityService identityService, 
    IWebHostEnvironment env 
    ) : IDeleteMemberService
{


}
