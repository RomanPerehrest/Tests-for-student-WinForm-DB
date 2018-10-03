USE TESTS
GO


ALTER PROCEDURE [dbo].[Authorization]
    @login varchar(50),
	@password varchar(50)
AS
BEGIN
	SELECT Student.Student_id, Student.Last_name, Student.First_name, Student.Middle_name, Student.Number, Student.Email
	FROM Student WHERE Student.Login = @login and Student.Password = @password
END
GO

CREATE PROCEDURE [dbo].[GetStudentResult]
@StudentId int
AS
BEGIN
	SELECT s.Subject_id, s.Subject_name, r.CorrectAnswersCount, r.IncorrectAnswersCount From Result r
	inner join Subject s on s.Subject_id = r.Subject_id 
	Where r.Student_id = @StudentId
END
GO


CREATE PROCEDURE [dbo].[TestCompleted]
@StudentId int,
@SubjectId  int,
@CorrectCount int,
@IncorrectCount int
AS
BEGIN
	INSERT INTO Result VALUES (@StudentId, @SubjectId, @CorrectCount, @IncorrectCount);	
END
GO



CREATE PROCEDURE [dbo].[GetAllSubjects]
AS
BEGIN
	SELECT * FROM Subject;	
END
GO




CREATE PROCEDURE [dbo].[GetAllQuestionsForSubject]
	@subjectid int
AS
BEGIN

	SELECT q.Question_id, q.Question, a.Answer, a.TrueFalse from Question q
	inner join Answer a on a.Question_id = q.Question_id	
	WHERE  q.Subject_id = @subjectid
END
GO














