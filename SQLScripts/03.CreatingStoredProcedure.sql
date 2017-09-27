USE [TvpTestDB]
GO

--Dropping the stored procedure "AddEmployees" if already exists in a database
IF EXISTS
(
  SELECT * FROM dbo.sysobjects
  WHERE id = object_id(N'[dbo].[AddEmployees]')
         AND OBJECTPROPERTY(id, N'IsProcedure') = 1
)
DROP PROCEDURE [dbo].[AddEmployees]
GO

CREATE PROCEDURE AddEmployees
(
	@BatchEmployees AS EmployeesTVP READONLY
)
AS
BEGIN
	INSERT INTO Employees(Name, Age)
	SELECT Name, Age FROM @BatchEmployees
END