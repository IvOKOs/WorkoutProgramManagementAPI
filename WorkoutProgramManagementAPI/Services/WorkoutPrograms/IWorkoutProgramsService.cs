using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Services.WorkoutPrograms
{
    public interface IWorkoutProgramsService
    {
        Task<Result<IEnumerable<GetWorkoutProgramDto>?>> GetWorkoutPrograms();
        Task<Result<GetWorkoutProgramDto?>> GetWorkoutProgram(int id);
        Task<GetWorkoutProgramDto> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto);
    }
}