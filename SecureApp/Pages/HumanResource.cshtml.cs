using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureApp.Dto;

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

		public async Task OnGet()
        {
			var httpClient = _httpClientFactory.CreateClient("SecureApi");
			Forecasts = await httpClient.GetFromJsonAsync<List<WeatherForecastDto>>("WeatherForecast") ?? new List<WeatherForecastDto>();
        }
    }
}
