using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Api.Helpers;
using University.Api.Models;
using University.DataAccess.Models.DataModels;

namespace University.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTSettings _jWTSettings;
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "jorgecapello1995@gmail.com",
                CreatedBy = "Jorge",
                CreationDate = DateTime.Now,
                Deleted = false,
                DeletedBy = "",
                DeletedDate = null,
                LastName = "Capello",
                Name = "Jorge M.",
                Password = "kiligan",
                UpdatedBy = "",
                UpdatedDate = null
            }
        };
        public AccountController(
            JWTSettings jwtSettings    
        )
        {
            _jWTSettings = jwtSettings;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                var token = new UserToken();
                if (!Logins.Any(x => x.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    return BadRequest("No se encontró el usuario.");
                }
                var user = Logins.FirstOrDefault(x => x.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                token = JWTHelpers.GetTokenKey(new UserToken()
                {
                    EmailId = user.Email,
                    UserName = user.Name,
                    Id = user.Id,
                    GuidId = Guid.NewGuid()                    
                }, _jWTSettings);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest("Error al obtener el token.");
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(Logins);
        }
    }
}
