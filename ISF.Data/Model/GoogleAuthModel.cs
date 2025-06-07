using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISF.Data.Model
{
    public class GoogleAuthModel
    {
        public string Sub { get; set; } // Google unique user ID
        public string Email { get; set; }
        public string Name { get; set; }

    }
}