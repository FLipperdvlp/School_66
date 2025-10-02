using System.ComponentModel.DataAnnotations;

public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!; // Полное имя пользователя

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!; // Email для входа

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = null!; // Пароль (рекомендуется хэшировать)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Дата создания учетной записи

        [MaxLength(50)]
        public string? Role { get; set; } // Роль пользователя (например, "Student", "Parent", "Admin")
    }