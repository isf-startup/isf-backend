using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace ISF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppendTestController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ISFDbContext _dbContext;
        public AppendTestController(ILogger logger, ISFDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
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
    }
}