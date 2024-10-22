using Job.ImportModel;
using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controller;

[ApiController]
[Route("api/[controller]")]
public class ActRecordController : ControllerBase
{
    private readonly IActRecordService _service;
    public ActRecordController(IActRecordService service)
    {
        _service = service;
    }
    [HttpGet(nameof(ActRecordList))]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult ActRecordList()
    {
        var result = _service.ActRecordList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(GetActRecord))]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult GetActRecord(GetActRecordImportModel getActRecord)
    {
        var result = _service.GetActRecord(getActRecord.r_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetHotRecord))]
    public IActionResult GetHotRecord()
    {
        var result = _service.GetHotRecord();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}
