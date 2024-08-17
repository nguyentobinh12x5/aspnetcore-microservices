using System;

namespace Auth.API.Models.Dto;

public class LoginResponseDto
{
    public string Token { get; set; }

    public UserDto User { get; set; }

}
