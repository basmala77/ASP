using CRUD_Operations.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryPatternWithUOW.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUD_Operations.Controllers
{
    [ApiController]
    public class UsersController(JwtOptions options,ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<string> AuthenticateUser(User2 user)
        {
            var users = dbContext.users3s.FirstOrDefault(x=> x.Name == user.UserName && 
            x.Password == user.Password);
            if (users == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new (ClaimTypes.Name,user.UserName),
                    new(ClaimTypes.Role,"SuperUser")
                })
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return Ok(accessToken);
        }
    }
}

