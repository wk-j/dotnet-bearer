using Microsoft.AspNetCore.Mvc;

namespace MyWeb.Controllers.Accounts {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase {
        [HttpPost]
        public ActionResult<AccountInfo> GetToken(TokenRequest request) {
            var accessToken = TokenUtils.GenerateToken(request.User, 60);

            return new AccountInfo {
                AccessToken = accessToken
            };
        }
    }
}