USE [master]
GO
/****** Object:  Database [CTP]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  User [TeamCTP]    Script Date: 2022-03-28 9:12:23 AM ******/
CREATE USER [TeamCTP] FOR LOGIN [TeamCTP] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [IIS APPPOOL\CTP]    Script Date: 2022-03-28 9:12:23 AM ******/
CREATE USER [IIS APPPOOL\CTP] FOR LOGIN [IIS APPPOOL\CTP] WITH DEFAULT_SCHEMA=[dbo]
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
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\CTP]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\CTP]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\CTP]
GO
/****** Object:  View [dbo].[vwCSCourses]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[vwCSCourses]    Script Date: 2022-02-21 10:32:12 AM ******/
CREATE VIEW [dbo].[vwCSCourses] AS

SELECT DISTINCT CAST(NEWID() as NVARCHAR(50)) as CSCoursesId, c.Numero as CourseCode, c.TitreLong AS CourseName
FROM [CLARA].[CLARA].[ReportClient].[EtudiantSession] ss
JOIN Clara.Clara.ReportClient.Etudiant e on ss.IDEtudiant = e.IDEtudiant
JOIN Clara.Clara.ReportClient.Inscription r on r.IDEtudiantSession = ss.IDEtudiantSession
JOIN Clara.Clara.ReportClient.Cours c on c.IDCours = r.IDCours
JOIN Clara.Clara.ReportClient.Groupe g on c.IDCours = g.IDCours
JOIN Clara.Clara.ReportClient.Discipline di on g.IDDiscipline = di.IDDiscipline
JOIN Clara.Clara.ReportClient.Departement d on d.IDDepartement = di.IDDepartement
WHERE g.IDUniteOrg in (235)
AND c.Numero like '420%'
GO
/****** Object:  View [dbo].[vwCSStudentCourses]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[vwCSStudentCourses]    Script Date: 2022-02-21 10:32:38 AM ******/
CREATE VIEW [dbo].[vwCSStudentCourses] AS

SELECT CAST(NEWID() as NVARCHAR(50)) as CSStudentCourseId, c.Numero as CourseCode, e.IDEtudiant AS StudentId, g.AnSession AS SemesterId
FROM [CLARA].[CLARA].[ReportClient].[EtudiantSession] ss
JOIN Clara.Clara.ReportClient.Etudiant e on ss.IDEtudiant = e.IDEtudiant
JOIN Clara.Clara.ReportClient.Inscription r on r.IDEtudiantSession = ss.IDEtudiantSession
JOIN Clara.Clara.ReportClient.Cours c on c.IDCours = r.IDCours
JOIN Clara.Clara.ReportClient.Groupe g on c.IDCours = g.IDCours
JOIN Clara.Clara.ReportClient.Discipline di on g.IDDiscipline = di.IDDiscipline
JOIN Clara.Clara.ReportClient.Departement d on d.IDDepartement = di.IDDepartement
WHERE g.IDUniteOrg in (235)
and (r.Etat = 2)
AND c.Numero like '420%'
GO
/****** Object:  View [dbo].[vwCSStudents]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[vwCSStudents]    Script Date: 2022-02-21 10:32:46 AM ******/
CREATE VIEW [dbo].[vwCSStudents] AS

SELECT DISTINCT CAST(NEWID() as NVARCHAR(50)) as CSStudentId, e.Nom AS "LastName", e.Prenom AS "FirstName", e.IDEtudiant AS 'StudentId'
FROM [CLARA].[CLARA].[ReportClient].[EtudiantSession] ss
JOIN Clara.Clara.ReportClient.Etudiant e on ss.IDEtudiant = e.IDEtudiant
JOIN Clara.Clara.ReportClient.Inscription r on r.IDEtudiantSession = ss.IDEtudiantSession
JOIN Clara.Clara.ReportClient.Cours c on c.IDCours = r.IDCours
JOIN Clara.Clara.ReportClient.Groupe g on c.IDCours = g.IDCours
WHERE
g.IDUniteOrg in (235)
AND ss.IdProgramme = '5638';
GO
/****** Object:  View [dbo].[vwCSTeacherCourses]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[vwCSTeacherCourses]    Script Date: 2022-02-21 10:32:54 AM ******/
CREATE VIEW [dbo].[vwCSTeacherCourses] AS

