using Job.ImportModel;
using Job.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }
    [HttpPost(nameof(EmailValid))]
    public IActionResult EmailValid(EmailValidImportModel emailValid)
    {
        var result = _service.EmailValid(emailValid);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(Register))]
    public IActionResult Register(RegisterImportModel register)
    {
        var result = _service.Register(register);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(Validate))]
    public IActionResult Validate(string account, string emailValid, string expires)
    {
        var result = _service.Validate(account, emailValid, expires);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(Login))]
    public IActionResult Login(LoginImportModel login)
    {
        var result = _service.Login(login);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPut(nameof(EditUser))]
    public IActionResult EditUser(EditUserImportModel editUser)
    {
        var result = _service.EditUser(editUser);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost(nameof(ForgetPassword))]
    public IActionResult ForgetPassword(ForgetPasswordImportModel forgetPassword)
    {
        var result = _service.ForgetPassword(forgetPassword);
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpGet(nameof(UserList))]
    public IActionResult UserList()
    {
        var result = _service.UserList();
        if (result.isSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}