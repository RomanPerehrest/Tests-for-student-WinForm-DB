using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TESTS
{
    public class DataAccess
    {
        private readonly string ConnectionString;

        public DataAccess()
        {
            ConnectionString = @"Data Source=ROMAN\SQLEXPRESS;Initial Catalog=TESTS;Integrated Security=True";
        }


        public Student Authorization(string login, string password)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Authorization", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter sqlParameter = new SqlParameter("@Login", SqlDbType.NVarChar);
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlParameter.Value = login;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Password", SqlDbType.NVarChar);
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlParameter.Value = password;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlConnection.Open();
                        SqlDataReader dr = sqlCommand.ExecuteReader();
                        if (dr.Read())
                        {
                            Student studentWithAccess = new Student();
                            studentWithAccess.Student_id = (int)dr["Student_id"];
                            studentWithAccess.Last_name = (string)dr["Last_name"];
                            studentWithAccess.First_name = (string)dr["First_name"];
                            studentWithAccess.Middle_name = (string)dr["Middle_name"];
                            studentWithAccess.Number = dr["Number"] == DBNull.Value ? null : (string)dr["Number"];
                            studentWithAccess.Email = dr["Email"] == DBNull.Value ? null : (string)dr["Email"];


                            return studentWithAccess;
                        }
                        return null;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Проблема з підключенням до бази даних. Детальна інформація: " +
                                            ex.Message);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        internal List<Question> GetQuestionsForSubject(Subject subject)
        {
            List<Question> questions;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetAllQuestionsForSubject", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter sqlParameter = new SqlParameter("@subjectid", SqlDbType.Int);
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlParameter.Value = subject.Subject_id;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlConnection.Open();
                        using (SqlDataReader dr = sqlCommand.ExecuteReader())
                        {
                            questions = new List<Question>();
                            int currentId = 0;
                            Question currentQuestion = null;


                            while (dr.Read())
                            {
                                // 1 2+2 5 n
                                // 1 2+2 4 y
                                // 1 2+2 3 n

                                // 2 4+4 an1 n
                                // 2 4+4 an2 n
                                // 2 4+4 8 y

                                // 1 q1 an1 n
                                int qid = (int)dr[0];
                                if (currentQuestion == null || currentId != qid)
                                {                                    
                                    currentQuestion = new Question();
                                    currentQuestion.QuestionText = (string)dr[1];
                                    currentQuestion.Answers = new List<Question.Answer>();
                                    currentQuestion.Rating = (double)dr[4];
                                    currentId = qid;
                                    questions.Add(currentQuestion);
                                }                         
                                
                                var a = new Question.Answer();
                                a.Text = (string)dr[2];
                                a.IsCorrect = (bool)dr[3];
                                currentQuestion.Answers.Add(a);                               
                            }
                        }
                        return questions;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Проблема з підключенням до бази даних. Детальна інформація: " +
                                            ex.Message);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        public void AddStudent(Student user)
        {
            SqlTransaction sqlTransaction = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();
                    using (SqlCommand sqlCommand = new SqlCommand("AddStudents", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Transaction = sqlTransaction;


                        SqlParameter sqlParameter = new SqlParameter();

                        sqlParameter = new SqlParameter("@Last_Name", SqlDbType.NVarChar);
                        sqlParameter.Value = user.Last_name;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@First_Name", SqlDbType.NVarChar);
                        sqlParameter.Value = user.First_name;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Middle_name", SqlDbType.NVarChar);
                        sqlParameter.Value = user.Middle_name;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Number", SqlDbType.Char);
                        sqlParameter.Value = user.Number;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Email", SqlDbType.NVarChar);
                        sqlParameter.Value = user.Email;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Login", SqlDbType.NVarChar);
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlParameter.Value = user.Login;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Password", SqlDbType.NVarChar);
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlParameter.Value = user.Password;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (sqlTransaction != null) sqlTransaction.Rollback();
                    throw new Exception("Проблемма с подключением к базе данных. Дополнительная информация: " +
                                        ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public List<Student> GetStudents()
        {
            List<Student> Students;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetStudents", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlConnection.Open();
                        SqlDataReader dr = sqlCommand.ExecuteReader();

                        Students = new List<Student>();

                        while (dr.Read())
                        {
                            Student stud = new Student();
                            stud.Student_id = (int)dr["Student_id"];
                            stud.Last_name = (string)dr["Last_name"];
                            stud.First_name = (string)dr["First_name"];
                            stud.Middle_name = (string)dr["Middle_name"];
                            stud.Number = dr["Number"] == DBNull.Value ? null : (string)dr["Number"];
                            stud.Email = dr["Email"] == DBNull.Value ? null : (string)dr["Email"];

                            Students.Add(stud);
                        }
                        return Students;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Проблема з підключенням до бази даних. Детальна інформація: " +
                                            ex.Message);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subjects;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetAllSubjects", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlConnection.Open();
                        using (SqlDataReader dr = sqlCommand.ExecuteReader())
                        {
                            subjects = new List<Subject>();

                            while (dr.Read())
                            {
                                Subject subj = new Subject();
                                subj.Subject_id = (int)dr["Subject_id"];
                                subj.Subject_name = (string)dr["Subject_name"];
                                subjects.Add(subj);
                            }
                        }
                        return subjects;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Проблема з підключенням до бази даних. Детальна інформація: " +
                                            ex.Message);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        public void AddResult(Result user)
        {
            SqlTransaction sqlTransaction = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();
                    using (SqlCommand sqlCommand = new SqlCommand("AddResult", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Transaction = sqlTransaction;


                        SqlParameter sqlParameter = new SqlParameter();

                        sqlParameter = new SqlParameter("@Student_id", SqlDbType.Int);
                        sqlParameter.Value = user.Student_id;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Subject_id", SqlDbType.Int);
                        sqlParameter.Value = user.Subject_id;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Ocinka", SqlDbType.Float);
                        sqlParameter.Value = user.Ocinka;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (sqlTransaction != null) sqlTransaction.Rollback();
                    throw new Exception("Проблемма с подключением к базе данных. Дополнительная информация: " +
                                        ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        //public void GetTestCompleted()
        //{

        //}

        public List<Result> GetStudentResult(int Student_id, int Subject_id)
        {
            List<Result> StudRes;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetStudentResult", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter sqlParameter = new SqlParameter();

                        sqlParameter = new SqlParameter("@StudentId", SqlDbType.Int);
                        sqlParameter.Value = Student_id;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        sqlParameter = new SqlParameter("@Subjectid", SqlDbType.Int);
                        sqlParameter.Value = Subject_id;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlCommand.Parameters.Add(sqlParameter);

                        
                        sqlConnection.Open();
                        using (SqlDataReader dr = sqlCommand.ExecuteReader())
                        {
                            StudRes = new List<Result>();

                            while (dr.Read())
                            {
                                Result sr = new Result();
                                sr.Subject_id = (int)dr["Subject_id"];
                                sr.Student_id = Student_id;
                                sr.Ocinka = (double)dr["Ocinka"];


                                StudRes.Add(sr);
                            }
                        }
                        return StudRes;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Проблема з підключенням до бази даних. Детальна інформація: " +
                                            ex.Message);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

    }
}
