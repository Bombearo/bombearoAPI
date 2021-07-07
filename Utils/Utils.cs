using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalWebsiteAPI
{
    public abstract class Utils
    {
        public static async Task<string> GetResponse(HttpClient client,string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent responseContent = response.Content;

            using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            return await reader.ReadToEndAsync();
        }
    }
}