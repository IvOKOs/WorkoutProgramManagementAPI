
namespace WorkoutProgramManagementAPI.Services.Workouts
{
    public interface IWorkoutsService
    {
        Task<bool> WorkoutExists(int id);
    }
}