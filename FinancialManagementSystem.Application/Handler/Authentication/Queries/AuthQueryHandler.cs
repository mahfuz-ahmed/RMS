using FinancialManagementSystem.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace FinancialManagementSystem.Application
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, AuthUserDto>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenService _jwtService;
        private readonly IRefreshTokenRepository _refreshRepo;

        public SignInQueryHandler(IUserRepository userRepo,IPasswordHasher<User> passwordHasher,IJwtTokenService jwtService,IRefreshTokenRepository refreshRepo)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshRepo = refreshRepo;
        }

        public async Task<AuthUserDto> Handle(SignInQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepo.UserGetDataByEmailAsync(query.email);
            if (user == null) throw new KeyNotFoundException("User not found");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, query.password);
            if (result != PasswordVerificationResult.Success) throw new UnauthorizedAccessException("Invalid credentials");

            var jwt = _jwtService.GenerateAccessToken(user);
            var jwtId = new JwtSecurityTokenHandler().ReadJwtToken(jwt).Id;
            var refreshToken = _jwtService.GenerateRefreshToken(user.Id, jwtId);
            await _refreshRepo.AddRefreshTokenAsync(refreshToken);

            return new AuthUserDto
            {
                ID = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                JwtToken = jwt,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
