using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;

namespace WorkoutProgramManagementAPI.Services.ExerciseSessions
{
    public interface IExerciseSessionsService
    {
        Task<ExerciseSession?> GetExerciseSession(int id);
        Task<bool> UpdateExerciseSessions(List<CreateExerciseSessionDto> createExerciseSessionDtos);
    }
}