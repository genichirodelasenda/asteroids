using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;


namespace AsteroidsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsteroidController : ControllerBase
    {
        private IAsteroidsService _asteroidsService;

        public AsteroidController(IAsteroidsService asteroidsService)
        {
            _asteroidsService = asteroidsService;
        }
  
        [HttpGet]
        public async Task<ActionResult<List<AsteroidsInfoDto>>> GetInfo(string? planet)
        {

            try
            {
                if (planet == null) return BadRequest("Planet is mandatory");

                var result = await _asteroidsService.GetInfoFromApi(planet);

                if (String.IsNullOrEmpty(result)) return NoContent();

                return Ok(result);
            } catch (Exception ex)
            {
                return  StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
       

        }
    }
            

    }
