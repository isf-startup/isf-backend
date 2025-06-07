using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISF.Core;
using ISF.Data.Model;
using ISF.Data.Model.DTO;

namespace ISF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentenceController : ControllerBase
    {
        private readonly ISentencesRepository _sentencesRepository;
        private readonly ISFDbContext _context;

        public SentenceController(ISentencesRepository sentencesRepository, ISFDbContext context)
        {
            _sentencesRepository = sentencesRepository;
            _context = context;
        }

        // Tüm cümleleri getir
        [HttpGet("getsentences")]
        public async Task<IActionResult> GetSentences()
        {
            var languageCode = HttpContext.Request.Headers["LanguageCode"].ToString();
            var result = await _sentencesRepository.GetSentencesAsync(languageCode);

            return result == null ? NotFound("Cümleler bulunamadı") : Ok(result);
        }

        // Favori cümleler
        [HttpGet("getfavoritesentences")]
        public async Task<IActionResult> GetFavoriteSentences([FromQuery] int userId)
        {
            var result = await _sentencesRepository.GetFavoriteSentencesAsync(userId.ToString());
            return result == null ? NotFound("Favori cümle bulunamadı") : Ok(result);
        }

        // Favori ekle
        [HttpPost("addfavoritesentence")]
        public async Task<IActionResult> AddFavoriteSentence([FromBody] FavoriteRequestDTO request)
        {
            var userId = request.UserId.ToString();
            var sentence = await _context.Sentences.FirstOrDefaultAsync(s => s.Id == request.SentenceId);

            if (sentence == null || userId == null)
                return BadRequest("Cümle veya kullanıcı bulunamadı");

            var text = sentence.WordEN ?? sentence.WordTR;

            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Cümle boş olamaz");

            var success = await _sentencesRepository.AddFavoriteSentenceAsync(userId, text);
            return success ? Ok(new { Message = "Favoriye eklendi." }) : NotFound("Ekleme başarısız.");
        }

        // Favori sil
        [HttpDelete("removefavoritesentence")]
        public async Task<IActionResult> RemoveFavoriteSentence([FromBody] FavoriteRequestDTO request)
        {
            var userId = request.UserId.ToString();
            var sentence = await _context.Sentences.FirstOrDefaultAsync(s => s.Id == request.SentenceId);

            if (sentence is null)
                return BadRequest("Cümle bulunamadı");

            var text = sentence.WordEN ?? sentence.WordTR;

            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Cümle boş olamaz");

            var success = await _sentencesRepository.RemoveFavoriteSentenceAsync(userId, text);
            return success ? Ok(new { Message = "Favoriden silindi." }) : NotFound("Silme başarısız.");
        }
    }
}
