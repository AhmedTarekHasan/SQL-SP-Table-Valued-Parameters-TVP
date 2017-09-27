USE [TvpTestDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Droping the employees table if the table already exists in the database
IF EXISTS
(
  SELECT * FROM dbo.sysobjects
  WHERE id = object_id(N'[dbo].[Employees]')
         AND OBJECTPROPERTY(id, N'IsUserTable') = 1
)
DROP TABLE [dbo].[Employees]
GO

--Creating the employees table
CREATE TABLE [dbo].[Employees](
	[Employee_ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Employee_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO