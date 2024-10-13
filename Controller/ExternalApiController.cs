using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly ExternalApiService _service;

    public BusinessController(ExternalApiService service)
    {
        _service = service;
    }

    [HttpGet(nameof(GetCompanyData))]
    public async Task<IActionResult> GetCompanyData()
    {
        var result = await _service.GetCompanyData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetBranchCompanyData))]
    public async Task<IActionResult> GetBranchCompanyData()
    {
        var result = await _service.GetBranchCompanyData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetForeignCompanyData))]
    public async Task<IActionResult> GetForeignCompanyData()
    {
        var result = await _service.GetForeignCompanyData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetBusinessData))]
    public async Task<IActionResult> GetBusinessData()
    {
        var result = await _service.GetBusinessData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetCompanyDisbandData))]
    public async Task<IActionResult> GetCompanyDisbandData()
    {
        var result = await _service.GetCompanyDisbandData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetBranchCompanyAbolishData))]
    public async Task<IActionResult> GetBranchCompanyAbolishData()
    {
        var result = await _service.GetBranchCompanyAbolishData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetStopBusinessData))]
    public async Task<IActionResult> GetStopBusinessData()
    {
        var result = await _service.GetStopBusinessData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(GetLtdDisbandData))]
    public async Task<IActionResult> GetLtdDisbandData()
    {
        var result = await _service.GetLtdDisbandData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}
