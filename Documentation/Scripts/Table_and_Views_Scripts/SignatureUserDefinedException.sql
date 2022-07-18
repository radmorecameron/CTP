USE [CTP]
GO

/****** Object:  Table [dbo].[SignatureUserDefinedException]    Script Date: 2022-03-28 7:30:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SignatureUserDefinedException](
	[SignatureUserDefinedExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[SignatureId] [int] NOT NULL,
	[UserDefinedExceptionId] [int] NOT NULL,
 CONSTRAINT [PK_SignatureUserDefinedException] PRIMARY KEY CLUSTERED 
(
	[SignatureUserDefinedExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SignatureUserDefinedException]  WITH CHECK ADD  CONSTRAINT [FK_SignatureUserDefinedException_MethodSignature] FOREIGN KEY([SignatureId])
REFERENCES [dbo].[MethodSignature] ([SignatureId])
GO

ALTER TABLE [dbo].[SignatureUserDefinedException] CHECK CONSTRAINT [FK_SignatureUserDefinedException_MethodSignature]
GO

ALTER TABLE [dbo].[SignatureUserDefinedException]  WITH CHECK ADD  CONSTRAINT [FK_SignatureUserDefinedException_UserDefinedException] FOREIGN KEY([UserDefinedExceptionId])
REFERENCES [dbo].[UserDefinedException] ([UserDefinedExceptionId])
GO

ALTER TABLE [dbo].[SignatureUserDefinedException] CHECK CONSTRAINT [FK_SignatureUserDefinedException_UserDefinedException]
GO