using Auth.API.Models.Dto;
using Auth.API.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthAPIController> _logger;

        public AuthAPIController(IAuthService authService, ILogger<AuthAPIController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterationRequestDto registerationRequestDto)
        {
            try
            {
                return Ok(await _authService.RegisterAsync(registerationRequestDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                return Ok(await _authService.LoginAsync(loginRequestDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole(string email, string roleName)
        {
            try
            {
                return Ok(await _authService.AssignRoleAsync(email, roleName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
