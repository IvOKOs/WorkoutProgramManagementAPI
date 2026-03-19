namespace WorkoutProgramManagementAPI.Shared.Result
{
    public record Error
    {
        public string Code { get; }
        public string? Description { get; }

        public Error(string code, string? description = null)
        {
            Code = code;
            Description = description;
        }

        public static Error None = new Error("No error");
    }
}
