using WorkoutManagement.Infrastructure;

namespace WorkoutProgramManagementAPI.Services.Workouts
{
    public class WorkoutsService : IWorkoutsService
    {
        private readonly WorkoutManagementDbContext _workoutManagementDbContext;

        public WorkoutsService(WorkoutManagementDbContext workoutManagementDbContext)
        {
            _workoutManagementDbContext = workoutManagementDbContext;
        }

        public async Task<bool> WorkoutExists(int id)
        {
            var workout = await _workoutManagementDbContext.Workouts.FindAsync(id);
            return workout is null ? false : true;
        }
    }
}
