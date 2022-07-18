USE [CTP]
GO

/****** Object:  Table [dbo].[TestCase]    Script Date: 2022-03-28 7:31:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestCase](
	[TestCaseId] [int] IDENTITY(1,1) NOT NULL,
	[MethodSignatureId] [int] NOT NULL,
	[TestCaseName] [varchar](50) NOT NULL,
	[ExpectedValue] [varchar](255) NULL,
	[ValidateTestCase] [bit] NOT NULL,
 CONSTRAINT [TestCase_PK] PRIMARY KEY CLUSTERED 
(
	[TestCaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TestCase] ADD  DEFAULT ('0') FOR [TestCaseName]
GO

ALTER TABLE [dbo].[TestCase] ADD  CONSTRAINT [DF_TestCase_ValidateTestCase]  DEFAULT ((1)) FOR [ValidateTestCase]
GO