using IMDBRapidAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace IMDBRapidAPI.Controllers
{
    public class HomeController : Controller
    {
        public async Task< IActionResult> Index(int page = 1)
        {
            List<MovieViewModel> movies = new List<MovieViewModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
    {
        { "x-rapidapi-key", "1678b341acmsh9cf54c10397041cp1400b9jsn73fdb90bb65d" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<MovieViewModel>>(body);
                var pagedMovies = movies.ToPagedList(page, 5);
                return View(pagedMovies);
            }
            

        }
       
    }
}
