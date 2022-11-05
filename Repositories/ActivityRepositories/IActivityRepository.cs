using Microsoft.AspNetCore.Mvc;
using payment.Dto;

namespace payment.Repositories.ActivityRepositories;

public interface IActivityRepository
{
    Task<ActionResult<List<ActivityDto>>> GetAllActivities();
    Task<ActionResult<ActivityDto>> GetActivity(int id);
    Task<ActionResult<List<ActivityDto>>> GetActivitiesByUserId(String uid);
    Task<IActionResult> CreateActivity(CreateActivityDto newActivity);
}