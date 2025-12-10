using System.ComponentModel.DataAnnotations;

namespace TalentoPlus.Domain.Entities;

public class Department
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
