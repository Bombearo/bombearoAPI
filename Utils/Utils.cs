using System;

namespace PersonalWebsiteAPI
{
    public class Utils
    {
        public static string getTimeBetween(DateTime time)
        {
            var d = (DateTime.Now-time).TotalDays;
            var y = (int) (d / 365);
            var days = (int) d;
            var m = days % 30;
            
            Console.WriteLine(days);Console.WriteLine(m);Console.WriteLine(y);

            string loyaltyString = "Created ";
            if (y > 0)
            {
                days %= (y * 365);
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
                days %= (m * 12);
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