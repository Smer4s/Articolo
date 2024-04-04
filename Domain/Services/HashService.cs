using System.Security.Cryptography;
using System.Text;

namespace Domain.Services;

public class HashService
{
	private readonly SHA256 hasher = SHA256.Create();
	public string HashPassword(string password)
	{
		var bytes = Encoding.ASCII.GetBytes(password);
		var hash = hasher.ComputeHash(bytes);

		return Convert.ToHexString(hash);
	}
}
