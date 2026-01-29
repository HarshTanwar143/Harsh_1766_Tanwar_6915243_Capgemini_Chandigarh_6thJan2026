namespace EmployeeCaseStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = new List<Employee>();

            Console.WriteLine("Enter the number of employees");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter employee " + (i + 1) + " details:");
                Console.WriteLine("Enter the name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the age");
                int age = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the designation");
                string designation = Console.ReadLine();

                Console.WriteLine("Enter the city");
                string city = Console.ReadLine();

                employeeList.Add(new Employee(name, age, designation, city));
            }

            EmployeeBO employeeBO = new EmployeeBO();
            string opt;

            do
            {
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1)Display Employee Details");
                Console.WriteLine("2)Display Youngest Employee Details");
                Console.WriteLine("3)Display Employees from City");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter employee name:");
                        employeeBO.DisplayEmployeeDetails(employeeList, Console.ReadLine());
                        break;

                    case 2:
                        employeeBO.DisplayYoungestEmployeeDetails(employeeList);
                        break;

                    case 3:
                        Console.WriteLine("Enter city");
                        employeeBO.DisplayEmployeesFromCity(employeeList, Console.ReadLine());
                        break;
                }

                Console.WriteLine("Do you want to continue(Yes/No)?");
                opt = Console.ReadLine();

            } while (opt.Equals("Yes"));
        }
    }
}
