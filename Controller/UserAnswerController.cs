using Job.ImportModel;
using Job.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserAnswerController : ControllerBase
{
    private readonly IUserAnswerService _service;
    public UserAnswerController(IUserAnswerService service)
    {
        _service = service;
    }

    [HttpPost(nameof(CreateAnswer))]
    public IActionResult CreateAnswer(CreateAnswerImportModel createAnswer)
    {
        var result = _service.CreateAnswer(createAnswer);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(CountGrade))]
    public IActionResult CountGrade(CountGradeImportModel countGrade)
    {
        var result = _service.CountGrade(countGrade.ua_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(CreateJob))]
    public IActionResult CreateJob(CreateJobImportModel createJob)
    {
        var result = _service.CreateJob(createJob);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }

}