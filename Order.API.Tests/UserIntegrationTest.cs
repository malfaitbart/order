using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Order.API.Controllers.Users;
using Order.Data;
using Order.Domain.Users;
using System;
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

		private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(username + ":" + password);
			return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
		}


		[Fact]
		public async Task GivenAnAPI_WhenCallingAllUsers_ThenGetListOfUsers()
		{
			//Given
			Database.Users[1].SetAdmin();
			var username = Database.Users[1].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);
			//When
			var response = await _client.GetAsync("/api/users");
			var responseString = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<UserDTO>>(responseString);

			//Then
			Assert.True(response.IsSuccessStatusCode);

		}

		[Fact]
		public async Task GivenAnAPI_WhenPostingUserData_ThenUserIsCreatedOnBackendAsync()
		{
			//Given
			var user = new UserDTO_Register("test", "test", "test@test.com", "test", new AddressDTO("test", "test", "teste", "teste"));

			//When
			var content = JsonConvert.SerializeObject(user);
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("/api/users", stringContent);

			//Then
			Assert.True(response.IsSuccessStatusCode);
		}

		[Fact]
		public async Task GivenAnAPI_WhenGettingUserByExistingID_ThenUserIsReturned()
		{
			//Given
			Database.Users[1].SetAdmin();
			var username = Database.Users[1].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var response = await _client.GetAsync("/api/users/0");
			var responseString = await response.Content.ReadAsStringAsync();
			var user = JsonConvert.DeserializeObject<UserDTO>(responseString);

			//Then
			Assert.Equal(0, user.ID);
		}

		[Fact]
		public async Task GivenAnAPI_WhenGettingUserByNonExistingID_ThenNotFoundIsReturned()
		{
			//Given
			Database.Users[1].SetAdmin();
			var username = Database.Users[1].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var response = await _client.GetAsync("/api/users/-1");
			var responseString = await response.Content.ReadAsStringAsync();

			//Then
			Assert.Equal("NotFound", response.StatusCode.ToString());
		}
	}
}
