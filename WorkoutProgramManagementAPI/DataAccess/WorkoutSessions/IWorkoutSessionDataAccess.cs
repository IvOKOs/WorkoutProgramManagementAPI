using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DataAccess.WorkoutSessions
{
    public interface IWorkoutSessionDataAccess
    {
        Task<WorkoutSession> AddSession(WorkoutSession workoutSession);
        Task CompleteSession(WorkoutSession workoutSession);
        Task<WorkoutSession?> GetWorkoutSession(int id);
    }
}