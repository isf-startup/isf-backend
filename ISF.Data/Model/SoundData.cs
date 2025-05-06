using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class SoundData
    {
        [Key]
        public int Id { get; set; }
        public string Pathway { get; set; } = string.Empty;
    }
}