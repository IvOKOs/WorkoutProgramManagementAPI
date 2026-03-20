using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.ExerciseSessions
{
    public interface IExerciseSessionsService
    {
        Task<ExerciseSession?> GetExerciseSession(int id);
        Task<Result> UpdateExerciseSessions(List<CreateExerciseSessionDto> createExerciseSessionDtos);
    }
}