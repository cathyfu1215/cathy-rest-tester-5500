using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace c_sharp_server_cathy.Tests
{
    public class UserApiTests : IClassFixture<WebApplicationFactory<c_sharp_server_cathy.Startup>>
    {
        private readonly HttpClient _client;

        public UserApiTests(WebApplicationFactory<c_sharp_server_cathy.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test_GetAllUsers()
        {
            var response = await _client.GetAsync("/users");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("[]", responseString);
        }

        [Fact]
        public async Task Test_AddUser()
        {
            var newUser = new { name = "John Doe" };
            var content = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/users", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(responseString);
            Assert.NotNull(user);
            Assert.Equal(1, user!.Id);
            Assert.Equal("John Doe", user.Name);
            Assert.Equal(0, user.HoursWorked);
        }

        [Fact]
        public async Task Test_GetUserById()
        {
            await Test_AddUser(); // Ensure there's at least one user
            var response = await _client.GetAsync("/users/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(responseString);
            Assert.NotNull(user);
            Assert.Equal(1, user!.Id);
            Assert.Equal("John Doe", user.Name);
            Assert.Equal(0, user.HoursWorked);
        }

        [Fact]
        public async Task Test_GetNonExistentUser()
        {
            var response = await _client.GetAsync("/users/999");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("User not found", responseString);
        }

        [Fact]
        public async Task Test_UpdateUser()
        {
            await Test_AddUser(); // Ensure there's at least one user
            var updatedUser = new { name = "Jane Doe" };
            var content = new StringContent(JsonSerializer.Serialize(updatedUser), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/users/1", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(responseString);
            Assert.NotNull(user);
            Assert.Equal(1, user!.Id);
            Assert.Equal("Jane Doe", user.Name);
        }

        [Fact]
        public async Task Test_UpdateUserHours()
        {
            await Test_AddUser(); // Ensure there's at least one user
            var hoursUpdate = new { HoursToAdd = 5 };
            var content = new StringContent(JsonSerializer.Serialize(hoursUpdate), Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync("/users/1", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(responseString);
            Assert.NotNull(user);
            Assert.Equal(1, user!.Id);
            Assert.Equal(5, user.HoursWorked);
        }

        [Fact]
        public async Task Test_DeleteUser()
        {
            await Test_AddUser(); // Ensure there's at least one user
            var response = await _client.DeleteAsync("/users/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(responseString);
            Assert.NotNull(user);
            Assert.Equal(1, user!.Id);
        }

        [Fact]
        public async Task Test_DeleteAllUsers()
        {
            await Test_AddUser(); // Ensure there's at least one user
            var response = await _client.DeleteAsync("/users");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("[]", responseString);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Make Name nullable
        public int HoursWorked { get; set; }
    }
}
