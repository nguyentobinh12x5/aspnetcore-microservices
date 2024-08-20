using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Models.Dto;

namespace AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}