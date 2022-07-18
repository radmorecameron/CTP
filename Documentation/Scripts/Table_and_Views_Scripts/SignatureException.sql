USE [CTP]
GO

/****** Object:  Table [dbo].[SignatureException]    Script Date: 2022-03-28 7:29:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SignatureException](
	[SignatureExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[SignatureId] [int] NOT NULL,
	[ExceptionId] [int] NOT NULL,
 CONSTRAINT [SignatureException_PK] PRIMARY KEY CLUSTERED 
(
	[SignatureExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SignatureException]  WITH CHECK ADD  CONSTRAINT [SignatureException_Exception_FK] FOREIGN KEY([ExceptionId])
REFERENCES [dbo].[Exception] ([ExceptionId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SignatureException] CHECK CONSTRAINT [SignatureException_Exception_FK]
GO

ALTER TABLE [dbo].[SignatureException]  WITH CHECK ADD  CONSTRAINT [SignatureException_Signature_FK] FOREIGN KEY([SignatureId])
REFERENCES [dbo].[MethodSignature] ([SignatureId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SignatureException] CHECK CONSTRAINT [SignatureException_Signature_FK]
GO