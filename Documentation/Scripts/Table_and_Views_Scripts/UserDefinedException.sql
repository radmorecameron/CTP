USE [CTP]
GO

/****** Object:  Table [dbo].[UserDefinedException]    Script Date: 2022-03-28 7:32:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserDefinedException](
	[UserDefinedExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[UserDefinedExceptionName] [varchar](40) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_UserDefinedException] PRIMARY KEY CLUSTERED 
(
	[UserDefinedExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserDefinedException]  WITH CHECK ADD  CONSTRAINT [FK_UserDefinedException_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO

ALTER TABLE [dbo].[UserDefinedException] CHECK CONSTRAINT [FK_UserDefinedException_Language]
GO