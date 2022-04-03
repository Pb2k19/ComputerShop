using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Text.RegularExpressions;

namespace ComputerShop.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        List<User> users = new();

        public async Task<List<User>> GetAllUsers()
        {
            return users;
        }
        public async Task<bool> UserExists(string email)
        {
            var users = await GetAllUsers();
            email = email.ToLower();
            return users.Any(x => x.Email.ToLower().Equals(email));
        }
        public async Task<ServiceResponse<string>> Register(Register register)
        {
            if(!PasswordPolicyCheck(register))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Podane hasło nie spełnia wymagań"
                };
            }
            if (await UserExists(register.Email))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Podany adres już istnieje w serwisie"
                };
            }
            User user = new()
            {
                Email = register.Email,
            };
            user.Password = await CreateHash(register.Password);
            if(string.IsNullOrEmpty(user.Password) || user.Password.Length < 12)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Coś poszło nie tak"
                };
            }
            users.Add(user);
            return new ServiceResponse<string> { Data = user.Id };
        }
        protected async Task<string> CreateHash(string password)
        {
            string hash = string.Empty;
            await Task.Run(() => hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12, BCrypt.Net.HashType.SHA512));
            return hash;
        }
        public virtual bool PasswordPolicyCheck(Register register)
        {
            if(register == null || string.IsNullOrWhiteSpace(register.Password) || 
               !register.ConfPassword.Equals(register.Password) || register.Password.ToLower().Contains(register.Email.ToLower()))
                return false;
            Regex regex = new(@"(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,30}");
            return regex.IsMatch(register.Password);
        }
    }
}
