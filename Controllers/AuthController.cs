using CompanyPortal.DTOs.Auth;
using CompanyPortal.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOtpServcie _otpService;
        private readonly IPasswordService _passwordService;

        public AuthController(IAuthService companySignUpService, IOtpServcie otpService, IPasswordService passwordService)
        {
            _authService = companySignUpService;
            _otpService = otpService;
            _passwordService = passwordService;
        }

        [HttpPost("CompanySignUp")]
        public async Task<IActionResult> CompanySignUpAsync([FromForm] CompanySignUpDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.SignUpAsync(dto);

            if (!result.Success)
                return BadRequest(new { Message = result.Message! });

            return Ok(new { Message = result.Message! });

        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtpAsync(VerifyOtpDto dto)
        {

            var result = await _otpService.VerifyOtpAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message! });
            }

            return Ok(new { Message = result.Message! });
        }

        [HttpPost("setPassword")]
        public async Task<IActionResult> SetPasswordAsync(SetPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _passwordService.SetPasswordAsync(dto);
            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message! });
            }
            return Ok(new { Message = "Password set successfully." });

        }

        [HttpGet("GetOtp")]
        public async Task<IActionResult> GetOtpAsync([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var result = await _otpService.GetOtpAsync(email);

            if (!result.Success)
                return BadRequest(new { Message = result.Message! });

            return Ok(new { Otp = result.Data });

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.Login(dto);

            if (!result.IsAuthenticated)
            {
                return BadRequest(new { Message = result.Message! });
            }

            return Ok(result);
        }
    }
}
