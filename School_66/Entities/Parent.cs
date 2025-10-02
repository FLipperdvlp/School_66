namespace School_66.Entities
{
    public class Parent
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;

        public string ChildFullName { get; set; } = string.Empty;
        public string ChildClass { get; set; } = string.Empty;

        public string ContactMethod { get; set; } = string.Empty;
        public string RequestText { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
