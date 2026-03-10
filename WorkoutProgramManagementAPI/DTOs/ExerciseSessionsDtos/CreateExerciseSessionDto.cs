using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.ExerciseSetsDtos;

namespace WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;

public class CreateExerciseSessionDto
{
    public int Id { get; set; }
    public List<ExerciseSetDto> Sets { get; set; } = new();
}
