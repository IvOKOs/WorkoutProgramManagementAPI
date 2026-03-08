using WorkoutProgramManagementAPI.DTOs;

namespace WorkoutProgramManagementAPI.Services
{
    public interface IWorkoutProgramsService
    {
        Task<GetWorkoutProgramDto> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto);
        Task<GetWorkoutProgramDto?> GetWorkoutProgramAsync(int id);
        Task<IEnumerable<GetWorkoutProgramsDto>> GetWorkoutProgramsAsync();
    }
}