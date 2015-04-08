using System.Threading.Tasks;
using Parse;

namespace SSWindows.Interfaces
{
    public interface IPerson
    {
        Task<string> Register();
        Task<string> Login();
        Task<string> Update(string oldUsername, string oldPassword);
        string Username { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        bool IsEmailVerified { get;}
    }
}