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
        public Task<List<Sentence>> GetSentencesAsync(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                throw new ArgumentException("Language code cannot be null or empty.", nameof(languageCode));
            }
            if (languageCode == "en")
            {
                return _context.Sentences.Where(s => s.WordEN == "en").ToListAsync();
            }
            else if (languageCode == "tr")
            {
                return _context.Sentences.Where(s => s.WordTR == "tr").ToListAsync();
            }
            else
            {
                throw new ArgumentException("Invalid language code.", nameof(languageCode));
            }

        }
    }
}