namespace WorkoutProgramManagementAPI.Shared.Result
{
    public static class WorkoutSessionsError
    {
        public static Error NotFound = new Error($"WorkoutSessions.{nameof(NotFound)}", "Workout session was not found.");
        public static Error InProgress = new Error($"WorkoutSessions.{nameof(InProgress)}", "Workout session is currently in progress.");

    }
}
