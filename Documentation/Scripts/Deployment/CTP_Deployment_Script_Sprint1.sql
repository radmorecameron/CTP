USE [master]
GO
/****** Object:  Database [CTP]    Script Date: 2021-11-19 11:57:48 AM ******/
CREATE DATABASE [CTP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CTP', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CTP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CTP_log', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Data\CTP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CTP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CTP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CTP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CTP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CTP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CTP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CTP] SET ARITHABORT OFF 
GO
ALTER DATABASE [CTP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CTP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CTP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CTP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CTP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CTP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CTP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CTP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CTP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CTP] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CTP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CTP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CTP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CTP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CTP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CTP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CTP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CTP] SET RECOVERY FULL 
GO
ALTER DATABASE [CTP] SET  MULTI_USER 
GO
ALTER DATABASE [CTP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CTP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CTP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CTP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CTP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CTP] SET QUERY_STORE = OFF
GO
USE [CTP]
GO
/****** Object:  User [TeamCTP]    Script Date: 2021-11-19 11:57:48 AM ******/
CREATE USER [TeamCTP] FOR LOGIN [TeamCTP] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [TeamCTP]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [TeamCTP]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [TeamCTP]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [TeamCTP]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [TeamCTP]
GO
ALTER ROLE [db_datareader] ADD MEMBER [TeamCTP]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [TeamCTP]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ActivityId] [int] IDENTITY (1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[CourseId] [int] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [Activity_PK] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityType]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityType](
	[ActivityTypeId] [int] IDENTITY (1,1) NOT NULL,
	[ActivityName] [varchar](50) NULL,
 CONSTRAINT [ActivityType_PK] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CodeUpload]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeUpload](
	[CodeUploadId] [int] IDENTITY (1,1) NOT NULL,
	[UploadDate] [date] NULL,
	[CodeUploadFile] [image] NULL,
	[CodeUploadText] [varchar](6000) NULL,
	[ResultsId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
 CONSTRAINT [CodeUpload_PK] PRIMARY KEY CLUSTERED 
(
	[CodeUploadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY (1,1) NOT NULL,
	[CourseName] [varchar](40) NOT NULL,
	[CourseCode] [varchar](30) NULL,
 CONSTRAINT [Course_PK] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTPUser]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTPUser](
	[UserId] [int] IDENTITY (1,1) NOT NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](50) NULL,
 CONSTRAINT [User_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataType]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataType](
	[DataTypeId] [int] IDENTITY (1,1) NOT NULL,
	[DataType] [varchar](20) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [DataType_PK] PRIMARY KEY CLUSTERED 
(
	[DataTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageId] [int] NOT NULL,
	[LanguageName] [varchar](25) NOT NULL,
	[LanguageVersion] [varchar](10) NOT NULL,
 CONSTRAINT [Language_PK] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MethodSignature]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MethodSignature](
	[SignatureId] [int] IDENTITY (1,1) NOT NULL,
	[MethodName] [varchar](100) NULL,
	[ActivityId] [int] NOT NULL,
 CONSTRAINT [Signature_PK] PRIMARY KEY CLUSTERED 
(
	[SignatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parameter]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameter](
	[ParameterId] [int] IDENTITY (1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[ParameterPosition] [int] NOT NULL,
	[InputParameter] [bit] NOT NULL,
	[TestCaseId] [int] NOT NULL,
	[DataTypeId] [int] NOT NULL,
 CONSTRAINT [OutputParameter_PK] PRIMARY KEY CLUSTERED 
(
	[ParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Result]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Result](
	[ResultId] [int] IDENTITY (1,1) NOT NULL,
	[PassFail] [bit] NULL,
	[ErrorMessage] [varchar](50) NULL,
	[ActivityId] [int] NOT NULL,
	[CodeUploadId] [int] NULL,
	[TestCaseId] [int] NOT NULL,
 CONSTRAINT [Results_PK] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SignatureParameter]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignatureParameter](
	[SignatureParameterId] [int] IDENTITY (1,1) NOT NULL,
	[ParameterPosition] [int] NULL,
	[InputParameter] [bit] NULL,
	[RequiredParameter] [bit] NULL,
	[DefaultValue] [varchar](100) NULL,
	[MethodSignatureId] [int] NOT NULL,
	[DataTypeId] [int] NOT NULL,
 CONSTRAINT [SignatureParameter_PK] PRIMARY KEY CLUSTERED 
(
	[SignatureParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [Student_PK] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [Teacher_PK] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestCase]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCase](
	[TestCaseId] [int] IDENTITY (1,1) NOT NULL,
	[MethodSignatureId] [int] NOT NULL,
 CONSTRAINT [TestCase_PK] PRIMARY KEY CLUSTERED 
(
	[TestCaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCourse]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCourse](
	[UserCourseId] [int] IDENTITY (1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [UserCourse_PK] PRIMARY KEY CLUSTERED 
(
	[UserCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
set IDENTITY_INSERT [dbo].[ActivityType] ON;
INSERT [dbo].[ActivityType] ([ActivityTypeId], [ActivityName]) VALUES (1, N'Labs')
GO
INSERT [dbo].[ActivityType] ([ActivityTypeId], [ActivityName]) VALUES (2, N'Exercises')
GO
INSERT [dbo].[ActivityType] ([ActivityTypeId], [ActivityName]) VALUES (3, N'Assignments')
set IDENTITY_INSERT [dbo].[ActivityType] OFF;
GO
set IDENTITY_INSERT [dbo].[DataType] ON;
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (1, N'String', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (2, N'char', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (3, N'boolean', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (4, N'int', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (5, N'double', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (6, N'long', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (7, N'byte', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (8, N'short', 25)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (9, N'str', 38)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (10, N'int', 38)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (11, N'float', 38)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (12, N'complex', 38)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (13, N'bool', 38)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (14, N'bytes', 38)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (15, N'String', 34)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (16, N'Integer', 34)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (17, N'Float', 34)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (18, N'Boolean', 34)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (19, N'string', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (20, N'char', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (21, N'bool', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (22, N'int', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (23, N'double', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (24, N'long', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (25, N'float', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (26, N'DateTime', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (27, N'byte', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (28, N'short', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (29, N'decimal', 8)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (30, N'String', 26)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (31, N'Boolean', 26)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (32, N'Number', 26)
set IDENTITY_INSERT [dbo].[DataType] OFF;
GO
set IDENTITY_INSERT [dbo].[Language] ON;
INSERT [dbo].[Language] ([LanguageId], [LanguageName]) VALUES (8, N'C#')
GO
INSERT [dbo].[Language] ([LanguageId], [LanguageName]) VALUES (25, N'Java')
GO
INSERT [dbo].[Language] ([LanguageId], [LanguageName]) VALUES (26, N'JavaScript')
GO
INSERT [dbo].[Language] ([LanguageId], [LanguageName]) VALUES (34, N'PHP')
GO
INSERT [dbo].[Language] ([LanguageId], [LanguageName]) VALUES (38, N'Python')
set IDENTITY_INSERT [dbo].[Language] OFF;
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [Activity_ActivityType_FK] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityType] ([ActivityTypeId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [Activity_ActivityType_FK]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [Activity_Course_FK] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [Activity_Course_FK]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [Activity_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [Activity_Language_FK]
GO
ALTER TABLE [dbo].[CodeUpload]  WITH CHECK ADD  CONSTRAINT [CodeUpload_Activity_FK] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[CodeUpload] CHECK CONSTRAINT [CodeUpload_Activity_FK]
GO
ALTER TABLE [dbo].[CodeUpload]  WITH CHECK ADD  CONSTRAINT [CodeUpload_Student_FK] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[CodeUpload] CHECK CONSTRAINT [CodeUpload_Student_FK]
GO
ALTER TABLE [dbo].[DataType]  WITH CHECK ADD  CONSTRAINT [DataType_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[DataType] CHECK CONSTRAINT [DataType_Language_FK]
GO
ALTER TABLE [dbo].[MethodSignature]  WITH CHECK ADD  CONSTRAINT [MethodSignature_Activity_FK] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[MethodSignature] CHECK CONSTRAINT [MethodSignature_Activity_FK]
GO
ALTER TABLE [dbo].[Parameter]  WITH CHECK ADD  CONSTRAINT [Parameter_DataType_FK] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataType] ([DataTypeId])
GO
ALTER TABLE [dbo].[Parameter] CHECK CONSTRAINT [Parameter_DataType_FK]
GO
ALTER TABLE [dbo].[Parameter]  WITH CHECK ADD  CONSTRAINT [Parameter_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
GO
ALTER TABLE [dbo].[Parameter] CHECK CONSTRAINT [Parameter_TestCase_FK]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [Result_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [Result_TestCase_FK]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [Results_CodeUpload_FK] FOREIGN KEY([CodeUploadId])
REFERENCES [dbo].[CodeUpload] ([CodeUploadId])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [Results_CodeUpload_FK]
GO
ALTER TABLE [dbo].[SignatureParameter]  WITH CHECK ADD  CONSTRAINT [SignatureParameter_DataType_FK] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataType] ([DataTypeId])
GO
ALTER TABLE [dbo].[SignatureParameter] CHECK CONSTRAINT [SignatureParameter_DataType_FK]
GO
ALTER TABLE [dbo].[SignatureParameter]  WITH CHECK ADD  CONSTRAINT [SignatureParameter_MethodSignature_FK] FOREIGN KEY([MethodSignatureId])
REFERENCES [dbo].[MethodSignature] ([SignatureId])
GO
ALTER TABLE [dbo].[SignatureParameter] CHECK CONSTRAINT [SignatureParameter_MethodSignature_FK]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [Student_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [Student_User_FK]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [Teacher_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [Teacher_User_FK]
GO
ALTER TABLE [dbo].[TestCase]  WITH CHECK ADD  CONSTRAINT [TestCase_MethodSignature_FK] FOREIGN KEY([MethodSignatureId])
REFERENCES [dbo].[MethodSignature] ([SignatureId])
GO
ALTER TABLE [dbo].[TestCase] CHECK CONSTRAINT [TestCase_MethodSignature_FK]
GO
ALTER TABLE [dbo].[UserCourse]  WITH CHECK ADD  CONSTRAINT [UserCourse_Course_FK] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[UserCourse] CHECK CONSTRAINT [UserCourse_Course_FK]
GO
ALTER TABLE [dbo].[UserCourse]  WITH CHECK ADD  CONSTRAINT [UserCourse_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
GO
ALTER TABLE [dbo].[UserCourse] CHECK CONSTRAINT [UserCourse_User_FK]
GO
/****** Object:  StoredProcedure [dbo].[SelectCompSciStudents]    Script Date: 2021-12-08 10:38:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectCompSciStudents]
AS
BEGIN
SELECT DISTINCT e.Nom, e.Prenom, e.IDEtudiant AS 'StudentId', -1 as 'UserId'
FROM [CLARA].[CLARA].[ReportClient].[EtudiantSession] ss
JOIN Clara.Clara.ReportClient.Etudiant e on ss.IDEtudiant = e.IDEtudiant
JOIN Clara.Clara.ReportClient.Inscription r on r.IDEtudiantSession = ss.IDEtudiantSession
JOIN Clara.Clara.ReportClient.Cours c on c.IDCours = r.IDCours
JOIN Clara.Clara.ReportClient.Groupe g on c.IDCours = g.IDCours
WHERE
g.IDUniteOrg in (235)
AND ss.IdProgramme = '5638';
END
GO
/****** Object:  StoredProcedure [dbo].[SelectCoursesForStudent]    Script Date: 2021-11-19 1:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectCoursesForStudent]
	@StudentID int,
	@Semester int
AS
BEGIN
SELECT DISTINCT r.IDCours as CourseId, c.Numero as CourseCode, c.TitreLong AS CourseName
FROM [CLARA].[CLARA].[ReportClient].[EtudiantSession] ss
JOIN Clara.Clara.ReportClient.Etudiant e on ss.IDEtudiant = e.IDEtudiant
JOIN Clara.Clara.ReportClient.Inscription r on r.IDEtudiantSession = ss.IDEtudiantSession
JOIN Clara.Clara.ReportClient.Cours c on c.IDCours = r.IDCours
JOIN Clara.Clara.ReportClient.Groupe g on c.IDCours = g.IDCours
JOIN Clara.Clara.ReportClient.Discipline di on g.IDDiscipline = di.IDDiscipline
JOIN Clara.Clara.ReportClient.Departement d on d.IDDepartement = di.IDDepartement
WHERE
ss.IDEtudiant = @StudentID -- Only the courses being taught by a specific teacher
AND g.AnSession = @Semester -- Only the courses being taught in the current semester
AND g.IDUniteOrg in (235)
and (r.Etat = 2)
AND c.Numero like '420%'
END


GO
/****** Object:  StoredProcedure [dbo].[SelectCoursesForTeacher]    Script Date: 2021-11-19 11:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SelectCoursesForTeacher]
	@TeacherID int,
	@Semester int
AS
BEGIN
		SELECT DISTINCT(c.IDCours) AS CourseId,
			   c.Numero AS CourseCode, 
			   c.TitreLong AS CourseName
		FROM [CLARA].[Clara].[ReportClient].Employe em 
			 JOIN [CLARA].[Clara].[ReportClient].EmployeGroupe eg	ON em.IDEmploye = eg.IDEmploye
			 JOIN [CLARA].[Clara].[ReportClient].Groupe g			ON eg.IDGroupe = g.IDGroupe
			 JOIN [CLARA].[Clara].[ReportClient].Cours c		ON c.IDCours = g.IDCours
			 JOIN [CLARA].[Clara].[ReportClient].Discipline di  ON g.IDDiscipline = di.IDDiscipline
			 JOIN [CLARA].[Clara].[ReportClient].Departement d  ON d.IDDepartement = di.IDDepartement
			 JOIN [CLARA].[Clara].[ReportClient].UniteOrg ug    ON ug.IDUniteOrg = g.IDUniteOrg
		WHERE em.IDEmploye = @TeacherID -- Only the courses being taught by a specific teacher
			  AND g.AnSession = @Semester -- Only the courses being taught in the current semester
			  AND g.IDUniteOrg = 235 --235 = Heritage Regular Education
			  AND d.[Numero] = '420'
		ORDER BY c.Numero
END
GO
USE [master]
GO
ALTER DATABASE [CTP] SET  READ_WRITE 
GO
