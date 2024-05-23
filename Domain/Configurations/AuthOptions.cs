using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Domain.Configurations
{
	public class AuthOptions
	{
		public const string ISSUER = "Articolo";
		public const string AUDIENCE = "ArticoloClient";
		private const string KEY = "AN90unU@BjkBUAHInbfhjBu(jKgUBJBhrHI#iuB#JkbGkpoKALs";
		public const int LIFETIME = 60;
		public static LifetimeValidator LifetimeValidator => ValidateLifetime;
		public static SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		private static bool ValidateLifetime(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
		{
			if (expires != null)
			{
				return expires > DateTime.UtcNow;
			}

			return false;
		}
	}
}
