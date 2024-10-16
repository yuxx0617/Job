using Job.ImportModel;
using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [HttpPost(nameof(CreateAnswerAndCount))]
    public IActionResult CreateAnswerAndCount(CreateAnswerImportModel createAnswer)
    {
        var result = _service.CreateAnswerAndCount(createAnswer);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpPost(nameof(GetAnswerResult))]
    public IActionResult GetAnswerResult(CountGradeImportModel countGrade)
    {
        var result = _service.GetAnswerResult(countGrade.ua_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpGet(nameof(AnswerResultList))]
    public IActionResult AnswerResultList()
    {
        var result = _service.AnswerResultList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}