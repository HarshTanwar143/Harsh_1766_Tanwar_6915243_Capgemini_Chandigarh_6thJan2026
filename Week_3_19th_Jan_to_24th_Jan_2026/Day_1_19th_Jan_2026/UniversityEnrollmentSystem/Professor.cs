using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityEnrollmentSystem
{
    internal class Professor : Person
    {
        private List<Course> courses = new List<Course>();

        public Professor(int id, string name, string email) : base(id, name, email) { }

        public void AssignCourse(Course course)
        {
            courses.Add(course);
            course.AssignProfessor(this);
        }

        public void ViewSchedule()
        {
            foreach (var c in courses)
                Console.WriteLine(c.CourseName);
        }
    }
}
