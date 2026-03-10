using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;

namespace WorkoutProgramManagementAPI.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly WorkoutManagementDbContext _workoutManagementDbContext;

        public UsersService(WorkoutManagementDbContext workoutManagementDbContext)
        {
            _workoutManagementDbContext = workoutManagementDbContext;
        }


        public async Task<bool> UserExists(int id)
        {
            var user = await _workoutManagementDbContext.Users.FindAsync(id);
            return user is null ? false : true;
        }

        public async Task<bool> HasActiveWorkoutSession(int userId)
        {
            return await _workoutManagementDbContext.WorkoutSessions
                .Where(w => w.UserId == userId)
                .AnyAsync(w => w.Status == WorkoutStatus.InProgress);
        }
    }
}
