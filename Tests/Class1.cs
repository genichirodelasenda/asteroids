
using System.Net.Http;
using Xunit;

namespace Tests
{
   
    public class Class1
    {
        [Fact]
        public  void TestApiActive()
        {
            HttpResponseMessage response;
            int expectedHttpCode = 200;
            var dateini = DateTime.Now.ToString("yyyy-MM-dd");
            var datefin = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

            using (var httpClient = new HttpClient())
            {

                string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={dateini}&end_date={datefin}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
                 response =  httpClient.GetAsync(url).Result ;

            }

            Assert.Equal(expectedHttpCode, (double)response.StatusCode);

        }
    }
}