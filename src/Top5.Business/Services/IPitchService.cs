using System;
using System.Collections.Generic;
using System.Text;
using Top5.Business.Result;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IPitchService
    {
        Task<Result<List<Pitch>>> GetPitchs();
        Task<Result<Pitch>> AddPitch(Pitch pitch);
    }
}
