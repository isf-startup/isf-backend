using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Core;
using ISF.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ISFDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public TestController(ILogger<TestController> logger, ISFDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginTest([FromBody]UserData data)     
        {
            var token = await _tokenService.GenerateToken(data);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("append")]
        public async Task<IActionResult> Append()
        {

                List<string> sentencesTR = new List<string>
                    {
                    "Bugün harika bir gün olacak.",
                    "Her şey yolunda gidecek.",
                    "Kendine güven, başaracaksın.",
                    "Gülümsemek sana çok yakışıyor.",
                    "Hayat güzelliklerle dolu.",
                    "Her yeni gün yeni umutlar getirir.",
                    "İçindeki güçle her engeli aşabilirsin.",
                    "Sevgiyle dolu bir gün seni bekliyor.",
                    "Başarı seninle olacak.",
                    "Mutluluk seninle.",

                    };
                List<string> sentencesEN = new List<string>
                    {
                    "Today will be a wonderful day.",
                    "Everything will go smoothly.",
                    "Believe in yourself, you will succeed.",
                    "Your smile looks great on you.",
                    "Life is full of beauty.",
                    "Every new day brings new hopes.",
                    "With your inner strength, you can overcome any obstacle.",
                    "A day full of love awaits you.",
                    "Success will be with you.",
                    "Happiness is with you.",

                    };
                
            _dbContext.Sentences.AddRange(sentencesTR.Select(text => new Sentence{ WordTR = text}));
            _dbContext.Sentences.AddRange(sentencesEN.Select(text => new Sentence{ WordEN = text}));
            await _dbContext.SaveChangesAsync();
            

            var result = new { Message = "Append operation completed successfully." };
            return Ok(result);
        }
        
        [HttpGet("test")]
        public async Task<IActionResult> TestApi()
        {
            var sentences = await _dbContext.Sentences.ToListAsync();
            return Ok($"Test Affirmation Words: {sentences}");
        }
    }

}