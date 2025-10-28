using System.ComponentModel.DataAnnotations;

public class Request
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty; 

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;  

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty; 

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public string UserEmail { get; set; } = string.Empty;
}