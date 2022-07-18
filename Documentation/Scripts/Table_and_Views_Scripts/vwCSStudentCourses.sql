USE [CTP]
GO

/****** Object:  View [dbo].[vwCSStudentCourses]    Script Date: 2022-03-28 7:33:31 PM ******/
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