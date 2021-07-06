using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace PersonalWebsiteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public GithubController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetResponse(HttpClient client,string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent responseContent = response.Content;

            using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            return await reader.ReadToEndAsync();
        }
        
        
        
        [HttpGet]
        public async Task<List<Github>> Get()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# BombearoAPI");

            var content = GetResponse(client, "https://api.github.com/users/bombearo/repos");

            var gitList = JsonSerializer.Deserialize<List<Github>>(await content);

            if (gitList == null) return null;
            foreach (var repo in gitList)
            {
                var repoContent = GetResponse(client,repo.languages_url);
                repo.languages = JsonSerializer.Deserialize<List<Language>>(await repoContent);
            }

            return gitList;

        }
    }
}