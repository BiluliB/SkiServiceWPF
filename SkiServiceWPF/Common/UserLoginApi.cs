using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SkiServiceWPF.Models;

namespace SkiServiceWPF.Common
{
    public class UserLoginApi
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public UserLoginApi(HttpClient httpClient, ApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
        }

        public async Task<LoginResponseModel> LoginAsync(AuthRequestModel authRequest)
        {
            var json = JsonConvert.SerializeObject(authRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiSettings.LoginEndpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                // Fehlerbehandlung
                throw new Exception("Fehler beim Login-Vorgang.");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
        }
    }

}
