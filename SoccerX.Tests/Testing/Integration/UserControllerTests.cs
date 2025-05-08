using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SoccerX.Application.Exceptions;
using SoccerX.Common.Extensions;
using SoccerX.DTO.Dto.User;
using SoccerX.DTO.Responses;
using SoccerX.Tests.Base.IntegrationFactory;
using SoccerX.Tests.Base.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
        }
        #endregion

        #region Public Method
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
            var response = await _client.PostAsJsonAsync(TestApiControllerEndPoint.User_Register, request);
            var responseContent = await response.Content.ReadAsStringAsync();

            // 🔍 DEBUG LOG
            Console.WriteLine($"[DEBUG] StatusCode: {response.StatusCode}");
            Console.WriteLine($"[DEBUG] ResponseContent: {responseContent}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"but received {response.StatusCode} with content: {responseContent}");
            responseContent.Should().Contain("true", $"but received content: {responseContent}");
        }

        [Fact]
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
            var response = await _client.PostAsJsonAsync(TestApiControllerEndPoint.User_Register_Admin, request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
