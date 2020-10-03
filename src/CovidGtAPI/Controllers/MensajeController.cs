using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CovidGtAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MensajeController
    {

        [HttpGet]
        public async Task<string> GetMensaje() {
            return "Hola que tal";
        }
        
    }
}