using TalentoPlus.Application.DTOs;
using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task CreateEmployeeAsync(CreateEmployeeDto dto);
    Task ImportEmployeesFromExcelAsync(Stream fileStream);
    Task<byte[]> DownloadResumeAsync(int employeeId);
}
