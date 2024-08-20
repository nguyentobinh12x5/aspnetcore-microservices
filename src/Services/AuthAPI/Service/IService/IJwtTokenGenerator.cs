using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Models;

namespace AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}