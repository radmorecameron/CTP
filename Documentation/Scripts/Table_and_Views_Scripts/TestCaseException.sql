USE [CTP]
GO

/****** Object:  Table [dbo].[TestCaseException]    Script Date: 2022-03-28 7:31:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestCaseException](
	[TestCaseExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[TestCaseId] [int] NOT NULL,
	[ExceptionId] [int] NOT NULL,
 CONSTRAINT [TestCaseException_PK] PRIMARY KEY CLUSTERED 
(
	[TestCaseExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TestCaseException]  WITH CHECK ADD  CONSTRAINT [TestCaseException_Exception_FK] FOREIGN KEY([ExceptionId])
REFERENCES [dbo].[Exception] ([ExceptionId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TestCaseException] CHECK CONSTRAINT [TestCaseException_Exception_FK]
GO

ALTER TABLE [dbo].[TestCaseException]  WITH CHECK ADD  CONSTRAINT [TestCaseException_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TestCaseException] CHECK CONSTRAINT [TestCaseException_TestCase_FK]
GO