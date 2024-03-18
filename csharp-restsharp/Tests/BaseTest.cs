using RestSharp;

namespace csharp_restsharp.Tests
{
    public class BaseTest
    {

        protected static IRestClient _client;

        [OneTimeSetUp]
        public static void InitalizeRestClient() =>
          _client = new RestClient("https://petstore.swagger.io/v2/");


        [OneTimeTearDown]
        public static void Cleanup()
        {
            _client?.Dispose();
        }

    }
}
