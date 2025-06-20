using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Core;
using ISF.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ISF.Services
{
    public class SentenceRepository : ISentencesRepository
    {
        private readonly ISFDbContext _context;
        public SentenceRepository(ISFDbContext context)
        {
            _context = context;
        }
        public async Task<List<Sentence>> GetSentencesAsync(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                throw new ArgumentException("Language code cannot be null or empty.", nameof(languageCode));
            }
            languageCode = languageCode.ToLower();
             if (languageCode == "en")
            {
                    var result = await _context.Sentences.Where(s => !string.IsNullOrEmpty(s.WordEN)).ToListAsync();
                    return result;
            }             

            else if (languageCode == "tr")
            {
            var result = await _context.Sentences.Where(s => !string.IsNullOrEmpty(s.WordTR)).ToListAsync();
            return result;
            }

            else
            {
                throw new ArgumentException("Invalid language code.", nameof(languageCode));
            }
        }
        public Task<List<FavoriteSentence>> GetFavoriteSentencesAsync(int userId)
        {
            return _context.FavoriteSentences.Where(s => s.UserId == userId).ToListAsync();
        }
        public async Task<bool> AddFavoriteSentenceAsync(int userId, string sentence)
        {
            if (userId == 0 || string.IsNullOrEmpty(sentence))
            {
                throw new ArgumentException("User ID and sentence cannot be null or empty.", nameof(userId));
            }
            var isSentenceExists = await _context.FavoriteSentences.AnyAsync(s => s.Sentence == sentence && s.UserId == userId);
            if (isSentenceExists)
            {
                return false;
            }
            var favoriteSentence = new FavoriteSentence
            {
                UserId = userId,
                Sentence = sentence
            };
            _context.FavoriteSentences.Add(favoriteSentence);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveFavoriteSentenceAsync(int userId, string sentence)
        {
            if (userId==0 || string.IsNullOrEmpty(sentence))
            {
                throw new ArgumentException("User ID and sentence cannot be null or empty.", nameof(userId));
            }
            var favoriteSentence = await _context.FavoriteSentences.FirstOrDefaultAsync(s => s.Sentence == sentence && s.UserId == userId);
            if (favoriteSentence == null)
            {
                return false;
            }
            var response = _context.FavoriteSentences.Remove(favoriteSentence);
            if (response.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}