using System.Collections.Generic;

namespace PersonalWebsiteAPI
{
    public class Github
    {
        public string description { get; set; }

        public string name { get; set; }

        public string languages_url { get; set; }

        public List<Language> languages { get; set; }
    }

    public class Language
    {
        public Language(string name,int chars)
        {
            this.name = name;
            this.chars = chars;
        }
        
        public string name { get; set; }
        
        public int chars { get; set; }
    }
    

}