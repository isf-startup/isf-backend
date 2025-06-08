using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class FavoriteSound
    {
        [Key]
        public int Id { get; set; }
        public string? SoundUrl { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserData User { get; set; } = null!;
    }
}