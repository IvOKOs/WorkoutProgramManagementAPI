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
        public List<Exercise> Exercises { get; set; }

        public WorkoutProgram WorkoutProgram { get; set; }
        public int WorkoutProgramId { get; set; }
    }
}
