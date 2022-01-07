using ApiDisneyPruebaCore5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiDisneyPruebaCore5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public IEmailSender EmailSender { get; set; }
        public AuthController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            EmailSender = emailSender;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<RespuestaAutenticacion>> CreateUser([FromBody] UserInfo model)
        {
          
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    try
                    {
                        var subject = "Registro exitoso";
                        var body = "Se ha registrado como nuevo usuario, muchas gracias.";
                        await EmailSender.SendEmailAsync(model.Email, subject, body);
                    }
                    catch (Exception ex)
                    {

                       
                    }
                 

                    return BuildToken(model);
                }
                else
                {
                    
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                   
                    return BadRequest("Login Incorrecto.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private RespuestaAutenticacion BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("Alkemy", "Ejemplo de claim"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave_Secreta"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new RespuestaAutenticacion
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                FechaExpiracion = expiration
            };

        }


    }
}
