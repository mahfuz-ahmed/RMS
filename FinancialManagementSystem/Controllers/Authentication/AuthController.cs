using FinancialManagementSystem.Application;
using FinancialManagementSystem.Core;
using FinancialManagementSystem.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _tokenService;
        private readonly ISender _sender;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(AppDbContext context, IJwtTokenService tokenService, ISender sender,IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _sender = sender;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto user)
        {
            var result = await _sender.Send(new SignInQuery(user.Email, user.Password));
            return Ok(result);
        }

        //[HttpPost("refresh")]
        //public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        //{
        //    var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
        //    if (principal == null) return BadRequest("Invalid token");

        //    var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        //    var storedToken = await _context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == userId);

        //    if (storedToken == null || storedToken.IsRevoked || storedToken.IsUsed || storedToken.Expires < DateTime.UtcNow)
        //        return Unauthorized("Invalid refresh token");

        //    storedToken.IsUsed = true;
        //    _context.RefreshToken.Update(storedToken);

        //    var user = await _context.Users.FindAsync(userId);
        //    var newJwt = _tokenService.GenerateAccessToken(user);
        //    var newRefreshToken = _tokenService.GenerateRefreshToken(user.ID, newJwt);

        //    _context.RefreshToken.Add(newRefreshToken);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { token = newJwt, refreshToken = newRefreshToken.Token });
        //}

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout([FromBody] string refreshToken)
        //{
        //    var token = await _context.RefreshToken.FirstOrDefaultAsync(x => x.Token == refreshToken);
        //    if (token == null) return NotFound();

        //    token.IsRevoked = true;
        //    _context.RefreshToken.Update(token);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}

        [HttpPost("signup")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] User user)
        {
            var result = await _sender.Send(new SignUpCommand(user));
            return Ok(result);
        }
    }
}
