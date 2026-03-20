using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.WorkoutPrograms;

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


    public async Task<Result<IEnumerable<GetWorkoutProgramDto>?>> GetWorkoutPrograms()
    {
        var workoutPrograms = await _workoutManagementDbContext.WorkoutPrograms
        .ProjectTo<GetWorkoutProgramDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
        if(workoutPrograms is null)
        {
            return Result<IEnumerable<GetWorkoutProgramDto>?>.Failure(WorkoutProgramError.NotFound);
        }
        return Result<IEnumerable<GetWorkoutProgramDto>?>.Success(workoutPrograms);
    }

    public async Task<Result<GetWorkoutProgramDto?>> GetWorkoutProgram(int id)
    {
        var workoutProgramDto = await _workoutManagementDbContext.WorkoutPrograms
        .Where(w => w.Id == id)
        .ProjectTo<GetWorkoutProgramDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

        if(workoutProgramDto is null)
        {
            return Result<GetWorkoutProgramDto?>.Failure(WorkoutProgramError.NotFound);
        }
        return Result<GetWorkoutProgramDto?>.Success(workoutProgramDto);
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
