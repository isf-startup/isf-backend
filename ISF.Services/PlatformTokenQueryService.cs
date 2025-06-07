using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ISF.Core;
using ISF.Data.Model;
using ISF.Data.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace ISF.Services
{
    public class PlatformTokenQueryService : IPlatformTokenQueryService
    {
        private readonly ISFDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public PlatformTokenQueryService(ISFDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
        public async Task<string> AppleTokenValidation(AppleLoginDTO appleLoginDTO)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(appleLoginDTO.IdentityToken);

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return "Invalid token";
            }

            var user = await _dbContext.UserDatas.FirstOrDefaultAsync(u => u.AppleId == userId);

            if (user == null)
            {
                user = new UserData{
                    AppleId = userId,
                    Email = email ?? "",
                    CreatedAt = DateTime.UtcNow,
                    IsPremium = false     //buraya bakıcam
                };
                await _dbContext.UserDatas.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }

            var token = await _tokenService.GenerateToken(user);
            return token;
        }

        public async Task<string> GoogleTokenValidation(GoogleLoginDTO googleLoginDTO)
        {
            using var httpClient = new HttpClient();

             var response = await httpClient.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={googleLoginDTO.IdToken}");
    
            if (!response.IsSuccessStatusCode)
                return "Geçersiz Google ID Token.";

                 var responseString = await response.Content.ReadAsStringAsync();
                 var googleData = JsonSerializer.Deserialize<GoogleAuthModel>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    // Kullanıcıyı DB'de bul ya da oluştur
                  var user = await _dbContext.UserDatas.FirstOrDefaultAsync(u => u.GoogleId == googleData!.Sub);
                  if (user == null)
                 {
                    user = new UserData
                         {
                        GoogleId = googleData!.Sub,
                            Email = googleData!.Email ?? "",
                                DisplayName = googleData!.Name,
                                    IsPremium = false,
                                    CreatedAt =DateTime.UtcNow,
                         };

                     await _dbContext.UserDatas.AddAsync(user);
                     await _dbContext.SaveChangesAsync();
                 }

                var token = await _tokenService.GenerateToken(user);
    
                return token;
        }
    }
}