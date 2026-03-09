using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;

namespace WorkoutProgramManagementAPI.Services.WorkoutSessions;

public class WorkoutSessionsService
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;

    public WorkoutSessionsService(WorkoutManagementDbContext workoutManagementDbContext)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
    }


    public async Task<ActionResult<CreateWorkoutSessionDto>?> StartWorkoutSession(int userId, int workoutId)
    {
        var workout = await _workoutManagementDbContext.Workouts.FindAsync(workoutId);
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

        var workoutSessionDto = new CreateWorkoutSessionDto()
        {
            Id = workoutSession.Id,
            StartedAt = workoutSession.StartedAt,
            Status = workoutSession.Status,
        };

        List<ExerciseSession> exerciseSessions = new List<ExerciseSession>();
        foreach(var workoutExercise in workout.WorkoutExercises)
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
    public async Task<bool> UserExists(int id)
    {
        var user = await _workoutManagementDbContext.Users.FindAsync(id);
        return user is null ? false : true;
    }

    public async Task<bool> WorkoutExists(int id)
    {
        var workout = await _workoutManagementDbContext.Workouts.FindAsync(id);
        return workout is null ? false : true;
    }

    public async Task<bool> HasActiveWorkoutSession(int userId)
    {
        return await _workoutManagementDbContext.WorkoutSessions
            .Where(w => w.UserId == userId)
            .AnyAsync(w => w.Status == WorkoutStatus.InProgress);
    }
}
