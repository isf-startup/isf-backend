using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class SoundModel
    {
        [Key]
        public int Id { get; set; }
        public string? SoundUrl { get; set; }

    }
}