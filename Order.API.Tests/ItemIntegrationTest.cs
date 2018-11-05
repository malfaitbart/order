using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Order.API.Controllers.Items;
using Order.Data;
using Order.Domain.Items;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Order.API.Tests
{
	public class ItemIntegrationTest
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public ItemIntegrationTest()
		{
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			_client = _server.CreateClient();
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
		{
			byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
			return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
		}


		[Fact]
		public async Task GivenAnAPI_WhenCallingAllItems_ThenGetListOfItems()
		{
			//Given
			var username = Database.Users[0].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var response = await _client.GetAsync("/api/items");
			var responseString = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<ItemDTO>>(responseString);

			//Then
			Assert.True(response.IsSuccessStatusCode);

		}

		[Fact]
		public async Task GivenAnAPI_WhenPostingItemData_ThenItemIsCreatedOnBackendAsync()
		{
			//Given
			var item = new Item("test", "test", 0.0, 0, 1);
			Database.Users[1].SetAdmin();
			var username = Database.Users[1].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var content = JsonConvert.SerializeObject(item);
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("/api/items", stringContent);

			//Then
			Assert.True(response.IsSuccessStatusCode);
		}

		[Fact]
		public async Task GivenAnAPI_WhenGettingItemByExistingID_ThenItemIsReturned()
		{
			//Given

			//When
			var response = await _client.GetAsync("/api/items/0");
			var responseString = await response.Content.ReadAsStringAsync();
			var item = JsonConvert.DeserializeObject<ItemDTO>(responseString);

			//Then
			Assert.Equal(0, item.ID);
		}

		[Fact]
		public async Task GivenAnAPI_WhenGettingItemByNonExistingID_ThenNotFoundIsReturned()
		{
			//Given

			//When
			var response = await _client.GetAsync("/api/items/-1");
			var responseString = await response.Content.ReadAsStringAsync();

			//Then
			Assert.Equal("NotFound", response.StatusCode.ToString());
		}

	}
}
