using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace csharp_restsharp.Tests
{
    public class PetTest : BaseTest
    {

        [Test]
        public async Task createPet()
        {
            var request = new RestRequest("/pet", Method.Post);
            var response = await _client.ExecuteAsync(request);
        }

        [Test]
        public async Task findByStatus()
        {
            var request = new RestRequest("pet/findByStatus", Method.Get)
                .AddQueryParameter("status", "available");
            var response = await _client.ExecuteAsync(request);
            var fullUrl = _client.BuildUri(request).ToString();
            Console.WriteLine($"Request URL: {fullUrl}");
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
