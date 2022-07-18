USE [CTP]
GO
SET IDENTITY_INSERT [dbo].[Activity] ON 
INSERT [dbo].[Activity] ([ActivityId], [Title], [StartDate], [EndDate], [CourseId], [ActivityTypeId], [LanguageId]) VALUES (5, N'Lab 2', CAST(N'2022-02-21' AS Date), CAST(N'2022-09-22' AS Date), 1, 1, 71)
SET IDENTITY_INSERT [dbo].[Activity] OFF
GO
SET IDENTITY_INSERT [dbo].[MethodSignature] ON 
INSERT [dbo].[MethodSignature] ([SignatureId], [MethodName], [ActivityId], [ReturnTypeId], [Description], [LastUpdated], [ReturnObjectType]) VALUES (4, N'get_evens', 5, 7, N'Get all the even values in a list', null, null)
INSERT [dbo].[MethodSignature] ([SignatureId], [MethodName], [ActivityId], [ReturnTypeId], [Description], [LastUpdated], [ReturnObjectType]) VALUES (7, N'get_even_count', 5, 2, N'Get the # of even values in a list', null, null)
SET IDENTITY_INSERT [dbo].[MethodSignature] OFF
GO
SET IDENTITY_INSERT [dbo].[TestCase] ON 
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (4, 4, N'Odds and evens', N'[2, 4, 6, 8, 10]', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (7, 4, N'Only odds', N'[]', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (8, 4, N'negative numbers', N'[-2, -4, 200]', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (9, 4, N'Only evens', N'[100, 200, 300, 400]', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (10, 4, N'All zeros', N'[0, 0, 0, 0]', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (12, 7, N'Odds and evens', N'5', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (13, 7, N'Only odds', N'0', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (14, 7, N'negative numbers', N'3', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (15, 7, N'Only evens', N'4', 1)
INSERT [dbo].[TestCase] ([TestCaseId], [MethodSignatureId], [TestCaseName], [ExpectedValue], [ValidateTestCase]) VALUES (16, 7, N'All zeros', N'4', 1)
SET IDENTITY_INSERT [dbo].[TestCase] OFF
GO
SET IDENTITY_INSERT [dbo].[SignatureParameter] ON 
INSERT [dbo].[SignatureParameter] ([SignatureParameterId], [ParameterName], [ParameterPosition], [InputParameter], [RequiredParameter], [DefaultValue], [MethodSignatureId], [DataTypeId], [ObjectDataType]) VALUES (7, N'num_list', 0, 1, 0, NULL, 4, 7, null)
INSERT [dbo].[SignatureParameter] ([SignatureParameterId], [ParameterName], [ParameterPosition], [InputParameter], [RequiredParameter], [DefaultValue], [MethodSignatureId], [DataTypeId], [ObjectDataType]) VALUES (11, N'num_list', 0, 1, 0, NULL, 7, 7, null)
SET IDENTITY_INSERT [dbo].[SignatureParameter] OFF
GO
SET IDENTITY_INSERT [dbo].[Parameter] ON 
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (4, N'[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]', 4, 7)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (7, N'[1, 3, 5, 7, 9]', 7, 7)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (8, N'[-2, -4, 5, 7, 201, 200]', 8, 7)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (9, N'[100, 200, 300, 400]', 9, 7)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (10, N'[0, 0, 0, 0]', 10, 7)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (13, N'[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]', 12, 11)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (14, N'[1, 3, 5, 7, 9]', 13, 11)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (15, N'[-2, -4, 5, 7, 201, 200]', 14, 11)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (16, N'[100, 200, 300, 400]', 15, 11)
INSERT [dbo].[Parameter] ([ParameterId], [Value], [TestCaseId], [SignatureParameterId]) VALUES (17, N'[0, 0, 0, 0]', 16, 11)
SET IDENTITY_INSERT [dbo].[Parameter] OFF
GO