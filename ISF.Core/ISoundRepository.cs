using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Data.Model;

namespace ISF.Core
{
    public interface ISoundRepository
    {
        Task<List<SoundModel>> GetSoundModelsAsync();
        Task<List<FavoriteSound>> GetFavoriteSoundsAsync(string userId);
        Task<bool> AddFavoriteSoundAsync(string userId, string soundUrl);
        Task<bool> RemoveFavoriteSoundAsync(string userId, string soundUrl);
    }
}