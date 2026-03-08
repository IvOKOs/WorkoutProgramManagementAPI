using System.ComponentModel.DataAnnotations;
using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DTOs
{
    public class CreateWorkoutProgramDto
    {
        [Required]
        public string Name { get; set; } = "";

        public string? Description { get; set; }

        [Required]
        public DifficultyLevel Difficulty { get; set; }
    }
}
