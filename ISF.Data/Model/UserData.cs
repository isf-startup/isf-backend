using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    //dotnet ef migrations add initialcreate --project .\ISF.Data\ --startup-project .\ISF.API\
    public class UserData
    {
        [Key]
        public int Id { get; set; } // local ID
        public string? GoogleId { get; set; }
        public string? AppleId { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPremium { get; set; } // Indicates if the user is a premium user
        public ICollection<FavoriteSentence> FavoriteSentences { get; set; } = new HashSet<FavoriteSentence>();
        public ICollection<FavoriteSound> FavoriteSounds { get; set; } = new HashSet<FavoriteSound>();
    }
}