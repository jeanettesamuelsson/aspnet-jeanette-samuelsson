using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CoreFitness.Application.Members.Services;

public class DeleteMemberService(
    IMemberRepository memberRepo,
    IIdentityService identityService, 
    IWebHostEnvironment env 
    ) : IDeleteMemberService
{
    //method to delete member and remove image from server --->!!(got help from AI with this)!!<----
    public async Task<Result> ExecuteAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            // get member
            var member = await memberRepo.GetMemberByUserIdAsync(userId, ct);

            if (member != null)
            {
                // remove file from server if exists (not null)
                if (!string.IsNullOrWhiteSpace(member.ProfileImageUri))
                {
                    var fullFilePath = Path.Combine(env.WebRootPath, member.ProfileImageUri.TrimStart('/'));
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                    }
                }

                // remove member profile from DB (with repo)
                await memberRepo.RemoveAsync(member, ct);
            }

            // remove (identity) user from DB (with userManager)
            return await identityService.DeleteUserAsync(userId, ct);
        }
        catch (Exception ex)
        {
            return Result.Error($"An error accured while trying to delete member: {ex.Message}");
        }
    }

}
