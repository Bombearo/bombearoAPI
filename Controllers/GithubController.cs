using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;


namespace PersonalWebsiteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly ILogger<GithubController> _logger;

        private IMemoryCache _cache;
        
        public GithubController(IMemoryCache memoryCache,ILogger<GithubController> logger)
        {
            _logger = logger;
            _cache = memoryCache;
        }

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        [Route("repos")]
        public async Task<List<Github>> GetRepos()
        {
            const string username = "Bombearo";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# BombearoAPI");

            var content = APIResponse.GetOauthResponse(client, $"https://api.github.com/users/{username.ToLower()}/repos",APIKeys.githubKey);
            

            var gitList = JsonSerializer.Deserialize<List<Github>>(await content);

            if (gitList == null) return null;

            var filteredGitList = (from repo in gitList
                where repo.name != username
                select repo).ToList();
            
            
            
            foreach (var repo in filteredGitList)
            {
                var repoContent = APIResponse.GetResponse(client,repo.languages_url);
                var serializedRepo = JsonSerializer.Deserialize<Dictionary<string, int>>(await repoContent);
                if (serializedRepo != null)
                {
                    var languages = new List<Language>();
                    languages.AddRange(serializedRepo.Select(languagePair => new Language(languagePair.Key, languagePair.Value)));
                    repo.Languages = languages;
                }

                var totalChars = repo.Languages.Sum(x => x.Chars);
                foreach (var language in repo.Languages)
                {
                    language.Percentage = ((double)language.Chars / (double)totalChars).ToString("0.00%");
                }
                


            }

            return filteredGitList;

        }

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        [Route("about")]
        public async void GetAbout()
        {
            Console.WriteLine("Test");
        }
    }
}