using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceWPFBiluliBBackend.Common
{
    public class UserCreationApi
    {
        private readonly HttpClient _httpClient;

        public UserCreationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateUserAsync(UserModel userModel)
        {
            var json = JsonConvert.SerializeObject(userModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/users/create", content);

            return response.IsSuccessStatusCode;
        }
    }
}
