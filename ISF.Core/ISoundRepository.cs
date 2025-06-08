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
        Task<List<FavoriteSound>> GetFavoriteSoundsAsync(int userId);
        Task<bool> AddFavoriteSoundAsync(int userId, string soundUrl);
        Task<bool> RemoveFavoriteSoundAsync(int userId, string soundUrl);
    }
}