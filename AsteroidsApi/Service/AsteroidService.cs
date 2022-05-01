using Newtonsoft.Json;

namespace AsteroidsApi
{
    public class AsteroidService : IAsteroidsService
    {
        public async Task<string> GetInfoFromApi(string planet)
        {
            AsteroidsInfo info;
            string responseJSOM = String.Empty;

            var dto = new List<AsteroidsInfoDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getUrl()))
                {
                    //La api solo devuelve datos del planeta tierra
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    info = JsonConvert.DeserializeObject<AsteroidsInfo>(apiResponse);
                }
            }
            if (info != null)
            {
                int cont = 0;
                var result = info.near_earth_objects?.Values.ToList();
                result?.ForEach(x =>
                {
                    var item = x.ElementAt(cont);
                    if (item.is_potentially_hazardous_asteroid)
                    {
                        //the api returns only one irwm for close_aproach_data
                        if (item.close_approach_data !=null && item.close_approach_data.Count > 0) { 
                            dto.Add(new AsteroidsInfoDto
                            {
                                Nombre = item.name ?? String.Empty,
                                Diametro = (item.estimated_diameter?.kilometers?.estimated_diameter_max ?? 0 + item.estimated_diameter?.kilometers?.estimated_diameter_min ?? 0) / 2,
                                Fecha = item.close_approach_data.ElementAt(0)?.close_approach_date ?? String.Empty,
                                Velocidad = item.close_approach_data.ElementAt(0).relative_velocity?.kilometers_per_hour ?? String.Empty,
                                Planeta = item.close_approach_data.ElementAt(0).orbiting_body ?? String.Empty
                            });
                        }
                        cont++;
                }
                });


                return  JsonConvert.SerializeObject(dto.OrderByDescending(x => x.Diametro).Take(3).ToList());

            }


            return responseJSOM;
        }

        private String getUrl()
        {
             var datefin = DateTime.Today.ToString("yyyy-MM-dd");
            var dateini = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");

            return  $"https://api.nasa.gov/neo/rest/v1/feed?start_date={dateini}&end_date={datefin}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";

        }
    }
}


