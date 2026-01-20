using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem
{
    internal class Appointment
    {
        private Patient patient;
        private Doctor doctor;
        private DateTime date;

        public Appointment(Patient patient, Doctor doctor, DateTime date)
        {
            this.patient = patient;
            this.doctor = doctor;
            this.date = date;
        }

        public Patient Patient { get { return patient; } }
        public Doctor Doctor { get { return doctor; } }
        public DateTime Date { get { return date; } }
    }
}
