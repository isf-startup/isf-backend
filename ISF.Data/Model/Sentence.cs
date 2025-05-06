using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class Sentence
    {
        public int Id { get; set; }
        public string WordTR { get; set; } = string.Empty;
        public string WordEN { get; set; } = string.Empty;
    }
}