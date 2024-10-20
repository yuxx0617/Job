using Job.ImportModel;
using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controller;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _service;
    public JobController(IJobService service)
    {
        _service = service;
    }
    [HttpPost(nameof(CreateJob))]
    public IActionResult CreateJob(CreateFileImportModel createJob)
    {
        var result = _service.CreateJob(createJob);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(GetJob))]
    public IActionResult GetJob(GetJobImportModel getJob)
    {
        var result = _service.GetJob(getJob.j_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(UpdateJobContent))]
    public async Task<IActionResult> UpdateJobContent()
    {
        var result = await _service.UpdateJobContent();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(CreateLesson))]
    public IActionResult CreateLesson(CreateFileImportModel createLesson)
    {
        var result = _service.CreateLesson(createLesson);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(CreateCertificate))]
    public IActionResult CreateCertificate(CreateFileImportModel createCertificate)
    {
        var result = _service.CreateCertificate(createCertificate);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(CreateSubsidy))]
    public IActionResult CreateSubsidy(CreateFileImportModel createSubsidy)
    {
        var result = _service.CreateSubsidy(createSubsidy);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpGet(nameof(LessonList))]
    public IActionResult LessonList()
    {
        var result = _service.LessonList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpGet(nameof(CertificateList))]
    public IActionResult CertificateList()
    {
        var result = _service.CertificateList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpGet(nameof(SubsidyList))]
    public IActionResult SubsidyList()
    {
        var result = _service.SubsidyList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}