SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vw_PrendasXCveDoc
AS
SELECT     PARF.CVE_DOC, SUM(CANT) AS PRENDAS
FROM         aspel_sae50.dbo.PAR_FACTF01 PARF LEFT JOIN
                      aspel_sae50.dbo.FACTF01 FACTF ON PARF.CVE_DOC = FACTF.CVE_DOC INNER JOIN
                      aspel_sae50.dbo.INVE01 INVE ON PARF.CVE_ART = INVE.CVE_ART
WHERE     PARF.TIPO_PROD = 'P' AND ltrim(rtrim(INVE.LIN_PROD)) <> 'MP' 
GROUP BY PARF.CVE_DOC
UNION ALL
SELECT     PARD.CVE_DOC, SUM(CANT) AS PRENDAS
FROM         aspel_sae50.dbo.PAR_FACTD01 PARD LEFT JOIN
                      aspel_sae50.dbo.FACTD01 FACTD ON PARD.CVE_DOC = FACTD.CVE_DOC INNER JOIN
                      aspel_sae50.dbo.INVE01 INVE ON PARD.CVE_ART = INVE.CVE_ART
WHERE     PARD.TIPO_PROD = 'P' AND ltrim(rtrim(INVE.LIN_PROD)) <> 'MP'
GROUP BY PARD.CVE_DOC
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[16] 4[45] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'dbo', 'VIEW', N'vw_PrendasXCveDoc', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=1
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'dbo', 'VIEW', N'vw_PrendasXCveDoc', NULL, NULL
GO
