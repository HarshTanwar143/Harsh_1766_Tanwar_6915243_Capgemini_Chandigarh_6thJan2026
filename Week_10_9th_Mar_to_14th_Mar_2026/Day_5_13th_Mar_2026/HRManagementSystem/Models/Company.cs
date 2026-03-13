namespace HRManagementSystem.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Industry { get; set; }

        // One company has many employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}