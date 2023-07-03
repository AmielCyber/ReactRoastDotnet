namespace ReactRoastDotnet.Data.Models.User;

/// <summary>
/// User object to send after a user successfully logins.
/// </summary>
/// <param name="FirstName">First name of the user.</param>
/// <param name="LastName">Last name of the user.</param>
/// <param name="Email">User's email.</param>
/// <param name="Token">The token to access the application.</param>
public record LoggedInUserDto(string FirstName, string LastName, string Email, string Token);