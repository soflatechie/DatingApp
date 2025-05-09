using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
   
    public class AccountController(
            UserManager<AppUser> userManager, 
            ITokenService tokenService, 
            IMapper mapper
        ) : BaseApiController
    {
        [HttpPost("register")] //accoun/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
        {
            if( await UserExists(registerDto.Username) )
                return BadRequest("Username is taken");         

            var user = mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Username.ToLower();                   

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return new UserDto{
                Username = user.UserName,
                Token = await tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender =  user.Gender
            }; 

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(n=> 
                    n.NormalizedUserName == loginDto.Username.ToUpper());

            if(user == null || user.UserName == null)
                return Unauthorized("Invalid username or password!");     

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!result)
                return Unauthorized();      
            
           return new UserDto{
                Username = user.UserName,
                Token = await tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x=> x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender =  user.Gender
            };
        }

        private async Task<bool> UserExists(string userName)
        {
            return await userManager.Users.AnyAsync(n=>
                n.NormalizedUserName == userName.ToUpper());
        }

    }
}
