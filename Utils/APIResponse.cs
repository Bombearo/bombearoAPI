using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteAPI
{
    public abstract class APIResponse
    {
        
        public static async Task<string> GetOauthResponse(HttpClient client, string url, string key)
        {
            using var request = new HttpRequestMessage(new HttpMethod("GET"), url);
            var base64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"Bombearo:{key}"));
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64Authorization}");

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> GetResponse(HttpClient client, string url)
        {

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent responseContent = response.Content;

            using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            return await reader.ReadToEndAsync();
            
        }
    }
}