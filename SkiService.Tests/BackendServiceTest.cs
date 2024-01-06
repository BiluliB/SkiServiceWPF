using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using SkiServiceWPF.DTOs;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;

namespace SkiService.Tests
{
    public class BackendServiceTests
    {
        [Fact]
        public async Task GetRegistrations_ReturnsExpectedData()
        {
            // Arrange
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("[{'RegistrationId':1,'FirstName':'John','LastName':'Doe'}]")
                });

            var httpClient = new HttpClient(httpClientMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiSettings:BaseUrl"]).Returns("https://localhost:7119");
            configurationMock.Setup(c => c["ApiSettings:GetAllRegistrationsEndpoint"]).Returns("/Registrations");

            var backendService = new BackendService(httpClient, configurationMock.Object);

            // Act
            var result = await backendService.GetRegistrations("GetAllRegistrationsEndpoint");

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].FirstName);
        }

        [Fact]
        public async Task GetStatuses_ReturnsExpectedData()
        {
            // Arrange
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("[{'statusId': 1, 'statusName': 'Offen', 'registration': []}]") // Ein StatusDto-Objekt
                });

            var httpClient = new HttpClient(httpClientMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiSettings:BaseUrl"]).Returns("https://localhost:7119");
            configurationMock.Setup(c => c["ApiSettings:GetStatusEndpoint"]).Returns("/Status");

            var backendService = new BackendService(httpClient, configurationMock.Object);

            // Act
            var result = await backendService.GetStatuses("GetStatusEndpoint");

            // Assert
            Assert.Single(result); // Überprüft, ob genau ein StatusDto-Objekt zurückgegeben wird
            Assert.Equal(1, result[0].StatusId);
            Assert.Equal("Offen", result[0].StatusName);
        }



        [Fact]
        public async Task LoginAsync_ReturnsAuthResponse()
        {
            // Arrange
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{'IsSuccess':true,'Token':'test-token'}")
                });

            var httpClient = new HttpClient(httpClientMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiSettings:BaseUrl"]).Returns("https://localhost:7119");
            configurationMock.Setup(c => c["ApiSettings:LoginEndpoint"]).Returns("/Employees/login");

            var backendService = new BackendService(httpClient, configurationMock.Object);
            var authRequestDto = new AuthRequestDto { UserName = "user", Password = "pass" };

            // Act
            var result = await backendService.LoginAsync(authRequestDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("test-token", result.Token);
        }

        [Fact]
        public async Task UpdateRegistrationAsync_ReturnsTrueOnSuccess()
        {
            // Arrange
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var httpClient = new HttpClient(httpClientMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiSettings:BaseUrl"]).Returns("https://localhost:7119");

            var backendService = new BackendService(httpClient, configurationMock.Object);
            var registrationModel = new RegistrationModel { RegistrationId = 1, FirstName = "John", LastName = "Doe" };

            // Act
            var result = await backendService.UpdateRegistrationAsync(registrationModel);

            // Assert
            Assert.True(result);
        }
    }
}
