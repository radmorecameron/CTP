USE [CTP]
GO

/****** Object:  Table [dbo].[Exception]    Script Date: 2022-03-28 7:27:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Exception](
	[ExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[ExceptionName] [varchar](40) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [Exception_PK] PRIMARY KEY CLUSTERED 
(
	[ExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Exception]  WITH CHECK ADD  CONSTRAINT [Exception_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO

ALTER TABLE [dbo].[Exception] CHECK CONSTRAINT [Exception_Language_FK]
GO