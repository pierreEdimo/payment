using Microsoft.AspNetCore.Mvc;
using payment.Repositories.ActivityRepositories;
using payment.Dto;

namespace payment.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ActivityController : ControllerBase
{
    private readonly IActivityRepository _repository;

    public ActivityController(IActivityRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetAllActivities()
    {
        return await _repository.GetAllActivities();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivity(int id)
    {
        return await _repository.GetActivity(id);
    }

    [HttpGet("{uid}")]
    public async Task<ActionResult<List<ActivityDto>>> GetActivitiesByUid(String uid)
    {
        return await _repository.GetActivitiesByUserId(uid);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto newActivity)
    {
        return await _repository.CreateActivity(newActivity);
    }
}