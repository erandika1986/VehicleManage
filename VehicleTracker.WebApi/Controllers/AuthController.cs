using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VehicleTracker.Business;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Users;

namespace VehicleTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;

        public AuthController(ILogger<AuthController> logger, IConfiguration config, IUserService userService)
        {
            this._logger = logger;
            this._config = config;
            this._userService = userService;
        }

        [HttpPost]
        [Route("registerNewUser")]
        [AllowAnonymous]
        public IActionResult RegisterNewUser()
        {
            this._userService.AddNewUser();

            return Ok(true);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (model == null)
            {
                return Unauthorized(new { ErrorMessage = "Login failed.Please enter your password and username." });
            }

            var user = _userService.GetUserByUsername(model.Username);

            if (user == null)
            {
                return Unauthorized(new { ErrorMessage = "Login failed.Invalid username has entered." });
            }
            else
            {
                var passwordHash = CustomPasswordHasher.GenerateHash(model.Password);

                if (BCrypt.Net.BCrypt.Verify(user.Password, model.Password))
                {
                    var test = _config["Tokens:Key"];
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(comapny.SecretKey.ToString()));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    string userRole = string.Empty;
                    string roles = string.Join(",", user.UserRoles.Select(t => t.Role.Name).ToList());

                    var now = DateTime.UtcNow;
                    DateTime nowDate = DateTime.UtcNow;
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Aud,"admin"),
                        new Claim(ClaimTypes.Role,roles)
                    };


                    var tokenOptions = new JwtSecurityToken(
                        issuer: _config["Tokens:Issuer"],
                        claims: claims,
                        notBefore: nowDate,
                        expires: nowDate.AddDays(100),
                        signingCredentials: signinCredentials

                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new
                    {
                        Token = tokenString,
                        FirstName = user.FirstName,
                        Email = user.Email,
                        ProfilePic = "",
                        Role = user.UserRoles.FirstOrDefault().Role.Name
                    });
                }
                else
                {
                    return Unauthorized(new { ErrorMessage = "Login failed.Invalid password has entered." });
                }
            }
        }
    }

    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
