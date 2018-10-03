using System;
using System.Collections.Generic;

namespace TESTS
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
        public double Rating { get; set; }

        public class Answer
        {
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
        }

        internal bool IsAnswerCorrect(Answer userAnswer)
        {
            if (userAnswer == null)
            {
                return false;
            }
            foreach (Answer a in Answers)
            {
                if (a == userAnswer && a.IsCorrect)
                    return true;
            }
            return false;
        }
    }
}