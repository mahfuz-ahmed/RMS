
using FinancialManagementSystem.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace FinancialManagementSystem.Application
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public SignUpCommandHandler(IUserRepository userRepository,IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {

            var exist = await _userRepository.UserGetDataByEmailAsync(command.user.Email);

            if(exist != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            command.user.PasswordHash = _passwordHasher.HashPassword(command.user, command.user.PasswordHash);

            await _userRepository.SignUpAsync(command.user);

            return command.user;
        }
    }

    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, (string JwtToken, string RefreshToken)>
    {
        private readonly IJwtTokenService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        public RefreshTokenHandler(IJwtTokenService jwtService, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<(string JwtToken, string RefreshToken)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenEntity = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (tokenEntity == null || tokenEntity.IsUsed || tokenEntity.IsRevoked || tokenEntity.Expires < DateTime.UtcNow)
                return default;

            tokenEntity.IsUsed = true;
            tokenEntity.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(tokenEntity);

            var user = await _userRepository.UserGetDataAsync(tokenEntity.UserId);
            if (user == null) return default;

            var newJwt = _jwtService.GenerateAccessToken(user);
            var jwtId = new JwtSecurityTokenHandler().ReadJwtToken(newJwt).Id;
            var newRefreshToken = _jwtService.GenerateRefreshToken(user.Id, jwtId);

            await _refreshTokenRepository.AddRefreshTokenAsync(newRefreshToken);

            return (newJwt, newRefreshToken.Token);
        }
       }
    }
