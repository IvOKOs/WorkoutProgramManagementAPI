using WorkoutProgramManagementAPI.DTOs.ExerciseSessionsDtos;

namespace WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos
{
    public class CompleteWorkoutSessionDto
    {
        public List<CreateExerciseSessionDto> Exercises { get; set; } = new();
    }
}
