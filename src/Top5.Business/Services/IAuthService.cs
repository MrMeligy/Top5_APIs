using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public interface IAuthService
    {
        public Task<Result<AuthResponseDto?>> login(AuthDto auth);
        public Task<Result<AuthResponseDto?>> register(Player player);
        public Task<Result<AuthResponseDto?>> refresh(string token);
    }
}
