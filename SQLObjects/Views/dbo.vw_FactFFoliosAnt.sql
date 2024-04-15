SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vw_FactFFoliosAnt
AS
SELECT     TOP (100) PERCENT aspel_sae50.dbo.CLIE01.CLAVE AS CCLIE, FACTF.STATUS, FACTF.FECHA_DOC, FACTF.CVE_DOC, FACTF.CVE_CLPV, 
                      aspel_sae50.dbo.CLIE01.NOMBRE, FACTF.CVE_VEND, FACTF.CAN_TOT - (FACTF.DES_TOT + FACTF.DES_FIN) AS SUBTOTAL, FACTF.IMP_TOT4 AS IVA, 
                      FACTF.CAN_TOT - (FACTF.DES_TOT + FACTF.DES_FIN) + FACTF.IMP_TOT4 AS TOTAL, FACTF.FOLIO
FROM         aspel_sae50.dbo.CLIE01 LEFT OUTER JOIN
                      aspel_sae50.dbo.FACTF01 AS FACTF ON LTRIM(RTRIM(aspel_sae50.dbo.CLIE01.CLAVE)) = LTRIM(RTRIM(FACTF.CVE_CLPV))
WHERE     (FACTF.STATUS <> 'C') AND (FACTF.TIP_DOC = 'F')
GROUP BY FACTF.CVE_DOC, FACTF.STATUS, FACTF.FECHA_DOC, FACTF.CVE_CLPV, aspel_sae50.dbo.CLIE01.NOMBRE, FACTF.CVE_VEND, FACTF.CAN_TOT, FACTF.DES_TOT, 
                      FACTF.IMP_TOT4, FACTF.DES_FIN, aspel_sae50.dbo.CLIE01.CLAVE, FACTF.FOLIO
ORDER BY FACTF.FOLIO
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[24] 4[37] 2[15] 3) )"
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
         Begin Table = "CLIE01 (aspel_sae50.dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FACTF"
            Begin Extent = 
               Top = 6
               Left = 303
               Bottom = 125
               Right = 517
            End
            DisplayFlags = 280
            TopColumn = 15
         End
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
      Begin ColumnWidths = 12
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
', 'SCHEMA', N'dbo', 'VIEW', N'vw_FactFFoliosAnt', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=1
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'dbo', 'VIEW', N'vw_FactFFoliosAnt', NULL, NULL
GO
