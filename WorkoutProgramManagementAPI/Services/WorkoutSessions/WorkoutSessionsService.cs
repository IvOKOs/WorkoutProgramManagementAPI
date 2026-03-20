using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.WorkoutSessions;

public class WorkoutSessionsService : IWorkoutSessionsService
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;
    private readonly IMapper _mapper;

    public WorkoutSessionsService(WorkoutManagementDbContext workoutManagementDbContext,
                                  IMapper mapper)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
        _mapper = mapper;
    }

    public async Task<Result<GetWorkoutSessionDto?>> StartWorkoutSession(int userId, int workoutId)
    {
        if(!await UserExists(userId))
        {
            return Result<GetWorkoutSessionDto?>.Failure(UserErrors.NotExists);
        }
        if( await UserHasActiveWorkoutSession(userId))
        {
            return Result<GetWorkoutSessionDto?>.Failure(UserErrors.AlreadyHasActiveWorkoutSession);
        }
        var workout = await GetWorkout(workoutId);
        if (workout is null)
            return Result<GetWorkoutSessionDto?>.Failure(WorkoutSessionsError.NotFound);

        var workoutSession = await StartSession(userId, workoutId);
        var workoutSessionDto = _mapper.Map<GetWorkoutSessionDto>(workoutSession);

        await StartExerciseSessions(workout.WorkoutExercises, workoutSessionDto.Id);
        
        return Result<GetWorkoutSessionDto?>.Success(workoutSessionDto);
    }

    public async Task<Result> EndWorkoutSession(int workoutSessionId, CompleteWorkoutSessionDto completeSessionDto)
    {
        var workoutSession = await GetWorkoutSession(workoutSessionId);
        if(workoutSession is null)
        {
            return Result.Failure(WorkoutSessionsError.NotFound);
        }
        if (workoutSession.Status != WorkoutStatus.InProgress)
        {
            return Result.Failure(WorkoutSessionsError.InProgress);
        }
        var exerciseSessionsResult = await UpdateExerciseSessions(completeSessionDto.Exercises);
        if (exerciseSessionsResult.IsFailure)
            return exerciseSessionsResult;
        await CompleteSession(workoutSession);
        return Result.Success();
    }


    private async Task<Result> UpdateExerciseSessions(List<CreateExerciseSessionDto> createExerciseSessionDtos)
    {
        // improve it -> batch loading and using transaction to update
        foreach (var exerciseSessionDto in createExerciseSessionDtos)
        {
            var exerciseSession = await _workoutManagementDbContext.ExerciseSessions.FindAsync(exerciseSessionDto.Id);
            if (exerciseSession is null)
            {
                return Result.Failure(ExerciseSessionsError.NotFound);
            }
            foreach (var exerciseSetDto in exerciseSessionDto.Sets)
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

    private async Task<bool> UserExists(int id)
    {
        var user = await _workoutManagementDbContext.Users.FindAsync(id);
        return user is null ? false : true;
    }

    private async Task<bool> UserHasActiveWorkoutSession(int userId)
    {
        return await _workoutManagementDbContext.WorkoutSessions
            .Where(w => w.UserId == userId)
            .AnyAsync(w => w.Status == WorkoutStatus.InProgress);
    }

    private async Task<Workout?> GetWorkout(int id)
    {
        return await _workoutManagementDbContext.Workouts
                            .Include(w => w.WorkoutExercises)
                                .ThenInclude(we => we.Exercise)
                            .FirstOrDefaultAsync(w => w.Id == id);
    }

    private async Task<WorkoutSession> StartSession(int userId, int workoutId)
    {
        var workoutSession = new WorkoutSession()
        {
            UserId = userId,
            WorkoutId = workoutId,
            Status = WorkoutStatus.InProgress,
        };
        workoutSession.StartedAt = DateTime.UtcNow;
        _workoutManagementDbContext.WorkoutSessions.Add(workoutSession);
        await _workoutManagementDbContext.SaveChangesAsync();
        return workoutSession;
    }

    private async Task StartExerciseSessions(List<WorkoutExercise> exercises, int workoutSessionId)
    {
        List<ExerciseSession> exerciseSessions = [];
        foreach (var workoutExercise in exercises)
        {
            var exerciseSession = new ExerciseSession()
            {
                WorkoutExerciseId = workoutExercise.Id,
                WorkoutSessionId = workoutSessionId,
                ExerciseName = workoutExercise.Exercise.Name,
                PlannedSets = workoutExercise.Sets,
                PlannedReps = workoutExercise.Reps,
                PlannedWeight = workoutExercise.Weight,
            };
            exerciseSessions.Add(exerciseSession);
        }
        await _workoutManagementDbContext.ExerciseSessions.AddRangeAsync(exerciseSessions);
        await _workoutManagementDbContext.SaveChangesAsync();
    }


    private async Task<WorkoutSession?> GetWorkoutSession(int id)
    {
        return await _workoutManagementDbContext.WorkoutSessions.FindAsync(id);
    }

    private async Task CompleteSession(WorkoutSession workoutSession)
    {
        workoutSession.CompletedAt = DateTime.UtcNow;
        workoutSession.Status = WorkoutStatus.Completed;
        await _workoutManagementDbContext.SaveChangesAsync();
    }
}
