using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PatientCaseStudy
{
    internal class PatientBO
    {
        public void DisplayPatientDetails(List<Patient> patientList, string name)
        {
            var result = patientList.Where(p => p.Name == name).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("Patient named " + name + " not found");
            }
            else
            {
                Console.WriteLine("Name                 Age   Illness          City");
                foreach (var p in result)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }

        public void DisplayYoungestPatientDetails(List<Patient> patientList)
        {
            int minAge = patientList.Min(p => p.Age);
            var patient = patientList.First(p => p.Age == minAge);

            Console.WriteLine("The Youngest Patient Details");
            Console.WriteLine("Name                 Age   Illness          City");
            Console.WriteLine(patient.ToString());
        }

        public void DisplayPatientsFromCity(List<Patient> patientList, string cname)
        {
            var result = patientList.Where(p => p.City == cname).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("City named " + cname + " not found");
            }
            else
            {
                Console.WriteLine("Name                 Age   Illness          City");
                foreach (var p in result)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }
    }
}
