using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using RegisterTime.Models;
using RegisterTime.Entities;
using RegisterTime.Services;

namespace TimeRegistration.Controllers
{
    //[Route("[controller]/[action]")]
    [Route("api/freelancer/[action]")]
    public class AccountController : Controller
    {
        private IWorkRepository _WorkRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IWorkRepository workRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _WorkRepository = workRepository;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var password = await _userManager.CheckPasswordAsync(user, model.Password);

                if (password)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    return await GenerateJwtToken(model.Email, appUser);
                }

                return BadRequest("Login Failed:: \nPlease try again later");
            }
            return BadRequest("Invalid Login..!\n" +
                "Please fill out and resend Login model variables\n" +
                "'Email': 'email', 'Password': 'Password123!'");            
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,//model.GetUserName(),
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    var freelancer = new Freelancer
                    {
                        Id = new Guid(user.Id),
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };
                    _WorkRepository.AddFreelancer(freelancer);
                    _WorkRepository.Save();
                    return await GenerateJwtToken(model.Email, user);
                }

                return result.Errors;
            }
            return BadRequest("Invalid registration..!\n" +
                "Please fill out and resend registration model variables\n" +
                "FirstName': 'Name', 'LastName': 'Name', 'Email': 'email', 'Password': 'Password123!'");
        }

        private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}