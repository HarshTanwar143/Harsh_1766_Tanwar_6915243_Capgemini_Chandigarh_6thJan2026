namespace HospitalManagementSystem
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Patient patient = new Patient(1, "Rahul");
            Doctor doctor = new Doctor(101, "Dr. Sharma", "Cardiology");
            Nurse nurse = new Nurse(201, "Anita", "ICU");

            Console.WriteLine("Patient Details");
            Console.WriteLine(patient.Id + " " + patient.Name);
            Console.WriteLine();

            Console.WriteLine("Doctor Details");
            Console.WriteLine(doctor.Id + " " + doctor.Name + " " + doctor.Specialization);
            Console.WriteLine();

            Appointment appointment = new Appointment(patient, doctor, DateTime.Now);

            Console.WriteLine("Appointment Scheduled");
            Console.WriteLine("Patient: " + appointment.Patient.Name);
            Console.WriteLine("Doctor: " + appointment.Doctor.Name);
            Console.WriteLine("Date: " + appointment.Date);
            Console.WriteLine();

            MedicalRecord record = new MedicalRecord(DateTime.Now, "High Blood Pressure");
            patient.AddRecord(record);

            Console.WriteLine("Medical History");
            foreach (var r in patient.Records)
                Console.WriteLine(r.RecordDate + " - " + r.Diagnosis);

            Console.ReadLine();
        }
    }
}
