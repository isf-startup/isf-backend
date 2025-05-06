using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model.DTO
{
    public class AppleLoginDTO
    {
        public string UserIdentifier { get; set; }
        public string IdentityToken { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}