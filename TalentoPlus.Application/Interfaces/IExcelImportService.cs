using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Application.Interfaces;

public interface IExcelImportService
{
    IEnumerable<Employee> ParseEmployees(Stream excelStream);
}
