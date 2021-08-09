using EventuallyAPI.Core.DTOs;
using EventuallyAPI.Core.Entities;
using EventuallyAPI.Data;
using EventuallyAPI.Infraestructure.Utils;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventuallyAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtProvider _jwtProvider;
        private readonly ApplicationDBContext _applicationDBContext;

        public AccountsController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtProvider jwtProvider,
            ApplicationDBContext applicationDBContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _applicationDBContext = applicationDBContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Jwt>> Register(RegisterDTO registerDTO)
        {
            var user = new User()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };

            using var transaction =await _applicationDBContext.Database.BeginTransactionAsync();

            try
            {

                var creationResult = await
                   _userManager.CreateAsync(user, registerDTO.Password);


                if (creationResult.Succeeded)
                {
                    var areas = registerDTO.Areas.Select(
                        areaId => new UserArea
                        {
                            UserId = user.Id,
                            AreaId = areaId
                        });

                    _applicationDBContext.AddRange(areas);

                    await _applicationDBContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return GenerateJwt(user);
                }
            }
            catch(Exception)
            {
                transaction.Rollback();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Jwt>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user != null)
            {
                var sigInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, true);

                if (sigInResult.Succeeded)
                {
                    return GenerateJwt(user);
                }
            }

            ModelState.AddModelError("Invalid-credentials","Credenciales invalidas");

            return BadRequest(ModelState);

        }

        private Jwt GenerateJwt(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email)
            };

            return _jwtProvider.GetJwt(claims);
        }
    }
}