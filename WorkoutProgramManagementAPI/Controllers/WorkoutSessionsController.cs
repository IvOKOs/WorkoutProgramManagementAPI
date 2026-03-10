using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;
using WorkoutProgramManagementAPI.Services.Users;
using WorkoutProgramManagementAPI.Services.Workouts;
using WorkoutProgramManagementAPI.Services.WorkoutSessions;

namespace WorkoutProgramManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutSessionsController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IWorkoutsService _workoutsService;
    private readonly IWorkoutSessionsService _workoutSessionsService;

    public WorkoutSessionsController(IUsersService usersService,
                                     IWorkoutsService workoutsService,
                                     IWorkoutSessionsService workoutSessionsService)
    {
        _usersService = usersService;
        _workoutsService = workoutsService;
        _workoutSessionsService = workoutSessionsService;
    }


    [HttpPost("/users/{userId}/workouts/{workoutId}/workoutsessions/start")]
    public async Task<ActionResult<GetWorkoutSessionDto>> CreateWorkoutSession(int userId,
                                                                               int workoutId)
    {
        var userExists = await _usersService.UserExists(userId);
        if (!userExists)
        {
            return NotFound("User with the given id does not exist.");
        }

        var hasActiveWorkout = await _usersService.HasActiveWorkoutSession(userId);
        if (hasActiveWorkout)
        {
            return BadRequest("User has already an active workout. Please, try again later.");
        }

        var workoutExists = await _workoutsService.WorkoutExists(workoutId);
        if (!workoutExists)
        {
            return NotFound("Workout with the given id does not exist.");
        }
        var createdWorkoutSession = await _workoutSessionsService.StartWorkoutSession(userId, workoutId);
        if(createdWorkoutSession is null)
        {
            return BadRequest("WorkoutSession could not be created.");
        }
        return Ok(createdWorkoutSession);
    }


    [HttpPost("/workoutsessions/{workoutSessionId}/complete")]
    public async Task<ActionResult> CompleteWorkoutSession(int workoutSessionId,
                                                           [FromBody] List<ExerciseSession> exercises)
    {

    }
}
