using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecureApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecureApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public AuthController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost]
		public IActionResult Authenticate([FromBody] Credential credential)
		{
			if (ValidateCredentials(credential))
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, "admin"),
					new Claim(ClaimTypes.Email, "t@g.c"),
					new Claim("Admin", "true"),
					new Claim("Department", "HR"),
					new Claim("StartedDate", "2024-06-01"),
				};

				var expiresAt = DateTime.Now.AddMinutes(15);

				return Ok(new
				{
					access_token = GenerateToken(claims, expiresAt),
					expires_at = expiresAt,
				});
			}

			ModelState.AddModelError("Unauthorized", "You are not unauthorized to access the endpoint");
			return Unauthorized(ModelState);
		}

		private bool ValidateCredentials(Credential credential)
		{
			return credential.UserName == "admin" && credential.Password == "password";
		}

		private string GenerateToken (IEnumerable<Claim> claims, DateTime expireAt) {
			var secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));

			var jwt = new JwtSecurityToken(
				claims: claims,
				notBefore: DateTime.Now,
				expires: expireAt,
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(secretKey),
					SecurityAlgorithms.HmacSha256Signature
				)
			);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}	
	}
}
