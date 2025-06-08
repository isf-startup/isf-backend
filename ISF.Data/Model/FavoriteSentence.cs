using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class FavoriteSentence
    {
        [Key]
        public int Id { get; set; }
        public string? Sentence { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserData User { get; set; } = null!;
    }
}