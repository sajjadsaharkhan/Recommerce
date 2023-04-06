namespace Project.Identity.Dto.Jwt;

public record JwtCredentialDto(
    int UserId,
    string UserName,
    string SecurityStamp
);