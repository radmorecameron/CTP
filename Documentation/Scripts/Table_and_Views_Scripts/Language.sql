USE [CTP]
GO

/****** Object:  Table [dbo].[Language]    Script Date: 2022-03-28 7:27:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Language](
	[LanguageId] [int] NOT NULL,
	[LanguageName] [varchar](25) NULL,
	[LanguageVersion] [varchar](10) NOT NULL,
 CONSTRAINT [Language_PK] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO