SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vw_EstadoCuenta
AS
SELECT        CVE_CLIE = rtrim(ltrim(CXC.CVE_CLIE)), CONVERT(VARCHAR, CXC.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, CXC.DOCTO AS Documento, CXC.REFER AS Referencia, 
                         ISNULL(CONVERT(VARCHAR, CXC.FECHA_APLI, 106), 'No Dispo.') AS Aplicado, ISNULL(CONVERT(VARCHAR, CXC.FECHA_VENC, 106), 'No Dispo.') AS Vencido, 
                         ISNULL(CONVERT(VARCHAR, CXC.FECHAELAB, 106), 'No Dispo.') AS Elaborado, CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC.IMPORTE, 0), 2) 
                         ELSE ROUND(0, 2) END AS Cargo, CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC.IMPORTE, 0), 2) ELSE ROUND(0, 2) END AS Abono, DATEDIFF(Day, 
                         CXC.FECHA_APLI, CXC.FECHA_VENC) AS Dias
FROM            aspel_sae50.dbo.CUEN_M01 CXC LEFT JOIN
                         aspel_sae50.dbo.CONC01 CO ON CXC.NUM_CPTO = CO.NUM_CPTO
UNION ALL
SELECT        CVE_CLIE = rtrim(ltrim(CXC_H.CVE_CLIE)), CONVERT(VARCHAR, CXC_H.NUM_CPTO) + ' - (' + CO.DESCR + ')' AS Concepto, CXC_H.DOCTO AS Documento, CXC_H.REFER AS Referencia, 
                         ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_APLI, 106), 'No Dispo.') AS Aplicado, ISNULL(CONVERT(VARCHAR, CXC_H.FECHA_VENC, 106), 'No Dispo.') AS Vencido, 
                         ISNULL(CONVERT(VARCHAR, CXC_H.FECHAELAB, 106), 'No Dispo.') AS Elaborado, CASE WHEN CO.TIPO = 'C' THEN ROUND(ISNULL(CXC_H.IMPORTE, 0), 2) 
                         ELSE ROUND(0, 2) END AS Cargo, CASE WHEN CO.TIPO = 'A' THEN ROUND(ISNULL(CXC_H.IMPORTE, 0), 2) ELSE ROUND(0, 2) END AS Abono, DATEDIFF(Day, 
                         CXC_H.FECHA_APLI, CXC_H.FECHA_VENC) AS Dias
FROM            aspel_sae50.dbo.CUEN_DET01 CXC_H LEFT JOIN
                         aspel_sae50.dbo.CONC01 CO ON CXC_H.NUM_CPTO = CO.NUM_CPTO
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[15] 4[20] 2[54] 3) )"
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
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
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
', 'SCHEMA', N'dbo', 'VIEW', N'vw_EstadoCuenta', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=1
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'dbo', 'VIEW', N'vw_EstadoCuenta', NULL, NULL
GO
