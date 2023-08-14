using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSAPI.JWTHelpers;
using RMSAPI.Models;

namespace RMSAPI.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IJWTManagerRepository jWTManager) {
            this._jWTManager = jWTManager;
        }
        [HttpGet]
        public List<string> Get() {
            var users = new List<string>{
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };
            return users;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserModel usersdata) {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null) {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
