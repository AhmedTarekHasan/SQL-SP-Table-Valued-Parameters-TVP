USE [TvpTestDB]
GO

--Creating Employees TVP
CREATE TYPE EmployeesTVP AS TABLE
(
	[Name] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL
)
GO