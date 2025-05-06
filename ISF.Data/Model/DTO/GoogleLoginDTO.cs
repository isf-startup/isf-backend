using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model.DTO
{
    public class GoogleLoginDTO
    {
        public string IdToken { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}