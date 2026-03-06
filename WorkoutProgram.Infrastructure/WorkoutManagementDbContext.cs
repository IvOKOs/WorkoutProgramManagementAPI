using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutManagement.Infrastructure
{
    public class WorkoutManagementDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        public WorkoutManagementDbContext(DbContextOptions<WorkoutManagementDbContext> options) : base(options)
        {
            
        }
    }
}
