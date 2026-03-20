
namespace WorkoutProgramManagementAPI.DataAccess.Users
{
    public interface IUserDataAccess
    {
        Task<bool> UserExists(int id);
        Task<bool> UserHasActiveWorkoutSession(int userId);
    }
}