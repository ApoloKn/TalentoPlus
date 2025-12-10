using System.ComponentModel.DataAnnotations;
using TalentoPlus.Domain.Enums;

namespace TalentoPlus.Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string IdentificationNumber { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public JobPosition Position { get; set; }
    public EmploymentStatus Status { get; set; }
    public EducationLevel Education { get; set; }

    [MaxLength(500)]
    public string ProfileSummary { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    // Foreign Key
    public int DepartmentId { get; set; }
    
    // Navigation Property
    public Department? Department { get; set; }
}
