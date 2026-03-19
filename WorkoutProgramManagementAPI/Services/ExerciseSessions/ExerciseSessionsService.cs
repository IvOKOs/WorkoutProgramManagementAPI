using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.ExerciseSessions;

public class ExerciseSessionsService : IExerciseSessionsService
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;

    public ExerciseSessionsService(WorkoutManagementDbContext workoutManagementDbContext)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
    }


    public async Task<ExerciseSession?> GetExerciseSession(int id)
    {
        return await _workoutManagementDbContext.ExerciseSessions.FindAsync(id);
    }

    public async Task<Result> UpdateExerciseSessions(List<CreateExerciseSessionDto> createExerciseSessionDtos)
    {
        foreach (var exerciseSessionDto in createExerciseSessionDtos)
        {
            var exerciseSession = await GetExerciseSession(exerciseSessionDto.Id);
            if (exerciseSession is null)
            {
                return Result.Failure(ExerciseSessionsError.NotFound);
            }
            foreach(var exerciseSetDto in exerciseSessionDto.Sets)
            {
                var exerciseSet = new ExerciseSet()
                {
                    SetNumber = exerciseSetDto.SetNumber,
                    ActualReps = exerciseSetDto.ActualReps,
                    Weight = exerciseSetDto.Weight,
                    RestTimeSeconds = exerciseSetDto.RestTimeSeconds,
                    ExerciseSessionId = exerciseSession.Id,
                };
                exerciseSession.PerformedSets.Add(exerciseSet);
            }
        }
        await _workoutManagementDbContext.SaveChangesAsync();
        return Result.Success();
    }
}
