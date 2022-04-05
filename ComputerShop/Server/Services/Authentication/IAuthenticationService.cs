﻿using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<List<User>> GetAllUsers();
        Task<ServiceResponse<string>> Login (Login login);
        Task<ServiceResponse<string>> Register(Register register);
        Task<bool> UserExists(string email);
    }
}
