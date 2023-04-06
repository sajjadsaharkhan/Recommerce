namespace Project.Identity.Dto.Jwt;

public record RefreshTokenRequestDto(
    string JwtToken,
    string RefreshToken
);