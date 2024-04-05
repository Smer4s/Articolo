using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Client.Models.Auth
{
    public class AuthCredentials
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
