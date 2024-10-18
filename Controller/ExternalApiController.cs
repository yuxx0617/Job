using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExternalApiController : ControllerBase
{
    private readonly IExternalApiService _service;

    public ExternalApiController(IExternalApiService service)
    {
        _service = service;
    }

    [HttpGet(nameof(UpdateCompanyDisbandType))]
    public async Task<IActionResult> UpdateCompanyDisbandType()
    {
        var result = await _service.UpdateCompanyDisbandType();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(UpdateBranchCompanyAbolishType))]
    public async Task<IActionResult> UpdateBranchCompanyAbolishType()
    {
        var result = await _service.UpdateBranchCompanyAbolishType();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(UpdateStopBusinessType))]
    public async Task<IActionResult> UpdateStopBusinessType()
    {
        var result = await _service.UpdateStopBusinessType();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(UpdateVacanciesData))]
    public async Task<IActionResult> UpdateVacanciesData()
    {
        var result = await _service.UpdateVacanciesData();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}
