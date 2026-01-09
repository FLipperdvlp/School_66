using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = null!; 
    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = null!; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    [MaxLength(50)]
    public string? Role { get; set; } 
    [MaxLength(50)]
    public string? AuthProvider { get; set; }
}