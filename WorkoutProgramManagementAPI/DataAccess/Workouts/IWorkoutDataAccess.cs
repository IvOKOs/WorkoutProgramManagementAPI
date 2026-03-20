using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DataAccess.Workouts
{
    public interface IWorkoutDataAccess
    {
        Task<Workout?> GetWorkout(int id);
    }
}