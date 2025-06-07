using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISF.Data.Model.DTO;

namespace ISF.Core
{
    public interface IPlatformTokenQueryService
    {
        Task<string> GoogleTokenValidation(GoogleLoginDTO googleLoginDTO);
        Task<string> AppleTokenValidation(AppleLoginDTO appleLoginDTO);
    }
}