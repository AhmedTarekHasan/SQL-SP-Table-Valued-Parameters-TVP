USE [TvpTestDB]
GO

--Declaring a variable of TVP type "EmployeesTVP" to be used to hold the input batch employee data
DECLARE @BatchEmployees AS EmployeesTVP

--Inserting batch employee data in the @BatchEmployees variable
INSERT INTO @BatchEmployees(Name, Age)
VALUES ('Ahmed', 27)
, ('Tarek', 12)
, ('Hasan', 13)
, ('Saleh', 14)
, ('Selim', 15)

--Clearing tables before inserting new records
DELETE FROM Employees

--Calling the stored procedure "AddEmployees" passing to it the batch data variable "BatchEmployees"
EXEC AddEmployees @BatchEmployees
GO