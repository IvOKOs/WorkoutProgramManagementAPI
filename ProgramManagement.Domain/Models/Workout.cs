using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }

        public List<WorkoutExercise> WorkoutExercises { get; set; } = new();

        public int WorkoutProgramId { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; } = null!;
    }
}