SELECT DISTINCT CAST(NEWID() as NVARCHAR(50)) as CSTeacherCourseId, c.Numero as CourseCode, em.IDEmploye AS TeacherId, g.AnSession AS SemesterId
		FROM [CLARA].[Clara].[ReportClient].Employe em 
			 JOIN [CLARA].[Clara].[ReportClient].EmployeGroupe eg	ON em.IDEmploye = eg.IDEmploye
			 JOIN [CLARA].[Clara].[ReportClient].Groupe g			ON eg.IDGroupe = g.IDGroupe
			 JOIN [CLARA].[Clara].[ReportClient].Cours c		ON c.IDCours = g.IDCours
			 JOIN [CLARA].[Clara].[ReportClient].Discipline di  ON g.IDDiscipline = di.IDDiscipline
			 JOIN [CLARA].[Clara].[ReportClient].Departement d  ON d.IDDepartement = di.IDDepartement
			 JOIN [CLARA].[Clara].[ReportClient].UniteOrg ug    ON ug.IDUniteOrg = g.IDUniteOrg
		WHERE g.IDUniteOrg = 235 --235 = Heritage Regular Education
			  AND d.[Numero] = '420'
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[CourseId] [int] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [Activity_PK] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityType]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityType](
	[ActivityTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [varchar](50) NULL,
 CONSTRAINT [ActivityType_PK] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CodeUpload]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeUpload](
	[CodeUploadId] [int] IDENTITY(1,1) NOT NULL,
	[UploadDate] [datetime2](7) NULL,
	[CodeUploadFileName] [varchar](100) NOT NULL,
	[CodeUploadFile] [image] NULL,
	[CodeUploadText] [varchar](6000) NULL,
	[StudentId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
 CONSTRAINT [CodeUpload_PK] PRIMARY KEY CLUSTERED 
(
	[CodeUploadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](40) NOT NULL,
	[CourseCode] [varchar](30) NULL,
 CONSTRAINT [Course_PK] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseSetting]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSetting](
	[CourseCode] [varchar](30) NOT NULL,
	[DefaultLanguageId] [int] NULL,
 CONSTRAINT [PK_CourseSetting] PRIMARY KEY CLUSTERED 
(
	[CourseCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTPUser]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTPUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](50) NULL,
 CONSTRAINT [User_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataType]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[Exception]    Script Date: 2022-03-28 2:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exception](
	[ExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[ExceptionName] [varchar](40) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [Exception_PK] PRIMARY KEY CLUSTERED 
(
	[ExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[MethodSignature]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[Parameter]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[Result]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Result](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[PassFail] [bit] NULL,
	[ErrorMessage] [varchar](2000) NULL,
	[ActivityId] [int] NULL,
	[CodeUploadId] [int] NULL,
	[TestCaseId] [int] NOT NULL,
	[ActualValue] [varchar](255) NULL,
 CONSTRAINT [Results_PK] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SignatureException]    Script Date: 2022-03-28 2:28:21 PM ******/
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
/****** Object:  Table [dbo].[SignatureParameter]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[SignatureUserDefinedException]    Script Date: 2022-03-28 2:28:21 PM ******/
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
/****** Object:  Table [dbo].[Student]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[Teacher]    Script Date: 2022-03-28 9:12:23 AM ******/
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
/****** Object:  Table [dbo].[TestCase]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCase](
	[TestCaseId] [int] IDENTITY(1,1) NOT NULL,
	[MethodSignatureId] [int] NOT NULL,
	[TestCaseName] [varchar](50) NOT NULL,
	[ExpectedValue] [varchar](255) NULL,
	[ValidateTestCase] [bit] NOT NULL,
 CONSTRAINT [TestCase_PK] PRIMARY KEY CLUSTERED 
(
	[TestCaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestCaseException]    Script Date: 2022-03-28 2:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCaseException](
	[TestCaseExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[TestCaseId] [int] NOT NULL,
	[ExceptionId] [int] NOT NULL,
 CONSTRAINT [TestCaseException_PK] PRIMARY KEY CLUSTERED 
(
	[TestCaseExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCourse]    Script Date: 2022-03-28 9:12:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCourse](
	[UserCourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [UserCourse_PK] PRIMARY KEY CLUSTERED 
(
	[UserCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDefinedException]    Script Date: 2022-03-28 2:28:21 PM ******/
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

--DATABASE DATA
----------------------------------------------------------------------------------------------------------------------------------------------------------------
set IDENTITY_INSERT [dbo].[ActivityType] ON;
INSERT [dbo].[ActivityType] ([ActivityTypeId], [ActivityName]) VALUES (1, N'Labs')
GO
INSERT [dbo].[ActivityType] ([ActivityTypeId], [ActivityName]) VALUES (2, N'Exercises')
GO
INSERT [dbo].[ActivityType] ([ActivityTypeId], [ActivityName]) VALUES (3, N'Assignments')
set IDENTITY_INSERT [dbo].[ActivityType] OFF;
GO
-- There is no identity seeding for the language table. The LanguageId must match the judge 0 language id.
INSERT [dbo].[Language] ([LanguageId], [LanguageName], [LanguageVersion]) VALUES (71, N'Python', N'3.8.1')
GO
set IDENTITY_INSERT [dbo].[DataType] ON;
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (1, N'str', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (2, N'int', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (3, N'float', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (4, N'complex', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (5, N'bool', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (6, N'bytes', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (7, N'list', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (8, N'bytesarray', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (9, N'frozenset', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (10, N'set', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (11, N'dict', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (12, N'tuple', 71)
GO
INSERT [dbo].[DataType] ([DataTypeId], [DataType], [LanguageId]) VALUES (13, N'other', 71)
set IDENTITY_INSERT [dbo].[DataType] OFF;
GO
SET IDENTITY_INSERT [dbo].[Exception] ON 
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (1, N'AssertionError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (2, N'AttributeError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (3, N'EOFError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (4, N'FloatingPointError	', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (5, N'GeneratorExit', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (6, N'ImportError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (7, N'IndexError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (8, N'KeyError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (9, N'KeyboardInterrupt', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (10, N'MemoryError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (11, N'NameError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (12, N'NotImplementedError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (13, N'OSError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (14, N'OverflowError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (15, N'ReferenceError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (16, N'RuntimeError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (17, N'StopIteration', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (18, N'SyntaxError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (19, N'IndentationError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (20, N'TabError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (21, N'SystemError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (22, N'SystemExit', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (23, N'UnboundLocalError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (24, N'UnicodeError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (25, N'UnicodeEncodeError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (26, N'UnicodeDecodeError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (27, N'UnicodeTranslateError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (28, N'ValueError', 71)
GO
INSERT [dbo].[Exception] ([ExceptionId], [ExceptionName], [LanguageId]) VALUES (29, N'ZeroDivisionError', 71)
GO
SET IDENTITY_INSERT [dbo].[Exception] OFF
GO

--Relationships
----------------------------------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[MethodSignature] ADD  CONSTRAINT [DF_MethodSignature_ReturnType]  DEFAULT ((0)) FOR [ReturnTypeId]
GO
ALTER TABLE [dbo].[SignatureParameter] ADD  DEFAULT ((1)) FOR [RequiredParameter]
GO
ALTER TABLE [dbo].[TestCase] ADD  DEFAULT ('0') FOR [TestCaseName]
GO
ALTER TABLE [dbo].[TestCase] ADD  CONSTRAINT [DF_TestCase_ValidateTestCase]  DEFAULT ((1)) FOR [ValidateTestCase]
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
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CodeUpload] CHECK CONSTRAINT [CodeUpload_Activity_FK]
GO
ALTER TABLE [dbo].[CodeUpload]  WITH CHECK ADD  CONSTRAINT [CodeUpload_Student_FK] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CodeUpload] CHECK CONSTRAINT [CodeUpload_Student_FK]
GO
ALTER TABLE [dbo].[DataType]  WITH CHECK ADD  CONSTRAINT [DataType_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[DataType] CHECK CONSTRAINT [DataType_Language_FK]
GO
ALTER TABLE [dbo].[Exception]  WITH CHECK ADD  CONSTRAINT [Exception_Language_FK] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[Exception] CHECK CONSTRAINT [Exception_Language_FK]
GO
ALTER TABLE [dbo].[MethodSignature]  WITH CHECK ADD  CONSTRAINT [MethodSignature_Activity_FK] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MethodSignature] CHECK CONSTRAINT [MethodSignature_Activity_FK]
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
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [Result_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [Result_TestCase_FK]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [Results_CodeUpload_FK] FOREIGN KEY([CodeUploadId])
REFERENCES [dbo].[CodeUpload] ([CodeUploadId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [Results_CodeUpload_FK]
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
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [Student_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [Student_User_FK]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [Teacher_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [Teacher_User_FK]
GO
ALTER TABLE [dbo].[TestCaseException]  WITH CHECK ADD  CONSTRAINT [TestCaseException_Exception_FK] FOREIGN KEY([ExceptionId])
REFERENCES [dbo].[Exception] ([ExceptionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestCaseException] CHECK CONSTRAINT [TestCaseException_Exception_FK]
GO
ALTER TABLE [dbo].[TestCaseException]  WITH CHECK ADD  CONSTRAINT [TestCaseException_TestCase_FK] FOREIGN KEY([TestCaseId])
REFERENCES [dbo].[TestCase] ([TestCaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestCaseException] CHECK CONSTRAINT [TestCaseException_TestCase_FK]
GO
ALTER TABLE [dbo].[UserCourse]  WITH CHECK ADD  CONSTRAINT [UserCourse_Course_FK] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCourse] CHECK CONSTRAINT [UserCourse_Course_FK]
GO
ALTER TABLE [dbo].[UserCourse]  WITH CHECK ADD  CONSTRAINT [UserCourse_User_FK] FOREIGN KEY([UserId])
REFERENCES [dbo].[CTPUser] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCourse] CHECK CONSTRAINT [UserCourse_User_FK]
GO
ALTER TABLE [dbo].[UserDefinedException]  WITH CHECK ADD  CONSTRAINT [FK_UserDefinedException_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[UserDefinedException] CHECK CONSTRAINT [FK_UserDefinedException_Language]
GO
USE [master]
GO
ALTER DATABASE [CTP] SET  READ_WRITE 
GO
