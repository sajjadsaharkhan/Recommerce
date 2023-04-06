namespace Recommerce.Identity.Dto.OtpLogin;

public record UserLoginInformationDto(
    string FirstName,
    string LastName
)
{
    public int UserId { get; set; }
}