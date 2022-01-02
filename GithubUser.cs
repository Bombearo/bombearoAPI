using System;

namespace PersonalWebsiteAPI
{
    public class GithubUser
    {
        public string login { get; set; }
        
        public string repos_url { get; set; }
        
        public string avatar_url { get; set; }
        
        public string html_url { get; set; }
        
        public string bio { get; set; }
        
        public int followers { get; set; }
        
        public int following { get; set; }
        
        public int public_repos { get; set; }
        
        public DateTime created_at { get; set; }

        public string loyalty
        {
            get
            {
                return Utils.getTimeBetween(created_at);
            }
        }
    }
}