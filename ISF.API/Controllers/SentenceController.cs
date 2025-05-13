using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Core;
using Microsoft.AspNetCore.Mvc;

namespace ISF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentenceController : ControllerBase
    {
        private readonly ISentencesRepository _sentencesRepository;
        public SentenceController(ISentencesRepository sentencesRepository)
        {
            _sentencesRepository = sentencesRepository;
        }
        
        [HttpGet("getsentences")]
        public async Task<IActionResult> GetSentences()
        {
           var languageCode = HttpContext.Request.Headers["LanguageCode"].ToString();
           var result = await _sentencesRepository.GetSentencesAsync(languageCode);
           return Ok();
        }
    }
}