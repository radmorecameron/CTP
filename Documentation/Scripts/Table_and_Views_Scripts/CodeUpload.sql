USE [CTP]
GO

/****** Object:  Table [dbo].[CodeUpload]    Script Date: 2022-03-28 7:25:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CodeUpload](
	[CodeUploadId] [int] IDENTITY(1,1) NOT NULL,
	[UploadDate] [datetime2](7) NULL,
	[CodeUploadFileName] [varchar](100) NOT NULL,
	[CodeUploadFile] [image] NULL,
	[CodeUploadText] [varchar](6000) NULL,
	[StudentId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
 CONSTRAINT [CodeUpload_PK] PRIMARY KEY CLUSTERED 
(
	[CodeUploadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CodeUpload]  WITH CHECK ADD  CONSTRAINT [CodeUpload_Activity_FK] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CodeUpload] CHECK CONSTRAINT [CodeUpload_Activity_FK]
GO

ALTER TABLE [dbo].[CodeUpload]  WITH CHECK ADD  CONSTRAINT [CodeUpload_Student_FK] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CodeUpload] CHECK CONSTRAINT [CodeUpload_Student_FK]
GO