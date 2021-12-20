using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace PersonalWebsiteAPI
{
    public class Github
    {
        public string description { get; set; }

        public string name { get; set; }

        public string languages_url { get; set; }

        public List<Language> Languages { get; set; }

        public bool fork { get; set; }
        
        public string html_url { get; set; }
        
        public string node_id { get; set; }
        
        public string clone_url { get; set; }
        
        public string ssh_url { get; set; }
        
        public string git_url { get; set; }
        
        public int stargazers_count { get; set; }
        
        public int watchers_count { get; set; }
        
        public DateTime created_at { get; set; }
        public DateTime pushed_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Language
    {
        public Language(string name,int chars)
        {
            Name = name;
            Chars = chars;
        }
        
        public string Name { get; set; }
        
        public int Chars { get; set; }
        
        public string Percentage { get; set; }
    }
    

}