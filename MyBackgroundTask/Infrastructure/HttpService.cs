using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBackgroundTask.Infrastructure
{
    public class HttpService
    {
        private static readonly HttpClient Client;
        public async Task<string> PostForm(string url, IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            var uri = new Uri(url);
            return await PostForm(uri, keyValues);
        }

        public async Task<string> PostForm(Uri uri, IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            var formContent = new FormUrlEncodedContent(keyValues);
            return await Post(uri, formContent);
        }
        private async Task<string>Post(Uri uri, ByteArrayContent content)
        {
            HttpResponseMessage response;

            response = await Client.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            string respString = await response.Content.ReadAsStringAsync();

            return respString;
        }
    }
}
