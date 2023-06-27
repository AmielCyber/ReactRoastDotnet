namespace ReactRoastDotnet.API.Models.RequestDto;

public record UserRegisterDto : UserLoginDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}