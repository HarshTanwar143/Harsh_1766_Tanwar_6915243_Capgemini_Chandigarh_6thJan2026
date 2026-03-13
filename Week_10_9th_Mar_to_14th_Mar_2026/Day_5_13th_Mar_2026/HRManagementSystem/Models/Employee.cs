namespace HRManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}