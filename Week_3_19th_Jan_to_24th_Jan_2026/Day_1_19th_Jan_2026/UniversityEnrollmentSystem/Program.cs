using System.Collections.Generic;

namespace UniversityEnrollmentSystem
{
    class Program
    {
        static void Main()
        {
            Student s1 = new Student(1, "Harsh", "harsh@mail.com");
            Professor p1 = new Professor(101, "Dr. Kumar", "kumar@mail.com");
            Staff st1 = new Staff(201, "Admin", "admin@mail.com", "Office Staff");

            Course c1 = new Course(501, "Data Structures");
            Course c2 = new Course(502, "Operating Systems");

            Department d1 = new Department("Computer Science");
            d1.AddCourse(c1);
            d1.AddCourse(c2);

            p1.AssignCourse(c1);
            s1.EnrollCourse(c1);

            s1.DisplayProfile();
            s1.ViewSchedule();

            p1.DisplayProfile();
            p1.ViewSchedule();

            st1.DisplayProfile();
            st1.DisplayRole();

            d1.ShowCourses();
        }
    }
}
