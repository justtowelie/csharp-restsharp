using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace csharp_restsharp.Tests
{
    public class PetStore : BaseTest
    {

        [Test]
        public async Task GetInventory()
        {
            var request = new RestRequest("store/inventory", Method.Get);
            var response = await _client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonResponse = JObject.Parse(response.Content);
            Assert.IsNotNull(jsonResponse["available"], "available item is null");
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/GetInventory.json"));
            Assert.True(jsonResponse.IsValid(jsonSchema));
        }

        [Test]
        public async Task GetOrderById()
        {
            var request = new RestRequest("store/order/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonResponse = JToken.Parse(response.Content);
            Assert.AreEqual("1", jsonResponse.SelectToken("id").ToString());
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/GetOrderById.json"));
            Assert.True(jsonResponse.IsValid(jsonSchema));
        }

        [Test]
        public async Task GetOrderWithInvalidId()
        {
            var request = new RestRequest("store/order/555", Method.Get);
            var response = await _client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var jsonResponse = JToken.Parse(response.Content); ;
            Assert.AreEqual("Order not found", jsonResponse.SelectToken("message").ToString());
        }
    }
}
