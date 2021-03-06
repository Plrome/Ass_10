using System.ComponentModel.DataAnnotations;

namespace Ass_10.Models;

public class StudentViewModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? City { get; set; }
}
public class StudentCreateModel
{
    [Required, MaxLength(50)]
    public string? FirstName { get; set; }

    [Required, MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(20)]
    public string? City { get; set; }

    public string? State { get; set; }
}

