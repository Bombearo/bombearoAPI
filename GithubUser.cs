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
                var d = (DateTime.Now-created_at).TotalDays;
                var y = (int)(d / 365);
                var m = y % 12;
                var days = m % 30;

                string loyaltyString = "Created ";
                if (y > 0)
                {
                    if (y == 1)
                    {
                        loyaltyString += $"{y} year";
                    }
                    else
                    {
                        loyaltyString += $"{y} years";
                    }
                }
                if (m > 0)
                {
                    if (y > 0)
                    {
                        loyaltyString += ", ";
                    }
                    if (m == 1)
                    {
                        loyaltyString += $"{m} month";
                    }
                    else
                    {
                        loyaltyString += $"{m} months";
                    }
                }

                if (days <= 0) return loyaltyString + " ago";
                if (m > 0 || y > 0)
                {
                    loyaltyString += ", and ";
                }
                if (days == 1)
                {
                    loyaltyString += $"{days} day";
                }
                else
                {
                    loyaltyString += $"{days} days";
                }
                return loyaltyString + " ago";
            }
        }
    }
}