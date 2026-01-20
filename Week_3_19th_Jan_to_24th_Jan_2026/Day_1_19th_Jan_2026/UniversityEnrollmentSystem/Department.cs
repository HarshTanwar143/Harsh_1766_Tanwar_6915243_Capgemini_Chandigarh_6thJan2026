using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityEnrollmentSystem
{
    internal class Department
    {
        private string departmentName;
        private List<Course> courses = new List<Course>();

        public Department(string departmentName)
        {
            this.departmentName = departmentName;
        }

        public void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public void ShowCourses()
        {
            foreach (var c in courses)
                Console.WriteLine(c.CourseName);
        }
    }
}
