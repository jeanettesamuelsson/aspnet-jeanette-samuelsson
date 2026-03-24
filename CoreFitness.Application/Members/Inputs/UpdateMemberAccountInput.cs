namespace CoreFitness.Application.Members.Inputs;

public record UpdateMemberAccountInput(
    
    string UserId,
    string FirstName,
    string LastName, 
    string? PhoneNumber, 
    string? ProfileImageUri

    );


