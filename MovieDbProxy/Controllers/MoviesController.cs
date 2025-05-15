using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MovieDbProxy.Models;

namespace MovieDBProxyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        // Combined constructor to accept both dependencies
        public MoviesController(IOptions<MovieDBSettings> appSettings, IHttpClientFactory httpClientFactory)
        {
            _apiKey = appSettings.Value.ApiKey; // Access the API key from configuration
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularMovies()
        {
            string apiKey = _apiKey;
            string endpoint = $"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}";

            var response = await _httpClient.GetStringAsync(endpoint);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string query)
        {
            string endpoint = $"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={query}";

            var response = await _httpClient.GetStringAsync(endpoint);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            string endpoint = $"https://api.themoviedb.org/3/movie/{id}?api_key={_apiKey}";

            var response = await _httpClient.GetStringAsync(endpoint);
            return Ok(response);
        }

        [HttpGet("{id}/recommendations")]
        public async Task<IActionResult> GetMovieRecommendations(int id)
        {
            string endpoint = $"https://api.themoviedb.org/3/movie/{id}/recommendations?api_key={_apiKey}";

            var response = await _httpClient.GetStringAsync(endpoint);
            return Ok(response);
        }
        [HttpGet("tv/popular")]
        public async Task<IActionResult> GetPopularTVShows()
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/tv/popular?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Content(json, "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
        [HttpGet("now_playing")]
        public async Task<IActionResult> GetNowPlaying()
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/now_playing?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Content(json, "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }

        // New endpoint: 'upcoming' movies
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/upcoming?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Content(json, "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }

        // New endpoint: 'top rated' movies
        [HttpGet("top_rated")]
        public async Task<IActionResult> GetTopRated()
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/top_rated?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Content(json, "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
    }
    }
