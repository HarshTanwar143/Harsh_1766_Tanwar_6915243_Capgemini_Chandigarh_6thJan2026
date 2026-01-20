using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityEnrollmentSystem
{
    internal class Student : Person
    {
        private List<Course> courses = new List<Course>();

        public Student(int id, string name, string email) : base(id, name, email) { }

        public void EnrollCourse(Course course)
        {
            courses.Add(course);
        }

        public void ViewSchedule()
        {
            foreach (var c in courses)
                Console.WriteLine(c.CourseName);
        }
    }

}
