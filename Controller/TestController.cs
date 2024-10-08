using Job.ImportModel;
using Job.Service.Interface;
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
    [HttpPost(nameof(CreateTest))]
    public IActionResult CreateTest(CreateTestImportModel createTest)
    {
        var result = _service.CreateTest(createTest);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPut(nameof(EditTest))]
    public IActionResult EditTest(EditTestImportModel editTest)
    {
        var result = _service.EditTest(editTest);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpDelete(nameof(DeleteTest))]
    public IActionResult DeleteTest(DeleteTestImportModel deleteTest)
    {
        var result = _service.DeleteTest(deleteTest);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(TestList))]
    public IActionResult TestList()
    {
        var result = _service.TestList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(CreateSeletion))]
    public IActionResult CreateSeletion(CreateSeletionImportModel createSeletion)
    {
        var result = _service.CreateSeletion(createSeletion);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPut(nameof(EditSeletion))]
    public IActionResult EditSeletion(EditSeletionImportModel editSeletion)
    {
        var result = _service.EditSeletion(editSeletion);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpDelete(nameof(DeleteSeletion))]
    public IActionResult DeleteSeletion(DeleteSeletionImportModel deleteSeletion)
    {
        var result = _service.DeleteSeletion(deleteSeletion.ts_id);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetTest))]
    public IActionResult GetTest(GetTestImportModel getTest)
    {
        var result = _service.GetTest(getTest);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetTestSeletion))]
    public IActionResult GetTestSeletion(GetTestSeletionImportModel getTestSeletion)
    {
        var result = _service.GetTestSeletion(getTestSeletion);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}