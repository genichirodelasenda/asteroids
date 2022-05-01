using Moq;
using System;
using System.Net.Http;
using Xunit;
using AsteroidsApi;

namespace TestProject1
{
    public class AsteroidsTests
    {
        [Fact]
        public async void TestsIsApiSiteOnline()
        {
            int expectedstatuscode = 200;
            var dateini = DateTime.Now.ToString("yyyy-MM-dd");
            var datefin = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");



            using (var httpClient = new HttpClient())
            {

                string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={dateini}&end_date={datefin}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
                var response = await httpClient.GetAsync(url);

                Assert.Equal(expectedstatuscode, (int)response.StatusCode);
            }



        }
        [Fact]
        public async  void TestEndpointValid ()
        {

            var asteroidsMock = new Mock<IAsteroidsService>();
            string expectedResult = @"[{""Nombre"":523732(2014 PG51),""Diametro"":0.2172751123,""Velocidad"":51000.7027147888,""Fecha"":""2022 - 05 - 08"",""Planeta"":""Earth""}]";
            asteroidsMock.Setup(x => x.GetInfoFromApi("Earth")).ReturnsAsync(()=>expectedResult) ;
            var obj = asteroidsMock.Object;
            var result = obj.GetInfoFromApi("Earth").Result;

            AsteroidService asteroidService = new AsteroidService();
            string jsonresult= await asteroidService.GetInfoFromApi("Earth");
            Assert.Equal(expectedResult, jsonresult);


        }
    }
}