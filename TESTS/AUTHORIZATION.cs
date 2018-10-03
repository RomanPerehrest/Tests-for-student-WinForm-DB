using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTS
{
    public partial class AUTHORIZATION : Form
    {
        
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            using (AddStudent f = new AddStudent())
            {
                f.ShowDialog();
            }
        }




        private DataAccess _dal;     

        public AUTHORIZATION()
        {
            InitializeComponent();
            _dal = new DataAccess();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student student = null;
            try
            {
                student = _dal.Authorization(textBox1.Text, textBox2.Text);
                //if (student != null)
                //{
                //    Hide();
                //    using (MAIN_FORM f = new MAIN_FORM())
                //    {
                //        f.ShowDialog();
                //    }
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show("Помилка авторизації. Детальна інформація: " +
                                exception.Message, "Авторизація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (student == null)
                MessageBox.Show("Ви ввели не вірний логін/пароль.", "Авторизація", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            else
                {
                    MAIN_FORM StudentForm = new MAIN_FORM(student);
                    Thread thread = new Thread(() => StudentForm.ShowDialog());
                    thread.Start();
                    Close();
                }
            }
        }

       
    }
