using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.WorkoutSessions
{
    public interface IWorkoutSessionsService
    {
        Task<Result<GetWorkoutSessionDto?>> StartWorkoutSession(int userId, int workoutId);
        Task<WorkoutSession?> GetWorkoutSession(int id);
        Task CompleteSession(WorkoutSession workoutSession);
    }
}