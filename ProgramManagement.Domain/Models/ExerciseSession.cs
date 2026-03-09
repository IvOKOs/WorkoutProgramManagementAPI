using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public class ExerciseSession
    {
        public int Id { get; set; }

        public int? WorkoutExerciseId { get; set; }
        public WorkoutExercise? WorkoutExercise { get; set; }

        public int WorkoutSessionId { get; set; }
        public WorkoutSession WorkoutSession { get; set; } = null!;

        public List<ExerciseSet> PerformedSets { get; set; } = new();
        public string ExerciseName { get; set; } = "";
        public int PlannedSets { get; set; }
        public int PlannedReps { get; set; }
        public double PlannedWeight { get; set; }
    }
}
