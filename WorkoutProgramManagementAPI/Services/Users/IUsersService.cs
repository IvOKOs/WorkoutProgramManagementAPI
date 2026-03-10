
namespace WorkoutProgramManagementAPI.Services.Users
{
    public interface IUsersService
    {
        Task<bool> HasActiveWorkoutSession(int userId);
        Task<bool> UserExists(int id);
    }
}