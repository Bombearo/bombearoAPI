using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Octokit;
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
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
            var user = await github.User.Get("Bombearo");
            Console.WriteLine(user.Followers + " folks love Bombearo!");
            return "Jo";
        }
    }
}