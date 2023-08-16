using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReactRoastDotnet.Data.Entities;

namespace ReactRoastDotnet.API.Services;

public class TokenService
{
  private readonly UserManager<User> _userManager;
  private readonly IConfiguration _configuration;

  public TokenService(UserManager<User> userManager, IConfiguration configuration)
  {
    _userManager = userManager;
    _configuration = configuration;
  }

  public async Task<string> GenerateToken(User user)
  {
    if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email))
    {
      throw new Exception("Invalid user email or username passed for token creation.");
    }

    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
    var roles = await _userManager.GetRolesAsync(user);
    foreach (var role in roles)
    {
      claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:TokenKey"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

    var tokenOptions = new JwtSecurityToken(
        issuer: null,
        audience: null,
        claims: claims,
        expires: DateTime.Now.AddDays(7),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
  }
}
