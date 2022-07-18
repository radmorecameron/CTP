USE [CTP]
GO

/****** Object:  Table [dbo].[Activity]    Script Date: 2022-03-28 7:23:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Activity](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[CourseId] [int] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [Activity_PK] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [Activity_ActivityType_FK] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityType] ([ActivityTypeId])
GO

ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [Activity_ActivityType_FK]
GO

ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [Activity_Course_FK] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [Activity_Course_FK]
GO

ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [Activity_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO

ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [Activity_Language_FK]
GO