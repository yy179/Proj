using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Project.DataAccess;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Project.Tests.ControllerTests
{
    public class VolunteerControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;
        public VolunteerControllerTest(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _output = output;
            _factory = factory;
        }

        private HttpClient GetAuthenticatedClient(bool isAdmin = false)
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProjectDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ProjectDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.Volunteers.Add(new VolunteerEntity { Id = 1, FirstName = "John", LastName = "Doe", City = "Kharkiv", Bio = "Hello!", DateOfBirth = new DateTime(1990, 5, 15) });
                    context.Volunteers.Add(new VolunteerEntity { Id = 2, FirstName = "Jane", LastName = "Doe", City = "Kyiv", Bio = "Hi!", DateOfBirth = new DateTime(1985, 8, 25) });

                    var organization1 = new OrganizationEntity { Id = 1, Name = "Org1", City = "Kharkiv", Description = "ASd" };
                    var organization2 = new OrganizationEntity { Id = 2, Name = "Org2", City = "Kharkiv", Description = "ASdt" };
                    context.Organizations.AddRange(organization1, organization2);
                    context.SaveChanges();

                    context.VolunteerOrganizations.Add(new VolunteerOrganizationEntity { VolunteerId = 2, OrganizationId = 1 });
                    context.VolunteerOrganizations.Add(new VolunteerOrganizationEntity { VolunteerId = 2, OrganizationId = 2 });

                    var request1 = new RequestEntity { Id = 1, Title = "Req1", Description = "Need help", EndDate = new DateTime(2024, 12, 11), Importance = 1, IsActive = true, TakenByVolunteerId = 2 };
                    var request2 = new RequestEntity { Id = 2, Title = "Req2", Description = "Looking for assistance", EndDate = new DateTime(2024, 12, 12), Importance = 2, IsActive = true, TakenByVolunteerId = 2 };
                    context.Requests.AddRange(request1, request2);
                    context.SaveChanges();
                });
            }).CreateClient();

            var role = isAdmin ? "admin" : "user";
            var token = GenerateJwtToken(role);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        private string GenerateJwtToken(string role)
        {
            var claims = new[] {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "testuser"),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jdRJV3Cm6ytS7ecaXZAfU2WhnvsgwKF4xzbLHpPuN8qkT"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AuthApi",
                audience: "AuthClient",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        [Fact]
        public async Task Index_ReturnsSuccessAndCorrectView()
        {
            var client = GetAuthenticatedClient(isAdmin: false);
            var response = await client.GetAsync("/Volunteer/Index");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Список волонтеров", responseString);
            Assert.Contains("John", responseString);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenVolunteerDoesNotExist()
        {
            var client = GetAuthenticatedClient(isAdmin: true);
            var response = await client.GetAsync("/Volunteer/Details/3");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Volunteer with ID 3 not found.", responseString);
        }

        [Fact]
        public async Task Details_ReturnsVolunteerWithOrganizationsAndRequests_WhenVolunteerExists()
        {
            var client = GetAuthenticatedClient(isAdmin: true);
            var response = await client.GetAsync("/Volunteer/Details/2");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseString);
            Assert.Contains("Jane Doe", responseString);
            Assert.Contains("Org1", responseString);
            Assert.Contains("Org2", responseString);
            Assert.Contains("Req1", responseString);
            Assert.Contains("Req2", responseString);
        }
    }
}

