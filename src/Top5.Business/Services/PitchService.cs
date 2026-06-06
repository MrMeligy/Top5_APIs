using System;
using System.Collections.Generic;
using System.Text;
using Top5.Business.Result;
using Top5.Data.Repositories;
using Top5.Domain.Entities;
using System.Threading.Tasks;

namespace Top5.Business.Services
{
    public class PitchService : IPitchService
    {
        private readonly IRepository<Pitch> _repository;

        public PitchService(IRepository<Pitch> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Pitch>> AddPitch(Pitch pitch)
        {
            await _repository.AddAsync(pitch);
            await _repository.SaveChangesAsync();
            return Result<Pitch>.Success(pitch);
        }

        public async Task<Result<List<Pitch>>> GetPitchs()
        {
            var pitchs = await _repository.GetAllAsync();
            if (pitchs == null) {
                return Result<List<Pitch>>.Failure("No pitchs found.");
            }
            return Result<List<Pitch>>.Success(pitchs.ToList());
        }
    }
}
