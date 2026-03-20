using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DataAccess.ExerciseSessions
{
    public interface IExerciseSessionDataAccess
    {
        Task AddMultipleExerciseSessions(List<ExerciseSession> exerciseSessions);
    }
}