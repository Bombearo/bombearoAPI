using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace PersonalWebsiteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly ILogger<GithubController> _logger;

        private IMemoryCache _cache;

        private readonly HttpClient _client;
        
        public GithubController(IMemoryCache memoryCache,ILogger<GithubController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", "C# BombearoAPI");
            _cache = memoryCache;
        }

        
        
        [ResponseCache(Duration = 3600)]
        [HttpGet]
        [Route("user")]
        public async Task<GithubUser> GetUser()
        {
            const string username = "Bombearo";
            var content = APIResponse.GetOauthResponse(_client, $"https://api.github.com/users/{username.ToLower()}",APIKeys.githubKey);
            return JsonSerializer.Deserialize<GithubUser>(await content);
        }
        
        
        [ResponseCache(Duration = 3600)]
        [HttpGet]
        [Route("repos")]
        public async Task<List<Github>> GetRepos()
        {
            var gitUser = await GetUser();
            var content = APIResponse.GetOauthResponse(_client, gitUser.repos_url,APIKeys.githubKey);
            

            var gitList = JsonSerializer.Deserialize<List<Github>>(await content);

            if (gitList == null) return null;

            var filteredGitList = (from repo in gitList
                where repo.name != gitUser.login
                select repo).ToList();
            
            
            
            foreach (var repo in filteredGitList)
            {
                var repoContent = APIResponse.GetResponse(_client,repo.languages_url);
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
                    language.Percentage = (language.Chars / (double)totalChars).ToString("0.00%");
                }
                


            }

            return filteredGitList;

        }

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        [Route("about")]
        public void GetAbout()
        {
            Console.WriteLine("Test");
        }
    }
}