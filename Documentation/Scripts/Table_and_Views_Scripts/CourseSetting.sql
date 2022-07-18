USE [CTP]
GO

/****** Object:  Table [dbo].[CourseSetting]    Script Date: 2022-03-28 7:26:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CourseSetting](
	[CourseCode] [varchar](30) NOT NULL,
	[DefaultLanguageId] [int] NULL,
 CONSTRAINT [PK_CourseSetting] PRIMARY KEY CLUSTERED 
(
	[CourseCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO