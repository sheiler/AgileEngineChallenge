using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Services
{
    public class AgileEngineClient : IAgileEngineClient
    {
        private readonly HttpClient _client;
        // ToDo: app.config
        private readonly string secret = "23567b218376f79d9415";

        public AgileEngineClient(HttpClient httpClient) 
        {
            _client = httpClient;
            _client.BaseAddress = new Uri("https://developer.setmore.com"); // ToDo endpoint list in a const
        }

        public async Task<ImageModelResponse> GetAll(int page = 1)
        {
            var data = await GetData("/Images?page=" + page.ToString());

            var response = JsonSerializer.Deserialize<ImageModelResponse>(data);

            return response;
        }

        public async Task<ImageDetails> GetDetails(string id)
        {
            var data = await GetData("/Images/" + id);

            var response = JsonSerializer.Deserialize<ImageDetails>(data);

            return response;
        }

        private async Task<HttpResponseMessage> PostData(string endpoint, string content)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(_client.BaseAddress + endpoint);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await _client.PostAsync(endpoint, byteContent);
        }

        private async Task<string> GetData(string endpoint, int retry = 0)
        {
            try
            {
                return await _client.GetStringAsync(endpoint);
            }
            catch (HttpRequestException hex)
            {
                if(hex.HResult == 401 && retry == 0)
                {
                    await RefreshToken();
                    retry++;
                    return await this.GetData(endpoint, retry);
                }

                throw hex;
            }
            
        }
        private async Task<AuthResponse> GetToken()
        {
            var rq = new AuthRequest(this.secret);

            var rs = await PostData("/auth", JsonSerializer.Serialize(rq));

            if (rs.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<AuthResponse>(rs.Content.ReadAsStringAsync().Result);
            else
                throw new Exception("Wrong status code"); // ToDo CustomException
        }
        private async Task RefreshToken()
        {
            var token = await GetToken();

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", token.token);
        }
    }
}
