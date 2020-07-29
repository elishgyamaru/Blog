using Blog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Blog.Api.Dtos;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Blog.Api.Helpers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _siginManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<JwtConfigurations> _jwtConfig;

        public AccountController(
            SignInManager<ApplicationUser> siginManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IOptionsSnapshot<JwtConfigurations> jwtConfig
            )
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._jwtConfig = jwtConfig;
            this._siginManager = siginManager;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(registrationDto);
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserDetailsDto>(user);
                return CreatedAtRoute("GetUser", new { Id = user.Id }, userToReturn);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _siginManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var tokenVal = GenerateToken(user, roles);
                return Ok(new { token = tokenVal,user=_mapper.Map<UserDetailsDto>(user) });
            }
            return Unauthorized();
        }

        private string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role.ToString(), r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Key));
            var siginingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresIn = DateTime.Now.AddDays(_jwtConfig.Value.ExpirationInDays);

            var token = new JwtSecurityToken(
                _jwtConfig.Value.Issuer,
                _jwtConfig.Value.Audience,
                claims,
                notBefore: null,
                expires: expiresIn,
                siginingCreds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}