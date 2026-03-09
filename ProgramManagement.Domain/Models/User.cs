using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public List<WorkoutProgram> WorkoutPrograms { get; set; } = new();
        public List<WorkoutSession> WorkoutSessions { get; set; } = new();
    }
}
