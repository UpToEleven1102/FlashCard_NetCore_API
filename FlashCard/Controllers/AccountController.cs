using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlashCard.Models;
using FlashCard.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FlashCard.Controllers
{
    public class LoginCredentials {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        readonly UserManager<CustomIdentityUser> userManager;
        readonly SignInManager<CustomIdentityUser> signInManager;
        readonly IConfiguration configuration;

        public AccountController(UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager, IConfiguration configuration) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount([FromBody]RegisterCredentials credentials) {
            if (!credentials.Password.Equals(credentials.RePassword)) {
                return BadRequest(new List<IdentityError>() { new IdentityError() { Description= "Passwords are not the same!"} });
            }

            var user = new CustomIdentityUser() { First = credentials.First, Last = credentials.Last, UserName = credentials.Email, Email = credentials.Email };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if(!result.Succeeded) {
                return BadRequest(result.Errors);
            }

            await signInManager.SignInAsync(user, false);

            return Ok(CreateToken(user));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials) {
            
            var result = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);
            if (!result.Succeeded) {
                return BadRequest(new List<IdentityError>() { new IdentityError() { Description = "Login failed" } });
            }
            var user = await userManager.FindByEmailAsync(credentials.Email);
            return Ok(CreateToken(user));
        }

        public string CreateToken(CustomIdentityUser user) {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Authentication:SecretKey").Value));
            var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var jwt = new JwtSecurityToken(signingCredentials: signingCredential, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}