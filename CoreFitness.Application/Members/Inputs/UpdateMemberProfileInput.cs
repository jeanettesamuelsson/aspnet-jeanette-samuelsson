using Microsoft.AspNetCore.Http;

namespace CoreFitness.Application.Members.Inputs;

public record UpdateMemberProfileInput(
    
    string UserId,
    string FirstName,
    string LastName, 
    string? PhoneNumber,
    IFormFile? ProfileFile

    );


