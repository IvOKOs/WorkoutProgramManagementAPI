using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;

public class CreateExerciseSessionDto
{
    public int Id { get; set; }
    public List<ExerciseSet> Sets { get; set; } = new();
}
