using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem
{
    internal class MedicalRecord
    {
        private DateTime recordDate;
        private string diagnosis;

        public MedicalRecord(DateTime recordDate, string diagnosis)
        {
            this.recordDate = recordDate;
            this.diagnosis = diagnosis;
        }

        public DateTime RecordDate { get { return recordDate; } }
        public string Diagnosis { get { return diagnosis; } }
    }
}
