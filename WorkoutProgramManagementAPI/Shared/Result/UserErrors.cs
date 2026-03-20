namespace WorkoutProgramManagementAPI.Shared.Result
{
    public static class UserErrors
    {
        public static Error NotExists = new Error("User.NotExists", "User does not exist.");
        public static Error AlreadyHasActiveWorkoutSession = new Error("User.AlreadyHasActiveWorkoutSession", "User has already an active workout session and cannot start another at the moment.");

    }
}
