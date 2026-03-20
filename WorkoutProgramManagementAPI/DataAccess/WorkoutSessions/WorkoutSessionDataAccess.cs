using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;

namespace WorkoutProgramManagementAPI.DataAccess.WorkoutSessions
{
    public class WorkoutSessionDataAccess : IWorkoutSessionDataAccess
    {
        private readonly WorkoutManagementDbContext _workoutManagementDbContext;

        public WorkoutSessionDataAccess(WorkoutManagementDbContext workoutManagementDbContext)
        {
            _workoutManagementDbContext = workoutManagementDbContext;
        }


        public async Task<WorkoutSession> AddSession(WorkoutSession workoutSession)
        {
            workoutSession.StartedAt = DateTime.UtcNow;
            _workoutManagementDbContext.WorkoutSessions.Add(workoutSession);
            await _workoutManagementDbContext.SaveChangesAsync();
            return workoutSession;
        }

        public async Task<WorkoutSession?> GetWorkoutSession(int id)
        {
            return await _workoutManagementDbContext.WorkoutSessions.FindAsync(id);
        }

        public async Task CompleteSession(WorkoutSession workoutSession)
        {
            workoutSession.CompletedAt = DateTime.UtcNow;
            workoutSession.Status = WorkoutStatus.Completed;
            await _workoutManagementDbContext.SaveChangesAsync();
        }
    }
}
