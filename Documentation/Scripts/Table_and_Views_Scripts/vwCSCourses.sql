USE [CTP]
GO

/****** Object:  View [dbo].[vwCSCourses]    Script Date: 2022-03-28 7:33:08 PM ******/
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