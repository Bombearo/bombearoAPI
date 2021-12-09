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