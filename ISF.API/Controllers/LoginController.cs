using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Core;
using ISF.Data.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ISF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IPlatformTokenQueryService _platformTokenQueryService;
        public LoginController(IPlatformTokenQueryService platformTokenQueryService)
        {
            _platformTokenQueryService = platformTokenQueryService;
        }
        [HttpPost("googlelogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDTO)
        {
            if (googleLoginDTO == null)
            {
                return BadRequest("Google login data is required");
            }

            var token = await _platformTokenQueryService.GoogleTokenValidation(googleLoginDTO);
            return Ok(token);
        }
        [HttpPost("applelogin")]
        public async Task<IActionResult> AppleLogin([FromBody] AppleLoginDTO appleLoginDTO)
        {
            if (appleLoginDTO == null)
            {
                return BadRequest("Apple login data is required");
            }

            var token = await _platformTokenQueryService.AppleTokenValidation(appleLoginDTO);
            return Ok(token);
        }
    }
}