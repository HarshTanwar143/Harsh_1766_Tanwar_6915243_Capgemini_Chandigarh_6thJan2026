using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem
{
    internal class Doctor : Person
    {
        private string specialization;

        public Doctor(int id, string name, string specialization)
            : base(id, name)
        {
            this.specialization = specialization;
        }

        public string Specialization { get { return specialization; } }
    }
}
