using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Core;
using ISF.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ISF.Services
{
    public class SoundRepository : ISoundRepository
    {
        private readonly ISFDbContext _context;
        public SoundRepository(ISFDbContext context)
        {
            _context = context;
        }
        public async Task<List<SoundModel>> GetSoundModelsAsync()
        {
            if (_context.SoundModels.Count() == 0)
            {
                throw new ArgumentException("Sound models not found.", nameof(_context.SoundModels));
            }
            var response = await _context.SoundModels.ToListAsync();
            return response ?? new List<SoundModel>();

        }
        public async Task<List<FavoriteSound>> GetFavoriteSoundsAsync(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            return await _context.FavoriteSounds.Where(s => s.UserId == userId).ToListAsync();
        }
        public async Task<bool> AddFavoriteSoundAsync(int userId, string soundUrl)
        {
            if (userId == 0 || string.IsNullOrEmpty(soundUrl))
            {
                throw new ArgumentException("User ID and sound URL cannot be null or empty.", nameof(userId));
            }
            var isSoundExists = await _context.FavoriteSounds.AnyAsync(s => s.SoundUrl == soundUrl && s.UserId == userId);
            if (isSoundExists)
            {
                return false;
            }
            var favoriteSound = new FavoriteSound
            {
                UserId = userId,
                SoundUrl = soundUrl
            };
            _context.FavoriteSounds.Add(favoriteSound);
            await _context.SaveChangesAsync();
            return true;
        }
            public async Task<bool> RemoveFavoriteSoundAsync(int userId, string soundUrl)
        {
            if (userId == 0 || string.IsNullOrEmpty(soundUrl))
            {
                throw new ArgumentException("User ID and sound URL cannot be null or empty.", nameof(userId));
            }
            var relativeSound = await _context.FavoriteSounds.FirstOrDefaultAsync(s => s.SoundUrl == soundUrl && s.UserId == userId);
            if (relativeSound is null)
            {
                return false;
            }
            _context.FavoriteSounds.Remove(relativeSound);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}