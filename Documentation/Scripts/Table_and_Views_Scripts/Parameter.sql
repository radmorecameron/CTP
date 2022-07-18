USE [CTP]
GO

/****** Object:  Table [dbo].[Parameter]    Script Date: 2022-03-28 7:28:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Parameter](
	[ParameterId] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](500) NULL,
	[TestCaseId] [int] NOT NULL,
	[SignatureParameterId] [int] NULL,
 CONSTRAINT [OutputParameter_PK] PRIMARY KEY CLUSTERED 
(
	[ParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Parameter]  WITH CHECK ADD  CONSTRAINT [Parameter_SignatureParameter_FK] FOREIGN KEY([SignatureParameterId])
REFERENCES [dbo].[SignatureParameter] ([SignatureParameterId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Parameter] CHECK CONSTRAINT [Parameter_SignatureParameter_FK]
GO

ALTER TABLE [dbo].[Parameter]  WITH CHECK ADD  CONSTRAINT [Parameter_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Parameter] CHECK CONSTRAINT [Parameter_TestCase_FK]
GO