using Microsoft.AspNetCore.Mvc;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;
using WorkoutProgramManagementAPI.Services.WorkoutSessions;
using WorkoutProgramManagementAPI.Shared.Result;

namespace WorkoutProgramManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutSessionsController : ControllerBase
{
    private readonly IWorkoutSessionsService _workoutSessionsService;

    public WorkoutSessionsController(IWorkoutSessionsService workoutSessionsService)
    {
        _workoutSessionsService = workoutSessionsService;
    }


    [HttpPost("/users/{userId}/workouts/{workoutId}/workoutsessions/start")]
    public async Task<ActionResult<GetWorkoutSessionDto>> CreateWorkoutSession(int userId,
                                                                               int workoutId)
    {
        var workoutSessionResult = await _workoutSessionsService.StartWorkoutSession(userId, workoutId);
        if (workoutSessionResult.IsFailure)
        {
            var errorDescription = workoutSessionResult.Error.Description;
            if (workoutSessionResult.Error == UserErrors.NotExists)
                return BadRequest(errorDescription ?? "User does not exist.");
            if (workoutSessionResult.Error == UserErrors.AlreadyHasActiveWorkoutSession)
                return BadRequest(errorDescription ?? "User has already an active workout session.");
            return StatusCode(500);
        }
        var createdWorkoutSession = workoutSessionResult.Value;
        return Ok(createdWorkoutSession);
    }


    [HttpPost("{workoutSessionId}/complete")]
    public async Task<ActionResult> CompleteWorkoutSession(int workoutSessionId,
                                                           [FromBody] CompleteWorkoutSessionDto completeSessionDto)
    {
        var workoutSessionResult = await _workoutSessionsService.EndWorkoutSession(workoutSessionId,
                                                                                   completeSessionDto);
        if (workoutSessionResult.IsFailure)
        {
            var errorDescription = workoutSessionResult.Error.Description;
            if (workoutSessionResult.Error == WorkoutSessionsError.NotFound)
                return NotFound($"Workout session with id {workoutSessionId} was not found.");
            if (workoutSessionResult.Error == WorkoutSessionsError.InProgress)
                return BadRequest($"Workout session with id {workoutSessionId} is currently in progress. It has already been started.");
            if (workoutSessionResult.Error == ExerciseSessionsError.NotFound)
                return NotFound(errorDescription ?? "Exercise session was not found.");
            return StatusCode(500);
        }
        return Ok();
    }
}
