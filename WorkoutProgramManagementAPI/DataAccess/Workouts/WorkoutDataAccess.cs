using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;

namespace WorkoutProgramManagementAPI.DataAccess.Workouts;

public class WorkoutDataAccess : IWorkoutDataAccess
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;

    public WorkoutDataAccess(WorkoutManagementDbContext workoutManagementDbContext)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
    }


    public async Task<Workout?> GetWorkout(int id)
    {
        return await _workoutManagementDbContext.Workouts
                            .Include(w => w.WorkoutExercises)
                                .ThenInclude(we => we.Exercise)
                            .FirstOrDefaultAsync(w => w.Id == id);
    }
}
