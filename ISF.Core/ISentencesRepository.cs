using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Data.Model;

namespace ISF.Core
{
    public interface ISentencesRepository
    {
        Task<List<Sentence>> GetSentencesAsync(string languageCode);
        Task<List<FavoriteSentence>> GetFavoriteSentencesAsync(string userId);
        Task<bool> AddFavoriteSentenceAsync(string userId, string sentence);
        Task<bool> RemoveFavoriteSentenceAsync(string userId, string sentence);
    }
}