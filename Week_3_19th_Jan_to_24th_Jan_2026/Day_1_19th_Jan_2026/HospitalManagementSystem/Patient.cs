using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem
{
    internal class Patient : Person
    {
        private List<MedicalRecord> records = new List<MedicalRecord>();

        public Patient(int id, string name) : base(id, name) { }

        public void AddRecord(MedicalRecord record)
        {
            records.Add(record);
        }

        public List<MedicalRecord> Records { get { return records; } }
    }
}
