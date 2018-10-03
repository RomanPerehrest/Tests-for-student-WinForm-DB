use TESTS
GO

CREATE PROCEDURE [dbo].[AddStudents]
	@Student_id int,			
	@Last_name nvarchar(50),
	@First_name nvarchar(50),
	@Middle_name nvarchar(50),
	@Number char(15) = null, 
	@Email nvarchar(50) = null,
	@Login nvarchar(50),  
	@Password nvarchar(50)
AS
BEGIN
	INSERT INTO Student
	VALUES(@Student_id, @Last_name, @First_name, @Middle_name, @Number, @Email, @Login, @Password)
END
GO





CREATE PROCEDURE [dbo].[Authorization]
	@Authorizations_id int,			
	@Login nvarchar(50),  
	@Password nvarchar(50)
AS
BEGIN
	INSERT INTO Authorization
	VALUES(@Authorizations_id, @Login, @Password)
END
GO






CREATE PROCEDURE [dbo].[AddResult]
	@Result_id int,
	@Student_id int,				
	@Question_id int,
	@Rating float		
AS
BEGIN
	INSERT INTO Result
	VALUES(@Result_id, @Student_id, @Question_id, @Rating)
END
GO












CREATE PROCEDURE [dbo].[Question]
	@Question_id int,
	@Question nvarchar(80),		
	@Rating float	
AS
BEGIN
	INSERT INTO Question
	VALUES(@Question_id, @Question, @Rating)
END
GO





CREATE PROCEDURE [dbo].[Answer]
	@Answer_id int,
	@Question_id int,
	@Answer nvarchar(80),		
	@TrueFalse bit	
AS
BEGIN
	INSERT INTO Answer
	VALUES(@Answer_id,@Question_id, @Answer, @TrueFalse)
END
GO






CREATE PROCEDURE [dbo].[Recommendation_Question]
	@Recommendation_id int,			
	@Question_id int	
AS
BEGIN
	INSERT INTO Recommendation_Question
	VALUES(@Recommendation_id, @Question_id)
END
GO







CREATE PROCEDURE [dbo].[Subject]
	@Subject_id int,	
	@Subject_name nvarchar(80)	
AS
BEGIN
	INSERT INTO Subject
	VALUES(@Subject_id, @Subject_name, @Recommendation_id)
END
GO
























