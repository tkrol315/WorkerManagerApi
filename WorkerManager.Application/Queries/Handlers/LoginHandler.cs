﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkerManager.Application.Authentication;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries.Handlers
{
    public class LoginHandler : IRequestHandler<Login, string>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserReadService _readService;
        private readonly AuthenticationSettings _authenticationSettings;

        public LoginHandler(IPasswordHasher<User> passwordHasher, IUserReadService readService)
        {
            _passwordHasher = passwordHasher;
            _readService = readService;
        }

        public async Task<string> Handle(Login query, CancellationToken cancellationToken)
        {
            var user = await _readService.getUserByUserName(query.Username);
            if (user is null)
            {
                throw new InvalidUsernameOrPasswordException();
            }
            var passwordValidationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, query.Password);
            if (passwordValidationResult is PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernameOrPasswordException();
            }
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.RoleId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
