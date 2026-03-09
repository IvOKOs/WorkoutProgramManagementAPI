using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public class ExerciseSet
    {
        public int Id { get; set; }
        public int SetNumber { get; set; }
        public int ActualReps { get; set; }
        public double Weight { get; set; }
        public int RestTimeSeconds { get; set; }
        public int ExerciseSessionId { get; set; }
        public ExerciseSession ExerciseSession { get; set; } = null!;
    }
}
