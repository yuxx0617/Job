using Job.ImportModel;
using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controller;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ITestService _service;
    public TestController(ITestService service)
    {
        _service = service;
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpPost(nameof(CreateTest))]
    public IActionResult CreateTest(CreateTestImportModel createTest)
    {
        var result = _service.CreateTest(createTest);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpPut(nameof(EditTest))]
    public IActionResult EditTest(EditTestImportModel editTest)
    {
        var result = _service.EditTest(editTest);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete(nameof(DeleteTest))]
    public IActionResult DeleteTest(DeleteTestImportModel deleteTest)
    {
        var result = _service.DeleteTest(deleteTest.t_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpGet(nameof(TestList))]
    public IActionResult TestList()
    {
        var result = _service.TestList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpPost(nameof(CreateSeletion))]
    public IActionResult CreateSeletion(CreateSeletionImportModel createSeletion)
    {
        var result = _service.CreateSeletion(createSeletion);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpPut(nameof(EditSeletion))]
    public IActionResult EditSeletion(EditSeletionImportModel editSeletion)
    {
        var result = _service.EditSeletion(editSeletion);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete(nameof(DeleteSeletion))]
    public IActionResult DeleteSeletion(DeleteSeletionImportModel deleteSeletion)
    {
        var result = _service.DeleteSeletion(deleteSeletion.ts_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpPost(nameof(GetTest))]
    public IActionResult GetTest(GetTestImportModel getTest)
    {
        var result = _service.GetTest(getTest.t_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [Authorize]
    [HttpPost(nameof(GetTestSeletion))]
    public IActionResult GetTestSeletion(GetTestSeletionImportModel getTestSeletion)
    {
        var result = _service.GetTestSeletion(getTestSeletion.t_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}