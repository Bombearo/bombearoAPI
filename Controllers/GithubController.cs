using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
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
        
        
        
        [HttpGet]
        public async Task<string> Get()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# BombearoAPI");
            
            HttpResponseMessage response = await client.GetAsync(
                $"https://api.github.com/users/bombearo/repos");
            HttpContent responseContent = response.Content;
            
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                var content = await reader.ReadToEndAsync();
            }

            return "Jo";
        }
    }
}