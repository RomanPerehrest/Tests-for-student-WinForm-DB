using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;

namespace TESTS
{
    public class Histoty
    {
        public Question question { get; set; }
        public Question.Answer QueAns { get; set; }
    }

    public class Result
    {
        public int Result_id { get; set; }
        public int Student_id { get; set; }
        public int Subject_id { get; set; }
        public double Ocinka { get; set; }

    }
    public class Subject
    {
        public int Subject_id { get; set; }
        public string Subject_name { get; set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Subject_name) ? base.ToString() : Subject_name;
        }

    }


    public class Student
    {
        public int Student_id { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string FullName
        {
            get { return ToString(); }
            set { FullNameToPart(value); }
        }

        private void FullNameToPart(string fullName)
        {
            string[] parts = fullName.Split(' ');
            Last_name = parts[0];
            First_name = parts[1];
            Middle_name = parts[2];
        }

        public override bool Equals(object obj)
        {
            Student stud = obj as Student;
            if (stud == null)
                return false;
            else return Student_id.Equals(stud.Student_id);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Last_name, First_name, Middle_name);
        }
    }
}
