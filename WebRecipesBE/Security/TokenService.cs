using WebRecipesBE.Data;
using WebRecipesBE.Models;

namespace WebRecipesBE.Security
{
    public class TokenService
    {
        private readonly DataContext _context;

        public TokenService(DataContext context)
        {
            _context = context;
        }

        public void SaveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public RefreshToken GetRefreshToken(string token)
        {
            return _context.RefreshTokens.SingleOrDefault(rt => rt.Token == token);
        }

        public void UpdateRefreshToken(RefreshToken oldToken, string newToken, int expirationDays)
        {
            oldToken.Token = newToken;
            oldToken.Expiration = DateTime.UtcNow.AddDays(expirationDays);
            _context.SaveChanges();
        }

        public bool RevokeRefreshToken(string refreshToken)
        {
            var token = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
            if (token == null)
            {
                return false;
            }

            _context.RefreshTokens.Remove(token);
            _context.SaveChanges();
            return true;
        }

    }
}
