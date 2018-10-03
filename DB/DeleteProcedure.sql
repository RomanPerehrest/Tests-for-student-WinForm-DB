USE TESTS
GO

CREATE PROCEDURE [dbo].[Answer]
@Answer_id uniqueidentifier 
AS 
BEGIN 
	SET NOCOUNT ON; 
	DELETE FROM Answer WHERE Answer_id = @Answer_id
END
GO






CREATE PROCEDURE [dbo].[Question]
@Question_id uniqueidentifier 
AS 
BEGIN 
	SET NOCOUNT ON; 
	DELETE FROM Question WHERE Question_id = @Question_id
END
GO







CREATE PROCEDURE [dbo].[Recommendation_Question]
@Recommendation_id uniqueidentifier 
AS 
BEGIN 
	SET NOCOUNT ON; 
	DELETE FROM Recommendation_Question WHERE @Recommendation_id = @Recommendation_id
END
GO






CREATE PROCEDURE [dbo].[Recommendation_Text]
@Recommendation_id uniqueidentifier 
AS 
BEGIN 
	SET NOCOUNT ON; 
	DELETE FROM Recommendation_Text WHERE @Recommendation_id = @Recommendation_id
END
GO







CREATE PROCEDURE [dbo].[Student]
@Student_id uniqueidentifier 
AS 
BEGIN 
	SET NOCOUNT ON; 
	DELETE FROM Student WHERE @Student_id = @Student_id
END
GO







CREATE PROCEDURE [dbo].[Subject]
@Subject_id uniqueidentifier 
AS 
BEGIN 
	SET NOCOUNT ON; 
	DELETE FROM Subject WHERE @Subject_id = @Subject_id
END
GO