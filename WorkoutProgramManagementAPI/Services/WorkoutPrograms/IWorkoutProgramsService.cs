using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;

namespace WorkoutProgramManagementAPI.Services.WorkoutPrograms
{
    public interface IWorkoutProgramsService
    {
        Task<GetWorkoutProgramDto> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto);
        Task<GetWorkoutProgramDto?> GetWorkoutProgramAsync(int id);
        Task<IEnumerable<GetWorkoutProgramDto>> GetWorkoutProgramsAsync();
    }
}