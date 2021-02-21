using System;
using System.Collections.Generic;
using System.Net;
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
            _client.BaseAddress = new Uri("http://interview.agileengine.com"); // ToDo endpoint list in a const
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ImageModelResponse> GetAll(int page = 1)
        {
            var data = await GetDataWithToken(_client.BaseAddress + "Images?page=" + page.ToString()); // ToDo bulild uris propertly

            var response = JsonSerializer.Deserialize<ImageModelResponse>(data.Content.ReadAsStringAsync().Result);

            return response;
        }

        public async Task<ImageDetails> GetDetails(string id)
        {
            var data = await GetDataWithToken(_client.BaseAddress+"Images/" + id); // ToDo bulild uris propertly

            var response = JsonSerializer.Deserialize<ImageDetails>(data.Content.ReadAsStringAsync().Result);

            return response;
        }

        #region "Primtive http"
        private async Task<HttpResponseMessage> PostData(string endpoint, string content)
        {
            var con = await _client.PostAsync(endpoint, new StringContent(content, Encoding.UTF8, "application/json"));

            return con;
        }
        private async Task<HttpResponseMessage> GetDataWithToken(string endpoint)
        {
            var rs = await _client.GetAsync(endpoint);

            if(rs.StatusCode == HttpStatusCode.OK)
            {
                return rs;
            }
            else if(rs.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshToken();
                return await _client.GetAsync(endpoint);
            }
            else
            {
                throw new Exception("Error getting information from the remote server.");
            }
           
        }
        #endregion

        #region "Token handling"
        private async Task<AuthResponse> GetToken()
        {
            var rq = new AuthRequest(this.secret);

            var rs = await PostData(_client.BaseAddress + "auth", JsonSerializer.Serialize(rq));

            if (rs.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<AuthResponse>(rs.Content.ReadAsStringAsync().Result);
            else if (rs.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Could not negotiate token. Possible cause: wrong apikey."); // ToDo CustomException
            else 
                throw new Exception("Error during connection"); // ToDo CustomException
        }
        private async Task RefreshToken()
        {
            var token = await GetToken();

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", token.token);
        }
        #endregion
    }
}
