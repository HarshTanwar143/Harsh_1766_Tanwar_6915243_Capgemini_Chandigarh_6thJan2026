using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace EmployeeCaseStudy
{
    internal class EmployeeBO
    {
        public void DisplayEmployeeDetails(List<Employee> employeeList, string name)
        {
            var result = employeeList.Where(e => e.Name == name).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("Employee named " + name + " not found");
            }
            else
            {
                Console.WriteLine("Name                 Age   Designation          City");
                foreach (var e in result)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void DisplayYoungestEmployeeDetails(List<Employee> employeeList)
        {
            int minAge = employeeList.Min(e => e.Age);
            var emp = employeeList.First(e => e.Age == minAge);

            Console.WriteLine("The Youngest Employee Details");
            Console.WriteLine("Name                 Age   Designation          City");
            Console.WriteLine(emp.ToString());
        }

        public void DisplayEmployeesFromCity(List<Employee> employeeList, string cname)
        {
            var result = employeeList.Where(e => e.City == cname).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("City named " + cname + " not found");
            }
            else
            {
                Console.WriteLine("Name                 Age   Designation          City");
                foreach (var e in result)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
