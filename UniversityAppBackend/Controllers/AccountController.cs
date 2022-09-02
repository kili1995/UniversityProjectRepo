using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Api.Helpers;
using University.Api.Models;
using University.BusinessLogic.Users;
using University.DataAccess.Context;
using University.DataAccess.Models.DataModels;

namespace University.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTSettings _jWTSettings;
        private readonly UniversityDBContext _universityDBContext;
        private readonly IUserService _userService;
        
        public AccountController(
            JWTSettings jwtSettings,
            UniversityDBContext universityDBContext,
            IUserService userService
        )
        {
            _jWTSettings = jwtSettings;
            _universityDBContext = universityDBContext;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                var user = await _userService.GetUserByName(userLogin.UserName);
                if (user == null)
                {
                    return BadRequest("No se encontró el usuario.");
                }
                var token = JWTHelpers.GetTokenKey(new UserToken()
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
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
