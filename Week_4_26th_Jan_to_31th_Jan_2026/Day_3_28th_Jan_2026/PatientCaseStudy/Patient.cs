using System;
using System.Collections.Generic;
using System.Text;

namespace PatientCaseStudy
{
    internal class Patient
    {
        private string _name;
        private int _age;
        private string _illness;
        private string _city;

        public Patient()
        {
        }

        public Patient(string name, int age, string illness, string city)
        {
            _name = name;
            _age = age;
            _illness = illness;
            _city = city;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string Illness
        {
            get { return _illness; }
            set { _illness = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public override string ToString()
        {
            return string.Format("{0,-21}{1,-6}{2,-17}{3,-20}", _name, _age, _illness, _city);
        }
    }
}
