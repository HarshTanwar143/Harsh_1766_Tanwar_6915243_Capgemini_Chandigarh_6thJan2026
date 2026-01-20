using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityEnrollmentSystem
{
    internal class Person
    {
        private int id;
        private string name;
        private string email;

        public Person(int id, string name, string email)
        {
            this.id = id;
            this.name = name;
            this.email = email;
        }

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public string Email { get { return email; } }

        public virtual void DisplayProfile()
        {
            Console.WriteLine($"ID: {id}, Name: {name}, Email: {email}");
        }
    }

}
