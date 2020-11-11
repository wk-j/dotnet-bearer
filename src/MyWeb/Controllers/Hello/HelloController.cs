using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWeb.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "X")]
    public class HelloController : ControllerBase {

        private readonly ILogger<HelloController> _logger;

        public HelloController(ILogger<HelloController> logger) {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Hi() {
            var user = this.User.Identity.Name;

            _logger.LogInformation("User {0}", user);


            return Ok("Hello");
        }
    }
}