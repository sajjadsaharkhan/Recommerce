namespace Project.Identity.Dto;

public record LoginResponseDataDto
(
    string FirstName,
    string LastName,
    string Token,
    string RefreshToken,
    bool IsNewUser
);