namespace Project.Identity.Dto.Jwt;

public record TokenResultDto(
    string Token,
    string RefreshToken
);