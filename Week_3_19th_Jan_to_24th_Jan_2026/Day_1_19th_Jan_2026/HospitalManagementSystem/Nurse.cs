using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem
{
    internal class Nurse : Person
    {
        private string department;

        public Nurse(int id, string name, string department)
            : base(id, name)
        {
            this.department = department;
        }

        public string Department { get { return department; } }
    }
}
