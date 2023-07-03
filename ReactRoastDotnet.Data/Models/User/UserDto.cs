namespace ReactRoastDotnet.Data.Models.User;

/// <summary>
/// User data transfer object to be sent after a successful login.
/// </summary>
/// <param name="FirstName">User's first name.</param>
/// <param name="LastName">User's last name.</param>
/// <param name="Email">User's email.</param>
public record UserDto(string FirstName, string LastName, string Email);