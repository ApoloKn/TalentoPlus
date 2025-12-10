using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("departments")]
    public IActionResult GetDepartments()
    {
        // Public endpoint
        // Ideally should come from DepartmentService, but mocking for speed
        return Ok(new[] { "IT", "HR", "Sales", "Marketing" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateEmployeeDto dto)
    {
        await _employeeService.CreateEmployeeAsync(dto);
        return Ok("Registration successful. Check your email.");
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyInfo()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim == null || !int.TryParse(idClaim.Value, out int id)) return Unauthorized();

        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound();

        return Ok(new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Position = employee.Position.ToString(),
            DepartmentName = employee.Department?.Name ?? "N/A"
        });
    }

    [Authorize]
    [HttpGet("resume")]
    public async Task<IActionResult> DownloadResume()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim == null || !int.TryParse(idClaim.Value, out int id)) return Unauthorized();

        try
        {
            var pdfBytes = await _employeeService.DownloadResumeAsync(id);
            return File(pdfBytes, "application/pdf", "resume.pdf");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
