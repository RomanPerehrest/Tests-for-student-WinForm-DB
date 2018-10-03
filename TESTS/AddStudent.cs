using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;


namespace TESTS
{
    public partial class AddStudent : Form
    {

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            using (AUTHORIZATION a = new AUTHORIZATION())
            {
                a.ShowDialog();
            }
        }


        private DataAccess _dal;
        

        public AddStudent()
        {
            InitializeComponent();
            _dal = new DataAccess();
        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == String.Empty || textBox2.Text.Trim() == String.Empty ||
                    textBox3.Text.Trim() == String.Empty || textBox6.Text.Trim() == String.Empty ||
                    textBox7.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("Ви не ввели обов'язкову інформацію про студента.", "Реєстрація студента",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Student stud = new Student();

                    stud.Last_name = textBox1.Text.Trim();
                    stud.First_name = textBox2.Text.Trim();
                    stud.Middle_name = textBox3.Text.Trim();
                    stud.Number = textBox4.Text.Trim() == String.Empty ? null : textBox4.Text;
                    stud.Email = textBox5.Text.Trim() == String.Empty ? null : textBox5.Text;
                    stud.Login = textBox6.Text.Trim();
                    stud.Password = textBox7.Text.Trim();

                    _dal.AddStudent(stud);

                    MessageBox.Show("Запис додано.", "Додавання студента",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Помилка додання студента в базу даних. Детальна інформація: " +
                                exception.Message, "Виведення...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
