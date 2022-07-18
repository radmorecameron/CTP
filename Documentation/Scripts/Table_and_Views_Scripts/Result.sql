USE [CTP]
GO

/****** Object:  Table [dbo].[Result]    Script Date: 2022-03-28 7:29:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Result](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[PassFail] [bit] NULL,
	[ErrorMessage] [varchar](2000) NULL,
	[ActivityId] [int] NULL,
	[CodeUploadId] [int] NULL,
	[TestCaseId] [int] NOT NULL,
	[ActualValue] [varchar](255) NULL,
 CONSTRAINT [Results_PK] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [Result_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [Result_TestCase_FK]
GO

ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [Results_CodeUpload_FK] FOREIGN KEY([CodeUploadId])
REFERENCES [dbo].[CodeUpload] ([CodeUploadId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [Results_CodeUpload_FK]
GO