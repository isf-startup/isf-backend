using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class FavoriteSentence
    {
        [Key]
        public int Id { get; set; }
        public string? Sentence { get; set; }
        public string? UserId { get; set; }
        public UserData User { get; set; } = null!;
    }
}