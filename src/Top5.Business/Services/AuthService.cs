using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Data.Repositories;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly IConfiguration _config;

        public AuthService(IPlayerRepository playerRepository,IConfiguration config)
        {
            _playerRepo = playerRepository;
            _config = config;
        }

        public async Task<string?> login(AuthDto auth)
        {
            var user = await _playerRepo.getByUserName(auth.username);
            if(user == null)
            {
                return null;
            }
            if (Verify(auth.password, user.password))
            {
                return GenerateToken(user);
            }
            return null;
        }

        public async Task<string?> register(Player player)
        {
            if (await _playerRepo.isExistAsync(player)) {
                return null;
            }
            player.password = BCrypt.Net.BCrypt.HashPassword(player.password);
            await _playerRepo.AddAsync(player);
            return GenerateToken(player);

        }

        private string GenerateToken(Player user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _config["JWT:Issuer"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["JWT:AccessTokenExpirationMinutes"])),
                signingCredentials: credentials
                );
            var _token = new JwtSecurityTokenHandler().WriteToken(token);
            return _token;
        }
        private bool Verify(string password, string hashedpassword)
        {
            return (BCrypt.Net.BCrypt.Verify(password, hashedpassword));
        }
    }
}
