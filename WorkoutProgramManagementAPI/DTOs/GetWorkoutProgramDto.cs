using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DTOs
{
    public class GetWorkoutProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DifficultyLevel Difficulty { get; set; }
    }

    //public class GetWorkoutProgramsDto
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } = "";
    //    public string? Description { get; set; }
    //    public DifficultyLevel Difficulty { get; set; }
    //}
}
