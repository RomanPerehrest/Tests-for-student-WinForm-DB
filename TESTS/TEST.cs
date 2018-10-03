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
    public partial class TEST : Form
    {
        private int currentQuestionIdx;

        private Student studentc;
        private Subject srydjects;

        private int correctAnswersCount;
        private int incorrectAnswerCount;
        private List<Question> questions;
        private List<Histoty> His;


        public TEST(List<Question> questions, Student srydic, Subject srybject) : this()
        {
            

            studentc = srydic;
            srydjects = srybject;

            this.questions = questions;
            currentQuestionIdx = -1;
            MoveToNextQuestion();

        }

        private void MoveToNextQuestion()
        {
            currentQuestionIdx++;

            if (currentQuestionIdx > questions.Count - 1)
            {
                SaveUserAnswer();
                ShowResults();
                button2.Visible = true;
                groupBox2.Visible = false;
            }
            else
            {
                SaveUserAnswer();
                ShowQuestion(questions[currentQuestionIdx]);
            }
            
        }

        private void ShowResults()
        {
            lblQuestionText.Text = "ТЕСТ ПРОЙДЕНИЙ \n";
            lblQuestionText.Text += string.Format(" \n Кількість правильних відповідей ={0}\n" + "\nКількість не правильних відповідей ={1}\n", correctAnswersCount, incorrectAnswerCount);

            radioButton1.Text = String.Empty;
            radioButton1.Checked = false;            
            radioButton1.Tag = null;
            radioButton1.Visible = false;

            radioButton2.Text = String.Empty;
            radioButton2.Checked = false;
            radioButton2.Tag = null;
            radioButton2.Visible = false;

            radioButton3.Text = String.Empty;
            radioButton3.Checked = false;
            radioButton3.Tag = null;
            radioButton3.Visible = false;

        }

        private void SaveUserAnswer()
        {
            int prevQuestionIdx = currentQuestionIdx - 1;

            if (prevQuestionIdx >= 0)
            {
                object answer = null;
                if (radioButton1.Checked)
                    answer = radioButton1.Tag;
                else if (radioButton2.Checked)
                    answer = radioButton2.Tag;
                else if (radioButton3.Checked)
                    answer = radioButton3.Tag;

                if (questions[prevQuestionIdx].IsAnswerCorrect(answer as Question.Answer))
                    correctAnswersCount++;
                else
                    incorrectAnswerCount++;
            }
           
        }

        private void ShowQuestion(Question question)
        {
            lblQuestionText.Text = question.QuestionText;
            radioButton1.Text = question.Answers[0].Text;
            radioButton1.Checked = false;
            radioButton1.Tag = question.Answers[0];

            radioButton2.Text = question.Answers[1].Text;
            radioButton2.Checked = false;
            radioButton2.Tag = question.Answers[1];

            radioButton3.Text = question.Answers[2].Text;
            radioButton3.Checked = false;
            radioButton3.Tag = question.Answers[2];

           
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            using (RESULT a = new RESULT(His, srydjects, studentc))
            {
                a.ShowDialog();
            }
        }



       Student Student { get; set; }
        public TEST(Student stud)
        {
            InitializeComponent();
            Text = "Студент " + stud.FullName;
            Student = stud;
        }

        public TEST()
        {
            InitializeComponent();
            His = new List<Histoty>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Question.Answer Queanswer;
            if (radioButton1.Checked)
            {
                Queanswer = (Question.Answer)radioButton1.Tag;
            }
            else if (radioButton2.Checked) Queanswer = (Question.Answer)radioButton2.Tag;
            else if (radioButton3.Checked) Queanswer = (Question.Answer)radioButton3.Tag;

            else
            { MessageBox.Show("Виберіть відповідь");
                return;
            }

            Histoty histor = new Histoty();
            histor.question = questions[currentQuestionIdx];
            histor.QueAns = Queanswer;

            His.Add(histor);

            MoveToNextQuestion();
        }
    }
}
