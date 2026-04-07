using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using Microsoft.AspNetCore.Hosting;


namespace CoreFitness.Application.Members.Services;

public class UpdateMemberProfileService(IMemberRepository memberRepository, IWebHostEnvironment environment) : IUpdateMemberProfileService
{
    public async Task<Result<Member>> ExecuteAsync(UpdateMemberProfileInput input, CancellationToken ct = default)
    {
        try
        {
            if (input is null)
                return Result<Member>.BadRequest("Input must be provided");

            var member = await memberRepository.GetMemberByUserIdAsync(input.UserId, ct);
            if (member is null)
                return Result<Member>.NotFound("Member was not found");


            // handle file upload if a new profile file is provided, otherwise keep existing URI

            string? fileName = member.ProfileImageUri; 

            if (input.ProfileFile != null && input.ProfileFile.Length > 0)
            {
                // create a unique file name using a GUID and preserve the original file extension

                fileName = $"{Guid.NewGuid()}{Path.GetExtension(input.ProfileFile.FileName)}";

                // Path to wwwroot/uploads/profiles
                var folderPath = Path.Combine(environment.WebRootPath, "uploads", "profiles");
                var filePath = Path.Combine(folderPath, fileName);

                // Create the folder if it doesn't exist
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // Save the file to the server
                using var stream = new FileStream(filePath, FileMode.Create);
                await input.ProfileFile.CopyToAsync(stream, ct);

                // update the fileName to be the relative path that can be stored in the database (e.g., /uploads/profiles/filename.jpg)
                fileName = $"/uploads/profiles/{fileName}";
            }


            member.UpdateInformation(input.FirstName, input.LastName, input.PhoneNumber, fileName);

            //result from repo returns true or false
            var result = await memberRepository.UpdateAsync(member, ct);

            //if result is not true(not updated) -> returns Error result, else ok result with updated member object
            return !result ? Result<Member>.Error("Member was not updated") : Result<Member>.Ok(member);


        }
        catch (Exception ex)
        {
            return Result<Member>.Error(ex.Message);
        }

    }
}
