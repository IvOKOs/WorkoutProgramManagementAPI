using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;

namespace WorkoutProgramManagementAPI.Services.WorkoutSessions
{
    public interface IWorkoutSessionsService
    {
        Task<GetWorkoutSessionDto?> StartWorkoutSession(int userId, int workoutId);
        Task<WorkoutSession?> GetWorkoutSession(int id);
        Task CompleteSession(WorkoutSession workoutSession);
    }
}