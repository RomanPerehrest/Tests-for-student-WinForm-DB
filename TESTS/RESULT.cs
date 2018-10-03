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
    public partial class RESULT : Form
    {

        

        public RESULT(List<Histoty> hisss, Subject sab, Student stub)
        {
            InitializeComponent();
            double result = 0;
            double maxRating = hisss.Select(x => x.question).Sum(x => x.Rating);
                
            foreach (var item in hisss)
            {
                if (item.QueAns.IsCorrect)
                {
                    result += item.question.Rating;
                }        
            }

            result = (result / maxRating) * 100;
            label1.Text += " " + result;
            if (result < 80)
            {
                label2.Text = "Вам треба підучити цей предмет";
            }
            else
            {
                label2.Text = "Ви цей предмет засвоїли";
            }


            Result res = new Result();
            res.Subject_id = sab.Subject_id;
            res.Student_id = stub.Student_id;
            res.Ocinka = result;

            DataAccess dav = new DataAccess();
            dav.AddResult(res);
        }



        



        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            using (AUTHORIZATION A = new AUTHORIZATION())
            {
                A.ShowDialog();
            }
        }

     

    }
}
