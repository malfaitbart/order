using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Order.API.Controllers.Users;
using Order.Domain.Users;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Order.API.Tests
{
	public class UserIntegrationTest
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public UserIntegrationTest()
		{
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			_client = _server.CreateClient();
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}
		[Fact]
		public async Task GivenAnAPI_WhenCallingAllUsers_ThenGetListOfUsers()
		{
			//Given

			//When
			var response = await _client.GetAsync("/api/users");
			var responseString = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<UserDTO_GetAll>>(responseString);

			//Then
			Assert.True(response.IsSuccessStatusCode);

		}

		[Fact]
		public async Task GivenAnAPI_WhenPostingUserData_ThenUserIsCreatedOnBackendAsync()
		{
			//Given
			var user = new User("test", "test", "test@test.com", "test", "test", "test", "teste", "teste");

			//When
			var content = JsonConvert.SerializeObject(user);
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("/api/users", stringContent);

			//Then
			Assert.True(response.IsSuccessStatusCode);
		}
	}
}
