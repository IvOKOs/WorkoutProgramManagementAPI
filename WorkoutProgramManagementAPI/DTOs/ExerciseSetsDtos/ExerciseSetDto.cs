namespace WorkoutProgramManagementAPI.DTOs.ExerciseSetsDtos
{
    public class ExerciseSetDto
    {
        public int SetNumber { get; set; }
        public int ActualReps { get; set; }
        public double Weight { get; set; }
        public int RestTimeSeconds { get; set; }
    }
}
