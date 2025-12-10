using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Controllers;

[Authorize] // Require login
public class DashboardController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(IEmployeeService employeeService, ILogger<DashboardController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        
        // simple stats
        ViewData["TotalEmployees"] = employees.Count();
        ViewData["TotalDepartments"] = employees.Select(e => e.DepartmentName).Distinct().Count();
        // ViewData["OnVacation"] = ... needs status handling

        return View(employees);
    }

    [HttpPost]
    public async Task<IActionResult> AskAi([FromBody] string query)
    {
        // Mock AI response
        await Task.Delay(500); 
        return Json(new { answer = $"I understood your query about '{query}' but I am running in demo mode. Try asking 'How many employees?'" });
    }
}
