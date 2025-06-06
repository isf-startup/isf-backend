using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Data.Model;

namespace ISF.Core
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserData user);
    }
}