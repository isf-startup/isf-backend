using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model.DTO
{
    public class FavoriteRequestDTO
    {
        public int SentenceId { get; set; }
        public int UserId { get; set; }
    }
}