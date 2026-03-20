using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;

namespace WorkoutProgramManagementAPI.DataAccess.ExerciseSessions;

public class ExerciseSessionDataAccess : IExerciseSessionDataAccess
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;

    public ExerciseSessionDataAccess(WorkoutManagementDbContext workoutManagementDbContext)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
    }

    public async Task AddMultipleExerciseSessions(List<ExerciseSession> exerciseSessions)
    {
        await _workoutManagementDbContext.ExerciseSessions.AddRangeAsync(exerciseSessions);
        await _workoutManagementDbContext.SaveChangesAsync();
    }
}
