using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;

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


    public async Task<GetWorkoutSessionDto?> StartWorkoutSession(int userId, int workoutId)
    {
        var workout = await _workoutManagementDbContext.Workouts
                            .Include(w => w.WorkoutExercises)
                                .ThenInclude(we => we.Exercise)
                            .FirstOrDefaultAsync(w => w.Id == workoutId);
        if (workout is null) return null;

        var workoutSession = new WorkoutSession()
        {
            UserId = userId,
            WorkoutId = workoutId,
            Status = WorkoutStatus.InProgress,
        };
        workoutSession.StartedAt = DateTime.UtcNow;
        _workoutManagementDbContext.WorkoutSessions.Add(workoutSession);
        await _workoutManagementDbContext.SaveChangesAsync();

        var workoutSessionDto = _mapper.Map<GetWorkoutSessionDto>(workoutSession);

        List<ExerciseSession> exerciseSessions = new List<ExerciseSession>();
        foreach (var workoutExercise in workout.WorkoutExercises)
        {
            var exerciseSession = new ExerciseSession()
            {
                WorkoutExerciseId = workoutExercise.Id,
                WorkoutSessionId = workoutSession.Id,
                ExerciseName = workoutExercise.Exercise.Name,
                PlannedSets = workoutExercise.Sets,
                PlannedReps = workoutExercise.Reps,
                PlannedWeight = workoutExercise.Weight,
            };
            exerciseSessions.Add(exerciseSession);
        }
        await _workoutManagementDbContext.ExerciseSessions.AddRangeAsync(exerciseSessions);
        await _workoutManagementDbContext.SaveChangesAsync();
        return workoutSessionDto;
    }

    public async Task<bool> WorkoutSessionExists(int id)
    {
        var workoutSession = await _workoutManagementDbContext.WorkoutSessions.FindAsync(id);
        return workoutSession is null ? false : true;
    }

}
