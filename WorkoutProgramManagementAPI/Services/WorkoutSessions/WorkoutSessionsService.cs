using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DataAccess.ExerciseSessions;
using WorkoutProgramManagementAPI.DataAccess.Users;
using WorkoutProgramManagementAPI.DataAccess.Workouts;
using WorkoutProgramManagementAPI.DataAccess.WorkoutSessions;
using WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.WorkoutSessions;

public class WorkoutSessionsService : IWorkoutSessionsService
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;
    private readonly IWorkoutSessionDataAccess _workoutSessionDataAccess;
    private readonly IUserDataAccess _userDataAccess;
    private readonly IWorkoutDataAccess _workoutDataAccess;
    private readonly IExerciseSessionDataAccess _exerciseSessionDataAccess;
    private readonly IMapper _mapper;

    public WorkoutSessionsService(WorkoutManagementDbContext workoutManagementDbContext,
        IWorkoutSessionDataAccess workoutSessionDataAccess,
        IUserDataAccess userDataAccess,
        IWorkoutDataAccess workoutDataAccess,
        IExerciseSessionDataAccess exerciseSessionDataAccess,
                                  IMapper mapper)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
        _workoutSessionDataAccess = workoutSessionDataAccess;
        _userDataAccess = userDataAccess;
        _workoutDataAccess = workoutDataAccess;
        _exerciseSessionDataAccess = exerciseSessionDataAccess;
        _mapper = mapper;
    }

    public async Task<Result<GetWorkoutSessionDto?>> StartWorkoutSession(int userId, int workoutId)
    {
        if(!await _userDataAccess.UserExists(userId))
        {
            return Result<GetWorkoutSessionDto?>.Failure(UserErrors.NotExists);
        }
        if( await _userDataAccess.UserHasActiveWorkoutSession(userId))
        {
            return Result<GetWorkoutSessionDto?>.Failure(UserErrors.AlreadyHasActiveWorkoutSession);
        }
        var workout = await _workoutDataAccess.GetWorkout(workoutId);
        if (workout is null)
            return Result<GetWorkoutSessionDto?>.Failure(WorkoutSessionsError.NotFound);

        var workoutSession = await StartSession(userId, workoutId);
        var workoutSessionDto = _mapper.Map<GetWorkoutSessionDto>(workoutSession);

        await StartExerciseSessions(workout.WorkoutExercises, workoutSessionDto.Id);
        
        return Result<GetWorkoutSessionDto?>.Success(workoutSessionDto);
    }

    public async Task<Result> EndWorkoutSession(int workoutSessionId, CompleteWorkoutSessionDto completeSessionDto)
    {
        var workoutSession = await _workoutSessionDataAccess.GetWorkoutSession(workoutSessionId);
        if(workoutSession is null)
        {
            return Result.Failure(WorkoutSessionsError.NotFound);
        }
        if (workoutSession.Status != WorkoutStatus.InProgress)
        {
            return Result.Failure(WorkoutSessionsError.InProgress);
        }

        await using var transaction = await _workoutManagementDbContext.Database.BeginTransactionAsync();
        try
        {
            var exerciseSessionsResult = await UpdateExerciseSessions(completeSessionDto.Exercises,
                                                                      workoutSessionId);
            if (exerciseSessionsResult.IsFailure)
                return exerciseSessionsResult;
            await _workoutSessionDataAccess.CompleteSession(workoutSession);

            await _workoutManagementDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
        return Result.Success();
    }

    

    private async Task<Result> UpdateExerciseSessions(List<CreateExerciseSessionDto> createExerciseSessionDtos,
                                                      int workoutSessionId)
    {
        var exerciseSessionIds = createExerciseSessionDtos
            .Select(sessionDto => sessionDto.Id)
            .ToList();

        var exerciseSessions = await _workoutManagementDbContext.ExerciseSessions
            .Where(session => exerciseSessionIds.Contains(session.Id))
            .ToListAsync();

        if(exerciseSessionIds.Count != exerciseSessions.Count)
        {
            return Result.Failure(ExerciseSessionsError.NotFound);
        }
        // complexity - O(n^2)
        foreach (var exerciseSessionDto in createExerciseSessionDtos)
        {
            var exerciseSession = exerciseSessions.First(session => session.Id == exerciseSessionDto.Id);
            if (exerciseSession.WorkoutSessionId != workoutSessionId)
                return Result.Failure(ExerciseSessionsError.InvalidSession);

            foreach (var exerciseSetDto in exerciseSessionDto.Sets)
            {
                var exerciseSet = new ExerciseSet()
                {
                    SetNumber = exerciseSetDto.SetNumber,
                    ActualReps = exerciseSetDto.ActualReps,
                    Weight = exerciseSetDto.Weight,
                    RestTimeSeconds = exerciseSetDto.RestTimeSeconds,
                    ExerciseSessionId = exerciseSessionDto.Id,
                };
                exerciseSession.PerformedSets.Add(exerciseSet);
            }
        }
        return Result.Success();
    }

    
    private async Task<WorkoutSession> StartSession(int userId, int workoutId)
    {
        var workoutSession = new WorkoutSession()
        {
            UserId = userId,
            WorkoutId = workoutId,
            Status = WorkoutStatus.InProgress,
        };
        workoutSession = await _workoutSessionDataAccess.AddSession(workoutSession);
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
        await _exerciseSessionDataAccess.AddMultipleExerciseSessions(exerciseSessions);
    }
}
