using TalentoPlus.Domain.Enums;

namespace TalentoPlus.Application.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public string DepartmentName { get; set; }
}

public class CreateEmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IdentificationNumber { get; set; }
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public JobPosition Position { get; set; }
    public EmploymentStatus Status { get; set; }
    public EducationLevel Education { get; set; }
}
