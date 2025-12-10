using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Application.Interfaces;

public interface IPdfService
{
    byte[] GenerateEmployeeResume(Employee employee);
}
