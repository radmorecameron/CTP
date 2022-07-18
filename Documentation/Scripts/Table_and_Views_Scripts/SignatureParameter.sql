USE [CTP]
GO

/****** Object:  Table [dbo].[SignatureParameter]    Script Date: 2022-03-28 7:30:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SignatureParameter](
	[SignatureParameterId] [int] IDENTITY(1,1) NOT NULL,
	[ParameterName] [varchar](100) NOT NULL,
	[ParameterPosition] [int] NOT NULL,
	[InputParameter] [bit] NOT NULL,
	[RequiredParameter] [bit] NOT NULL,
	[DefaultValue] [varchar](100) NULL,
	[MethodSignatureId] [int] NOT NULL,
	[DataTypeId] [int] NOT NULL,
	[ObjectDataType] [varchar](max) NULL,
 CONSTRAINT [SignatureParameter_PK] PRIMARY KEY CLUSTERED 
(
	[SignatureParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[SignatureParameter] ADD  DEFAULT ((1)) FOR [RequiredParameter]
GO

ALTER TABLE [dbo].[SignatureParameter]  WITH CHECK ADD  CONSTRAINT [SignatureParameter_DataType_FK] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataType] ([DataTypeId])
GO

ALTER TABLE [dbo].[SignatureParameter] CHECK CONSTRAINT [SignatureParameter_DataType_FK]
GO

ALTER TABLE [dbo].[SignatureParameter]  WITH CHECK ADD  CONSTRAINT [SignatureParameter_MethodSignature_FK] FOREIGN KEY([MethodSignatureId])
REFERENCES [dbo].[MethodSignature] ([SignatureId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SignatureParameter] CHECK CONSTRAINT [SignatureParameter_MethodSignature_FK]
GO