namespace WorkoutProgramManagementAPI.Shared.Result
{
    public static class ExerciseSessionsError
    {
        public static Error NotFound = new Error($"ExerciseSession.{nameof(NotFound)}", "Exercise session is not found.");
        public static Error InvalidSession = new Error($"ExerciseSession.{nameof(InvalidSession)}", "Exercise session is not valid.");

    }
}
