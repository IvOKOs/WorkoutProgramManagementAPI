using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public enum DifficultyLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }

    public class WorkoutProgram
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public List<Workout> Workouts { get; set; } = null!;

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
