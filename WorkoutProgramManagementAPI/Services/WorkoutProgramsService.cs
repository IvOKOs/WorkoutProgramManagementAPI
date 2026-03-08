using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs;

namespace WorkoutProgramManagementAPI.Services
{
    public class WorkoutProgramsService : IWorkoutProgramsService
    {
        private readonly WorkoutManagementDbContext _workoutManagementDbContext;

        public WorkoutProgramsService(WorkoutManagementDbContext workoutManagementDbContext)
        {
            _workoutManagementDbContext = workoutManagementDbContext;
        }

        public async Task<IEnumerable<GetWorkoutProgramsDto>> GetWorkoutProgramsAsync()
        {
            return await _workoutManagementDbContext.WorkoutPrograms
            .Select(w => new GetWorkoutProgramsDto()
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                Difficulty = w.Difficulty,
            })
            .ToListAsync();
        }

        public async Task<GetWorkoutProgramDto?> GetWorkoutProgramAsync(int id)
        {
            return await _workoutManagementDbContext.WorkoutPrograms
            .Where(w => w.Id == id)
            .Select(w => new GetWorkoutProgramDto()
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                Difficulty = w.Difficulty,
            })
            .FirstOrDefaultAsync();
        }

        public async Task<GetWorkoutProgramDto> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto)
        {
            var workoutProgram = new WorkoutProgram()
            {
                Name = workoutProgramDto.Name,
                Description = workoutProgramDto.Description,
                Difficulty = workoutProgramDto.Difficulty,
            };
            _workoutManagementDbContext.WorkoutPrograms.Add(workoutProgram);
            await _workoutManagementDbContext.SaveChangesAsync();

            var resultWorkoutProgramDto = new GetWorkoutProgramDto()
            {
                Id = workoutProgram.Id,
                Name = workoutProgramDto.Name,
                Description = workoutProgramDto.Description,
                Difficulty = workoutProgramDto.Difficulty,
            };
            return resultWorkoutProgramDto;
        }
    }
}
