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
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<ExerciseSession> ExerciseSessions { get; set; }
        public DbSet<ExerciseSet> ExerciseSets { get; set; }

        public WorkoutManagementDbContext(DbContextOptions<WorkoutManagementDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1 }
            );

            modelBuilder.Entity<WorkoutProgram>().HasData(
                new WorkoutProgram 
                { 
                    Id = 1, 
                    UserId = 1, 
                    Name = "Push Pull Legs", 
                    Difficulty = DifficultyLevel.Intermediate 
                }
            );

            modelBuilder.Entity<Workout>().HasData(
                new Workout { Id = 1, WorkoutProgramId = 1, DayNumber = 1 }
            );

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Bench Press" },
                new Exercise { Id = 2, Name = "Barbell Row" }
            );

            modelBuilder.Entity<WorkoutExercise>().HasData(
                new WorkoutExercise
                {
                    Id = 1,
                    ExerciseId = 1,
                    Sets = 3,
                    Reps = 8,
                    Weight = 80,
                    RestTimeSeconds = 120,
                    WorkoutId = 1
                }
            );


            modelBuilder.Entity<ExerciseSession>()
                .HasOne(es => es.WorkoutExercise)
                .WithMany()
                .HasForeignKey(es => es.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ExerciseSession>()
                .HasOne(es => es.WorkoutSession)
                .WithMany(ws => ws.ExerciseSessions)
                .HasForeignKey(es => es.WorkoutSessionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
