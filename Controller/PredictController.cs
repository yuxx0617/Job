using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PredictController : ControllerBase
{
    private readonly IPredictService _service;

    public PredictController(IPredictService service)
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
    [HttpGet(nameof(UpdatePredict))]
    public IActionResult UpdatePredict()
    {
        var result = _service.UpdatePredict();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(PredictCsv))]
    public IActionResult PredictCsv()
    {
        var result = _service.PredictCsv();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}
