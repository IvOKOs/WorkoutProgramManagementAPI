using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public enum WorkoutStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public class WorkoutSession
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;

        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public WorkoutStatus Status { get; set; } = WorkoutStatus.NotStarted;

        public List<ExerciseSession> ExerciseSessions { get; set; } = new();
    }
}
