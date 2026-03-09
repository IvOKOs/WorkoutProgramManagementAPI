using WorkoutManagement.Domain.Models;

namespace WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;

public class CreateWorkoutSessionDto
{
    public int Id { get; set; }
    public DateTime? StartedAt { get; set; }
    public WorkoutStatus Status { get; set; }
}
