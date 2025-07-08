using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TestController : Controller
    {

        [HttpGet("configuration")]
        public string Configuration(IConfiguration configuration)
        {
            return configuration.GetValue<string>("secreto") ?? "no fue encontrado el valor";
        }

    }
}
