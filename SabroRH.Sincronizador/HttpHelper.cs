using System.CodeDom;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SabroRH.Sincronizador
{
    public class HttpHelper
    {
        private const string BASE_URL = "https://api-rh.sabrosistemas.com.br";

        private string TOKEN { get; set; }

        public HttpHelper(string token)
        {
            TOKEN = token;
        }

        public HttpHelper() { }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(str, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, dynamic content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonSerializer.Serialize(content);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(endpoint, body);
                //response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(str, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, dynamic content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonSerializer.Serialize(content);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(endpoint, body);
                //response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(str, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}
