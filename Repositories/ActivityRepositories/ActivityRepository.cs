using Microsoft.AspNetCore.Mvc;
using payment.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using payment.DatabaseContext;
using payment.model;

namespace payment.Repositories.ActivityRepositories;

public class ActivityRepository : IActivityRepository

{
    private readonly PayMentDbContext? _dbContext;
    private readonly IMapper? _mapper;

    public ActivityRepository(IMapper mapper, PayMentDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ActionResult<List<ActivityDto>>> GetAllActivities()
    {
        List<Activity> activities = await _dbContext!.Activities!.ToListAsync();
        return _mapper!.Map<List<ActivityDto>>(activities);
    }

    public async Task<ActionResult<ActivityDto>> GetActivity(int id)
    {
        Activity? activity =
            await _dbContext!.Activities!.FirstOrDefaultAsync(x => x.Id == id);
        if (activity == null)
            return new NotFoundResult();
        return _mapper!.Map<ActivityDto>(activity);
    }

    public async Task<ActionResult<List<ActivityDto>>> GetActivitiesByUserId(string uid)
    {
        List<Activity>? activities = await _dbContext!.Activities!.Where(x => x.UserIdentifier == uid)
            .ToListAsync();
        return _mapper!.Map<List<ActivityDto>>(activities);
    }

    public async Task<IActionResult> CreateActivity(CreateActivityDto newActivity)
    {
        Activity createdActivity = _mapper!.Map<Activity>(newActivity);
        _dbContext!.Add(createdActivity);
        await _dbContext.SaveChangesAsync();
        ActivityDto activityDto = _mapper!.Map<ActivityDto>(createdActivity);
        return new CreatedResult("GetActivity", activityDto);
    }
}