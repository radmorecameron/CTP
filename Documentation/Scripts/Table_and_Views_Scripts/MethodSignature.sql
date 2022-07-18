USE [CTP]
GO

/****** Object:  Table [dbo].[MethodSignature]    Script Date: 2022-03-28 7:28:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MethodSignature](
	[SignatureId] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [varchar](100) NULL,
	[ActivityId] [int] NOT NULL,
	[ReturnTypeId] [int] NOT NULL,
	[Description] [varchar](255) NULL,
	[LastUpdated] [datetime2](7) NULL,
	[ReturnObjectType] [varchar](max) NULL,
 CONSTRAINT [Signature_PK] PRIMARY KEY CLUSTERED 
(
	[SignatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MethodSignature] ADD  CONSTRAINT [DF_MethodSignature_ReturnType]  DEFAULT ((0)) FOR [ReturnTypeId]
GO

ALTER TABLE [dbo].[MethodSignature]  WITH CHECK ADD  CONSTRAINT [MethodSignature_Activity_FK] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MethodSignature] CHECK CONSTRAINT [MethodSignature_Activity_FK]
GO