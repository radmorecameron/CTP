USE [CTP]
GO

/****** Object:  Table [dbo].[DataType]    Script Date: 2022-03-28 7:26:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DataType](
	[DataTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DataType] [varchar](20) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [DataType_PK] PRIMARY KEY CLUSTERED 
(
	[DataTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DataType]  WITH CHECK ADD  CONSTRAINT [DataType_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO

ALTER TABLE [dbo].[DataType] CHECK CONSTRAINT [DataType_Language_FK]
GO