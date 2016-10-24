using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiServer.Providers
{
    public class RefreshOAuthProvider : AuthenticationTokenProvider
    {
        public override async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().GUIDFormat();

            var tokenInfo = new RefreshToken()
            {
                Id = GetHash(refreshTokenId),
                UserName = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            };

            context.Ticket.Properties.IssuedUtc = tokenInfo.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = tokenInfo.ExpiresUtc;
            tokenInfo.ProtectedTicket = context.SerializeTicket();

            var result = await DependencyInjectionConfig.Containter.Resolve<IToken>().SaveRefreshTokenInfo(tokenInfo);
            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public override async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            string hashedTokenId = GetHash(context.Token);

            var refreshToken = await DependencyInjectionConfig.Containter.Resolve<IToken>().GetRefreshTokenInfo(hashedTokenId);

            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await DependencyInjectionConfig.Containter.Resolve<IToken>().RemoveRefreshTokenInfo(hashedTokenId);
            }
        }

        public string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}