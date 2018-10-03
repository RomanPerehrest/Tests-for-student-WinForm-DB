using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTS
{
    public partial class MAIN_FORM : Form
    {
        private DataAccess _dal = new DataAccess();
        public MAIN_FORM(Student studic)
        {
            strudic = studic;
            InitializeComponent();
            lblUser.Text = studic.FullName;
            
            listBoxSubjects.Items.Clear();
        }
        public Student strudic;


        private void btnStartTest_Click(object sender, EventArgs e)
        {
            Subject subject = listBoxSubjects.SelectedItem as Subject;

            if (subject != null)
            {
                List<Question> questions = _dal.GetQuestionsForSubject(subject);
                using (var frm = new TEST(questions, strudic, subject))
                {
                    frm.ShowDialog(this);
                }
            }
        }

        private void MAIN_FORM_Load(object sender, EventArgs e)
        {
            List<Subject> subjs = _dal.GetSubjects();
            foreach (var sub in subjs)
                listBoxSubjects.Items.Add(sub);
        }

        private void listBoxSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var warvar = _dal.GetStudentResult(strudic.Student_id, ((Subject)listBoxSubjects.SelectedItem).Subject_id);
            if (warvar.Count == 0)
            {
                label4.Text = "0";
                label5.Text = "Тест непроходився";
                return;
            }
            double osinca = warvar.Max(x => x.Ocinka);
            label4.Text = osinca + "%";
                if (osinca < 80)
                {
                    label5.Text = "Вам треба підучити цей предмет";
                }
                else
                {
                    label5.Text = "Ви цей предмет засвоїли";
                } 
                
        }
    }
}
