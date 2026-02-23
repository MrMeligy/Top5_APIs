using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Data.Migrations;
using Top5.Data.Repositories;
using Top5.Domain.Entities;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly IRepository<Token> _tokens;
        private readonly IConfiguration _config;

        public AuthService(IPlayerRepository playerRepository,IConfiguration config,IRepository<Token> tokens)
        {
            _playerRepo = playerRepository;
            _config = config;
            _tokens = tokens;
        }

        public async Task<AuthResponseDto?> login(AuthDto auth)
        {
            var user = await _playerRepo.getByUserName(auth.username);
            if(user == null)
            {
                return null;
            }
            if (!Verify(auth.password, user.password))
            {
                return null;
            };
            var refreshToken = GenerateRefreshToken();
            await _tokens.AddAsync(new Token()
            {
                playerId = user.id,
                createdAt = DateTime.UtcNow,
                expiresAt = DateTime.UtcNow.AddDays(7),
                hashedToken = HashToken(refreshToken),
            });
            
            return new AuthResponseDto()
            {
                accessToken = GenerateAccessToken(user),
                refreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto?> refresh(string token)
        {
            var hashed = HashToken(token);
            var refreshToken = await _tokens.FirstOrDefaultAsync(t => t.hashedToken == hashed);
            if ( refreshToken == null || refreshToken.expiresAt < DateTime.UtcNow || refreshToken.isRevoked )
            {
                return null;
            }
            var player = await _playerRepo.GetByIdAsync(refreshToken.playerId);
            if (player == null)
            {
                return null;
            }
            refreshToken.isRevoked = true;
            refreshToken.revokedAt = DateTime.UtcNow;
            await _tokens.UpdateAsync(refreshToken);
            var newToken = GenerateRefreshToken();
            await _tokens.AddAsync(new Token()
            {
                playerId = player.id,
                createdAt = DateTime.UtcNow,
                hashedToken = HashToken(newToken),
                expiresAt= DateTime.UtcNow.AddDays(7),
            });
            return new AuthResponseDto() { 
                accessToken = GenerateAccessToken(player) , 
                refreshToken = newToken 
            };

        }

        public async Task<AuthResponseDto?> register(Player player)
        {
            if (await _playerRepo.isExistAsync(player)) {
                return null;
            }
            player.password = BCrypt.Net.BCrypt.HashPassword(player.password);
            await _playerRepo.AddAsync(player);
            var refreshToken = GenerateRefreshToken();
            await _tokens.AddAsync(new Token()
            {
                playerId = player.id,
                createdAt = DateTime.UtcNow,
                expiresAt = DateTime.UtcNow.AddDays(7),
                hashedToken = HashToken(refreshToken),

            });
            return new AuthResponseDto()
            {
                accessToken = GenerateAccessToken(player),
                refreshToken = refreshToken
            };

        }



        private string GenerateAccessToken(Player user)
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
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        private string HashToken(string token)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
        private bool Verify(string password, string hashedpassword)
        {
            return (BCrypt.Net.BCrypt.Verify(password, hashedpassword));
        }
    }
}
