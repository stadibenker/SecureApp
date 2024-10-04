using Newtonsoft.Json;

namespace SecureApp.Models
{
	public class JwtToken
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; } = string.Empty;

		[JsonProperty("expires_at")]
		public DateTime ExpiresAt { get; set; }

		public bool IsExpired { get => DateTime.Now > ExpiresAt; }
	}
}
