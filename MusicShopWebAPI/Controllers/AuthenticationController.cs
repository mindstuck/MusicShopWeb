using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicShopWebAPI.Data;
using MusicShopWebAPI.Models;
using MusicShopWebAPI.Services.Authentication;
using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace MusicShopWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly MusicShopContext _context;

        public AuthenticationController(MusicShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var nameIdentifier = this.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return nameIdentifier?.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<string> Post(AuthenticationRequest authRequest, [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            // 1. Проверяем данные пользователя из запроса.
            if (!_context.Users.Any(u => u.Email == authRequest.Email && u.Password == authRequest.Password))
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            // 2. Создаем утверждения для токена.
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, authRequest.Email)
            };

            // 3. Генерируем JWT.
            var token = new JwtSecurityToken(
                issuer: "MusicShopWebAPI",
                audience: "MusicShopWebAPIClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
