using FluentAssertions;
using SoccerX.Common.Constants;
using SoccerX.Common.Extensions;
using SoccerX.DTO.Dto.User;
using SoccerX.DTO.Requests.Security;
using SoccerX.DTO.Responses;
using SoccerX.Tests.Base.IntegrationFactory;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SoccerX.Tests.Testing.Integration
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        #region Field
        private readonly HttpClient _client;
        #endregion

        #region Constructor
        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("Accept-Language", "tr-TR");
            var token = LocalLogin().Result;
            if(token != null)
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
            }
        }
        #endregion

        #region Public Method

        public async Task<AuthResponseDto?> LocalLogin()
        {
            // Arrange
            var request = new LocalLoginRequest
            {
                EmailOrUserName = "salih.yucel@univera.com.tr",
                Password = "Test1234!"
            };
            // Act
            var response = await _client.PostAsJsonAsync(SoccerXConstants.Authenticate_Local, request);
            if(response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            // 🔍 DEBUG LOG
            Console.WriteLine($"[DEBUG] StatusCode: {response.StatusCode}");
            Console.WriteLine($"[DEBUG] ResponseContent: {responseContent}");
            return responseContent?.FromJsonNewton<AuthResponseDto>() ?? new AuthResponseDto();
        }

        [Fact]
        public async Task VerifyEmail_ShouldReturnOk_WhenCodeIsValid()
        {
            // Arrange
            var fakeCode = "241174"; // Varsayım: bu kod test ortamında geçerli
            var requestContent = JsonContent.Create(fakeCode);

            // Act
            var response = await _client.PostAsync(SoccerXConstants.User_VerifyEmail, requestContent);

            // DEBUG: Hata olması durumunda içeriği göster
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[VerifyEmail] StatusCode: {response.StatusCode}");
            Console.WriteLine($"[VerifyEmail] Content: {content}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"but received {response.StatusCode} with content: {content}");
        }

        [Fact]
        public async Task SendNewVerifyEmail_ShouldReturnOk_WhenCalledWithEmptyString()
        {
            // Arrange
            var requestBody = JsonContent.Create(""); // veya new StringContent("\"\"", Encoding.UTF8, "application/json")

            // (Varsa) Token gerekiyorsa ekleyin:
            // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validJwtToken);

            // Act
            var response = await _client.PostAsync(SoccerXConstants.User_SendNewVerifyEmaill, requestBody);
            Thread.Sleep(TimeSpan.FromSeconds(15)); // Gecikme ekleyin, gerekirse

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[SendNewVerifyEmail] StatusCode: {response.StatusCode}");
            Console.WriteLine($"[SendNewVerifyEmail] Content: {responseContent}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                $"but received {response.StatusCode} with content: {responseContent}");
        }

        [Fact]
        public async Task VerifyEmail_ShouldReturnError_WhenCodeIsNotValid()
        {
            // Arrange
            var fakeCode = "485511"; // Varsayım: bu kod test ortamında geçerli
            var requestContent = JsonContent.Create(fakeCode);

            // Act
            var response = await _client.PostAsync(SoccerXConstants.User_VerifyEmail, requestContent);

            // DEBUG: Hata olması durumunda içeriği göster
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[VerifyEmail] StatusCode: {response.StatusCode}");
            Console.WriteLine($"[VerifyEmail] Content: {content}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);            
        }

        [Fact]
        public async Task Register_ShouldCreateUser_WhenValidRequest()
        {
            // Arrange
            var prefix = Guid.NewGuid().ToString("N").Substring(0, 5);
            var request = new UserCreateDto
            {
                Email = "salih.yucel@univera.com.tr",
                Password = "Test1234!",
                Username = prefix + "testuser",
                Address = "123 Test St",
                Countryid = Guid.Parse("13d1f194-f6bd-47d8-9728-2149f1e1f923"),
                Cityid = Guid.Parse("d47d77f5-56a3-47ac-88c7-a00eb430086d"),
                Name = prefix + "Salih",
                Surname = prefix + "Yücel",
                Phonenumber = prefix + "1234567890",                
                Birthdate = DateOnly.FromDateTime(DateTime.Now),
                Gender = Domain.Enums.UserGender.Male,
                Postalcode = "12345",
                Referraluserid = null
            };

            // Act
            var response = await _client.PostAsJsonAsync(SoccerXConstants.User_Register, request);
            var responseContent = await response.Content.ReadAsStringAsync();
            Thread.Sleep(TimeSpan.FromSeconds(25));

            // 🔍 DEBUG LOG
            Console.WriteLine($"[DEBUG] StatusCode: {response.StatusCode}");
            Console.WriteLine($"[DEBUG] ResponseContent: {responseContent}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"but received {response.StatusCode} with content: {responseContent}");
            responseContent.Should().Contain("true", $"but received content: {responseContent}");
        }

        //[Fact]
        public async Task Register_ShouldReturnBadRequest_WhenMissingFields()
        {
            // Arrange
            var request = new UserCreateDto
            {
                Email = "", // eksik alan
                Password = "",
                Username = "",
                Address = "",
                Name = "Test",
                Phonenumber = "",
                Surname = "",
            };

            // Act
            var response = await _client.PostAsJsonAsync(SoccerXConstants.User_Register_Admin, request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
