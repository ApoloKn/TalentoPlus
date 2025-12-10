using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;

namespace TalentoPlus.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IExcelImportService _excelImportService;
    private readonly IPdfService _pdfService;
    private readonly IEmailService _emailService;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IExcelImportService excelImportService,
        IPdfService pdfService,
        IEmailService emailService)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _excelImportService = excelImportService;
        _pdfService = pdfService;
        _emailService = emailService;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Position = e.Position.ToString(),
            DepartmentName = e.Department?.Name ?? "N/A"
        });
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            IdentificationNumber = dto.IdentificationNumber,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId,
            Position = dto.Position,
            Status = dto.Status,
            Education = dto.Education,
            HireDate = DateTime.UtcNow
        };

        await _employeeRepository.AddAsync(employee);
        await _emailService.SendEmailAsync(employee.Email, "Welcome", "Welcome to TalentoPlus!");
    }

    public async Task ImportEmployeesFromExcelAsync(Stream fileStream)
    {
        var employees = _excelImportService.ParseEmployees(fileStream);
        foreach (var employee in employees)
        {
            // Simple logic: default department if not found, or use Name to lookup
            // For now, assuming department exists or assigning 1
            if (employee.DepartmentId == 0) employee.DepartmentId = 1; 
            
            await _employeeRepository.AddAsync(employee);
        }
    }

    public async Task<byte[]> DownloadResumeAsync(int employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null) throw new Exception("Employee not found");
        return _pdfService.GenerateEmployeeResume(employee);
    }
}
