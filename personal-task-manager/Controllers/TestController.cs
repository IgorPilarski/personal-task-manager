using Microsoft.AspNetCore.Mvc;

namespace PersonalTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API działa z kontrolerami");
        }
    }
}