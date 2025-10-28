using System.ComponentModel.DataAnnotations;

namespace School_66.Entities;

public class StudentForm
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = "";

    [Required, MaxLength(50)]
    public string Title { get; set; } = "";

    [Required, MaxLength(50)]
    public string Type { get; set; } = "";

    [Required, MaxLength(50)]
    public string Status { get; set; } = "";


    [Required, MaxLength(50)]
    public string LastName { get; set; } = "";

    [Required, MaxLength(20)]
    public string ClassName { get; set; } = "";

    [Required, MaxLength(50)]
    public string ContactMethod { get; set; } = ""; // Telegram / Email / Phone

    [Required]
    public string RequestText { get; set; } = "";

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public string UserEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}