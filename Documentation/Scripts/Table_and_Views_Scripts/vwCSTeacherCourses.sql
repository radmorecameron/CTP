USE [CTP]
GO

/****** Object:  View [dbo].[vwCSTeacherCourses]    Script Date: 2022-03-28 7:34:11 PM ******/
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