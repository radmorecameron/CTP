USE [CTP]
GO

/****** Object:  View [dbo].[vwCSStudents]    Script Date: 2022-03-28 7:33:57 PM ******/
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