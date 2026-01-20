using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityEnrollmentSystem
{
    internal class Course
    {
        private int courseId;
        private string courseName;
        private Professor professor;

        public Course(int courseId, string courseName)
        {
            this.courseId = courseId;
            this.courseName = courseName;
        }

        public string CourseName { get { return courseName; } }

        public void AssignProfessor(Professor professor)
        {
            this.professor = professor;
        }
    }
}
