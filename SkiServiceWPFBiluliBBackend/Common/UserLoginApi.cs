using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceWPFBiluliBBackend.Common
{
    public class UserLoginApi
    {
        private readonly HttpClient _httpClient;

        public UserLoginApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponseModel> LoginAsync(AuthRequestModel authRequest)
        {
            var json = JsonConvert.SerializeObject(authRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/users/login", content);

            if (!response.IsSuccessStatusCode)
            {
                // Fehlerbehandlung
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
        }
    }
}
