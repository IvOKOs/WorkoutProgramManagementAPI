namespace WorkoutProgramManagementAPI.Shared.Result
{
    public static class ExerciseSessionsError
    {
        public static Error NotFound = new Error($"ExerciseSession.{nameof(NotFound)}", "Exercise session is not found.");
    }
}
