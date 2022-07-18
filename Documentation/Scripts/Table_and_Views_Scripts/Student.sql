USE [CTP]
GO

/****** Object:  Table [dbo].[Student]    Script Date: 2022-03-28 7:30:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[StudentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [Student_PK] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [Student_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [Student_User_FK]
GO