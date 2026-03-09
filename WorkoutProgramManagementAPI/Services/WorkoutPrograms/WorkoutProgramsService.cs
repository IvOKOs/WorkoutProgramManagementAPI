using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;
using WorkoutProgramManagementAPI.MappingProfiles;

namespace WorkoutProgramManagementAPI.Services.WorkoutPrograms
{
    public class WorkoutProgramsService : IWorkoutProgramsService
    {
        private readonly WorkoutManagementDbContext _workoutManagementDbContext;
        private readonly IMapper _mapper;

        public WorkoutProgramsService(WorkoutManagementDbContext workoutManagementDbContext,
                                      IMapper mapper)
        {
            _workoutManagementDbContext = workoutManagementDbContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetWorkoutProgramDto>> GetWorkoutProgramsAsync()
        {
            return await _workoutManagementDbContext.WorkoutPrograms
            .ProjectTo<GetWorkoutProgramDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<GetWorkoutProgramDto?> GetWorkoutProgramAsync(int id)
        {
            return await _workoutManagementDbContext.WorkoutPrograms
            .Where(w => w.Id == id)
            .ProjectTo<GetWorkoutProgramDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        }

        public async Task<GetWorkoutProgramDto> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto)
        {
            var workoutProgram = _mapper.Map<WorkoutProgram>(workoutProgramDto);
            _workoutManagementDbContext.WorkoutPrograms.Add(workoutProgram);
            await _workoutManagementDbContext.SaveChangesAsync();

            var resultWorkoutProgramDto = _mapper.Map<GetWorkoutProgramDto>(workoutProgram);
            return resultWorkoutProgramDto;
        }
    }
}
