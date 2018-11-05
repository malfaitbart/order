using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Order.API.Controllers.Orders;
using Order.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Order.API.Tests
{
	public class OrderIntegrationTest
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public OrderIntegrationTest()
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
		public async Task GivenAnAPI_WhenCallingAllOrders_ThenGetListOfOrders()
		{
			//Given
			var username = Database.Users[1].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var response = await _client.GetAsync("/api/orders");
			var responseString = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<OrderDTO>>(responseString);

			//Then
			Assert.True(response.IsSuccessStatusCode);

		}
		[Fact]
		public async Task GivenAnAPI_WhenCallingAllOrdersWithWrongUser_ThenGet403()
		{
			//Given
			var username = Database.Users[0].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var response = await _client.GetAsync("/api/orders");
			var responseString = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<OrderDTO>>(responseString);

			//Then
			Assert.Equal("Forbidden",response.StatusCode.ToString());

		}
		[Fact]
		public async Task GivenAnAPI_WhenPostingItemData_ThenItemIsCreatedOnBackendAsync()
		{
			//Given
			var dtocreate1 = new OrderDTO_Create(0,1);
			var dtocreate2 = new OrderDTO_Create(1, 1);
			List<OrderDTO_Create> orderDTO_Creates = new List<OrderDTO_Create>() { dtocreate1, dtocreate2 };

			var username = Database.Users[0].Email;
			var password = "";

			_client.DefaultRequestHeaders.Authorization = CreateBasicHeader(username, password);

			//When
			var content = JsonConvert.SerializeObject(orderDTO_Creates);
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("/api/orders", stringContent);

			//Then
			Assert.True(response.IsSuccessStatusCode);
		}


	}
}
