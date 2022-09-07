using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;
using University.Api.Controllers.Account.Models;
using University.Api.Helpers;
using University.Api.Models;
using University.BusinessLogic.Users;
using University.DataAccess.Context;
using University.DataAccess.Models.DataModels;

namespace University.Api.Controllers.Account
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTSettings _jWTSettings;
        private readonly UniversityDBContext _universityDBContext;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<AccountController> _accountLocalizer;

        public AccountController(
            JWTSettings jwtSettings,
            UniversityDBContext universityDBContext,
            IUserService userService,
            IStringLocalizer<AccountController> accountLocalizer
        )
        {
            _jWTSettings = jwtSettings;
            _universityDBContext = universityDBContext;
            _userService = userService;
            _accountLocalizer = accountLocalizer;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                bool validUser = await _userService.ValidateUserByNameAndPassword(userLogin.UserName, userLogin.Password);
                if (!validUser)
                {
                    return BadRequest(_accountLocalizer["InvalidLogin"].Value);
                }
                var user = await _userService.GetUserByName(userLogin.UserName);
                var token = JWTHelpers.GetTokenKey(new UserToken()
                {
                    EmailId = user.Email,
                    UserName = user.Username,
                    Id = user.Id,
                    GuidId = Guid.NewGuid()
                }, _jWTSettings);
                return Ok(new { Message = _accountLocalizer["SuccesfullyLogin"].Value, Token = token });
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

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(RegisterUserModelUI registerData)
        {
            if (string.IsNullOrWhiteSpace(registerData.Username))
            {
                return BadRequest("Ingrese un nombre de usuario.");
            }
            if (string.IsNullOrWhiteSpace(registerData.Password))
            {
                return BadRequest("Ingrese una contraseña.");
            }
            if (string.IsNullOrWhiteSpace(registerData.Email))
            {
                return BadRequest("Ingrese un email.");
            }
            var userByName = await _userService.GetUserByName(registerData.Username);
            if(userByName != null)
            {
                return BadRequest("Nombre de usuario en uso. Intente con otro.");
            }
            var usersByEmail = await _userService.GetUsersByEmail(registerData.Email);
            if (usersByEmail.Any())
            {
                return BadRequest("Email en uso. Intente con otro.");
            }
            var response = await _userService.CreateUser(new BusinessLogic.Users.Dtos.CreateUserDto()
            {
                Email = registerData.Email,
                Name = registerData.Username,
                Password = registerData.Password
            });
            if(response != null)
            {
                return Ok(new
                {
                    Message = "Usuario creado con éxito",
                    User = response
                });
            }
            else
            {
                return BadRequest("Error al crear el usuario.");
            }
        }
    }
}
