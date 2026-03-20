namespace WorkoutProgramManagementAPI.Shared.Result
{
    public static class WorkoutProgramError
    {
        public static Error NotFound = new Error($"WorkoutPrograms.{nameof(NotFound)}", "Workout program was not found.");

    }
}
