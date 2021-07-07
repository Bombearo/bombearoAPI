using System;
using System.Collections.Generic;
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
        private readonly ILogger<GithubController> _logger;

        public GithubController(ILogger<GithubController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Github>> Get()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# BombearoAPI");

            var content = Utils.GetResponse(client, "https://api.github.com/users/bombearo/repos");

            var gitList = JsonSerializer.Deserialize<List<Github>>(await content);

            if (gitList == null) return null;
            foreach (var repo in gitList)
            {
                var repoContent = Utils.GetResponse(client,repo.languages_url);
                var serializedRepo = JsonSerializer.Deserialize<Dictionary<string, int>>(await repoContent);
                if (serializedRepo != null)
                {
                    var languages = new List<Language>();
                    languages.AddRange(serializedRepo.Select(languagePair => new Language(languagePair.Key, languagePair.Value)));
                    repo.languages = languages;
                }

            }

            return gitList;

        }
    }
}