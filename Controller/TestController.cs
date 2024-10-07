using Job.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controller;

public class TestController : ControllerBase
{
    private readonly ITestService _service;
    public TestController(ITestService service)
    {
        _service = service;
    }
}