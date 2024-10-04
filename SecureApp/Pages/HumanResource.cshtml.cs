using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SecureApp.Dto;
using SecureApp.Models;
using SecureApp.Models.Account;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SecureApp.Pages
{
	[Authorize(Policy = "HrOnly")]
    public class HumanResourceModel : PageModel
    {
		private readonly IHttpClientFactory _httpClientFactory;

		[BindProperty]
		public List<WeatherForecastDto> Forecasts { get; set; } = new List<WeatherForecastDto>();

		public HumanResourceModel(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		private async Task<JwtToken> Authenticate(HttpClient httpClient)
		{
			JwtToken token = null;
			var strToken = HttpContext.Session.GetString("access_token");
			if (!string.IsNullOrEmpty(strToken))
			{
				token = JsonConvert.DeserializeObject<JwtToken>(strToken) ?? new JwtToken();

				if (!string.IsNullOrEmpty(token.AccessToken) && !token.IsExpired)
				{
					return token;
				}
			}

			var response = await httpClient.PostAsJsonAsync("auth", new Credential
			{
				UserName = "admin",
				Password = "password"
			});
			response.EnsureSuccessStatusCode();
			strToken = await response.Content.ReadAsStringAsync();
			HttpContext.Session.SetString("access_token", strToken);
			return JsonConvert.DeserializeObject<JwtToken>(strToken) ?? new JwtToken();
		}

		public async Task OnGet()
        {
			var httpClient = _httpClientFactory.CreateClient("SecureApi");

			var token = await Authenticate(httpClient);

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
			Forecasts = await httpClient.GetFromJsonAsync<List<WeatherForecastDto>>("WeatherForecast") ?? new List<WeatherForecastDto>();
        }
    }
}
