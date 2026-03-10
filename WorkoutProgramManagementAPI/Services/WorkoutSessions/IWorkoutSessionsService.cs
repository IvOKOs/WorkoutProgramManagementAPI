using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;

namespace WorkoutProgramManagementAPI.Services.WorkoutSessions
{
    public interface IWorkoutSessionsService
    {
        Task<GetWorkoutSessionDto?> StartWorkoutSession(int userId, int workoutId);
    }
}