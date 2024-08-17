using System;
using Auth.API.Models.Dto;

namespace Auth.API.Service.IService;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterationRequestDto registerationRequestDto);

    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

    Task<bool> AssignRoleAsync(string email, string roleName);

}
