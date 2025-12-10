using ClosedXML.Excel;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Enums;

namespace TalentoPlus.Infrastructure.Services;

public class ExcelImportService : IExcelImportService
{
    public IEnumerable<Employee> ParseEmployees(Stream excelStream)
    {
        var employees = new List<Employee>();

        using var workbook = new XLWorkbook(excelStream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header

        foreach (var row in rows)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = row.Cell(1).GetValue<string>(),
                    LastName = row.Cell(2).GetValue<string>(),
                    Email = row.Cell(3).GetValue<string>(),
                    IdentificationNumber = row.Cell(4).GetValue<string>(),
                    Salary = row.Cell(5).GetValue<decimal>(),
                    HireDate = row.Cell(7).GetValue<DateTime>(),
                    // Position, Status, Education need parsing or mapping
                    // Department assigned later or inferred
                    PhoneNumber = row.Cell(11).GetValue<string>()
                };
                
                // Basic Enum parsing
                if (Enum.TryParse<JobPosition>(row.Cell(8).GetValue<string>(), true, out var position))
                    employee.Position = position;
                
                if (Enum.TryParse<EmploymentStatus>(row.Cell(9).GetValue<string>(), true, out var status))
                    employee.Status = status;

                if (Enum.TryParse<EducationLevel>(row.Cell(10).GetValue<string>(), true, out var education))
                    employee.Education = education;

                // Department Name (to be handled by caller or service to lookup ID)
                // For now, we return employee with DepartmentId = 0 and let business logic handle it
                // Or we can store the department name in a temporary field if we had a DTO. 
                // Alternatively, we can assume the caller will handle Department mapping using the name from cell 6.
                
                employees.Add(employee);
            }
            catch (Exception ex)
            {
                // Log error or continue
            }
        }

        return employees;
    }
}
