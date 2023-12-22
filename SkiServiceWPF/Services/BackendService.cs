using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkiServiceWPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkiServiceWPF.Services
{
    public class BackendService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BackendService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<RegistrationsModel>> GetRegistrations(string routeKey)
        {
            try
            {
                string baseUrl = _configuration["ApiSettings:BaseUrl"];
                string endpoint = _configuration[$"ApiSettings:{routeKey}"];
                string fullUrl = $"{baseUrl}{endpoint}";
                var response = await _httpClient.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RegistrationsModel>>(json);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return new List<RegistrationsModel>();
        }

        public async Task<LoginResponseModel> LoginAsync(AuthRequestModel authRequest)
        {
            try
            {
                string loginEndpoint = _configuration["ApiSettings:LoginEndpoint"];
                string fullUrl = $"{_configuration["ApiSettings:BaseUrl"]}{loginEndpoint}";

                var json = JsonConvert.SerializeObject(authRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(fullUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Erfolgreiche Authentifizierung
                    return new AuthResponseModel
                    {
                        IsSuccess = true,
                        ResponseMessage = responseContent // oder Deserialisieren, wenn der Body eine spezifische Nachricht enthält
                    };
                }
                else
                {
                    // Authentifizierungsfehler
                    return new AuthResponseModel
                    {
                        IsSuccess = false,
                        ResponseMessage = responseContent // oder eine angepasste Nachricht basierend auf dem Statuscode
                    };

                    throw new Exception($"Server returned non-success status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in LoginAsync: {ex.Message}");
                // Fehlerfall
                return new AuthResponseModel
                {
                    IsSuccess = false,
                    ResponseMessage = $"Ein Fehler ist aufgetreten: {ex.Message}"
                };
                throw new Exception($"Login fehlgeschlagen: {ex.Message}");
            }
        }


    }
}

