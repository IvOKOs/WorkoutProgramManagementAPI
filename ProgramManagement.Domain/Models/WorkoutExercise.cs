using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int RestTimeSeconds { get; set; }
        public double Weight { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;
    }
}
