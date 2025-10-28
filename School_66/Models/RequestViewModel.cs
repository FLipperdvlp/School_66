public class RequestViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; }= string.Empty;

    public string Description { get; set; }= string.Empty;
}