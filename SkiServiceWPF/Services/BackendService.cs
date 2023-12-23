using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkiServiceWPF.DTOs;
using SkiServiceWPF.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace SkiServiceWPF.Services
{
    /// <summary>
    /// BackendService class for communicating with the backend
    /// </summary>
    public class BackendService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for BackendService
        /// </summary>
        /// <param name="httpClient">HTTP client for making requests</param>
        /// <param name="configuration">Configuration settings</param>
        #region BackendService
        public BackendService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        #endregion

        /// <summary>
        /// Gets a list of registrations from the backend
        /// </summary>
        /// <param name="routeKey">Route key to identify specific endpoints</param>
        /// <returns>List of RegistrationModel instances</returns>
        /// <exception cref="Exception">Throws an exception if the request fails</exception>
        #region GetRegistrations
        public async Task<List<RegistrationModel>> GetRegistrations(string routeKey)
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
                    var registrationdto = JsonConvert.DeserializeObject<List<RegistrationsDto>>(json);
                    var models = registrationdto.Select(dto => ConvertDtoToModel(dto)).ToList();
                    return models;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return new List<RegistrationModel>();
        }
        #endregion

        /// <summary>
        /// Gets a list of statuses with their registrations from the backend
        /// </summary>
        /// <param name="routeKey">Route key to identify specific endpoints for statuses</param>
        /// <returns>List of StatusDto instances</returns>
        /// <exception cref="Exception">Throws an exception if the request fails</exception>
        public async Task<List<StatusDto>> GetStatuses(string routeKey)
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
                    var statuses = JsonConvert.DeserializeObject<List<StatusDto>>(json);
                    return statuses;
                }
                else
                {
                    // Behandeln Sie nicht erfolgreiche Statuscodes entsprechend
                    throw new Exception($"Anfrage fehlgeschlagen mit Statuscode: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Abrufen der Statusdaten: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Convers a RegistrationsDto instance to a RegistrationModel instance
        /// </summary>
        /// <param name="registrationdto"></param>
        /// <returns></returns>
        private RegistrationModel ConvertDtoToModel(RegistrationsDto registrationdto)
        {
            return new RegistrationModel
            {
                RegistrationId = registrationdto.RegistrationId,
                LastName = registrationdto.LastName,
                FirstName = registrationdto.FirstName,
                PickupDate = registrationdto.PickupDate,
                Priority = registrationdto.Priority,
                Service = registrationdto.Service,
                Status = registrationdto.Status

            };
        }

        /// <summary>
        /// Processes login requests asynchronously
        /// </summary>
        /// <param name="authRequestDto">Authentication request DTO</param>
        /// <returns>Response DTO indicating authentication success or failure</returns>
        #region LoginAsync
        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto authRequestDto)
        {
            try
            {
                string loginEndpoint = _configuration["ApiSettings:LoginEndpoint"];
                string fullUrl = $"{_configuration["ApiSettings:BaseUrl"]}{loginEndpoint}";

                var json = JsonConvert.SerializeObject(authRequestDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(fullUrl, content);

                Debug.WriteLine($"Sending POST request to {fullUrl}");

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Successful authentication
                    var authResponseDto = JsonConvert.DeserializeObject<AuthResponseDto>(responseContent);
                    authResponseDto.IsSuccess = true;
                    return authResponseDto;
                }
                else
                {
                    // Authentication error
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        ResponseMessage = responseContent
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in LoginAsync: {ex.Message}");
                // Error case
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    ResponseMessage = $"Ein Fehler ist aufgetreten HTTP 404"
                };
            }
            #endregion
        }
    }
}
