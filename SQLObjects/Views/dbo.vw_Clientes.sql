SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vw_Clientes
AS
SELECT        LTRIM(RTRIM(ISNULL(C.CLAVE, 'No Disponible'))) AS CLAVE, ISNULL(C.RFC, 'No Disponible') AS RFC, ISNULL(C.NOMBRE, 'No Disponible') AS NOMBRE_CLIENTE, 
                         LTRIM(RTRIM(ISNULL(C.CALLE, ''))) + ' ' + LTRIM(RTRIM(ISNULL(C.NUMEXT, ''))) + ' ' + LTRIM(RTRIM(ISNULL(C.NUMINT, ''))) + ' ' + LTRIM(RTRIM(ISNULL(C.COLONIA,
                          ''))) + ' ' + LTRIM(RTRIM(ISNULL(C.MUNICIPIO, ''))) AS DIRECCION, ISNULL(C.TELEFONO, 'No Disponible') AS TELEFONO, ISNULL(C.CODIGO, 'No Disponible') 
                         AS CODIGO, ISNULL(CT.NOMBRE, 'No Disponible') AS ATENCION, ISNULL(V.NOMBRE, 'No Disponible') AS NOMBRE_VENDEDOR, ISNULL(C.DESCUENTO, 0) 
                         AS DESCUENTO, ISNULL(C.DIASCRED, 0) AS DIAS_CRE, ISNULL(C.LIMCRED, 0) AS LIM_CRED, ISNULL(C.LIMCRED - C.SALDO, 0) AS CRED_DISPO, 
                         ISNULL(C.SALDO, 0) AS SALDO, ISNULL(Z.TEXTO, 'No Disponible') AS TEXTO, ISNULL(CONVERT(VARCHAR, C.FCH_ULTCOM, 106), '') AS FCH_ULTCOM,
                             (SELECT        ISNULL(SUM(P.PXS), 0) AS Expr1
                               FROM            aspel_sae50.dbo.FACTF01 AS F LEFT OUTER JOIN
                                                         aspel_sae50.dbo.PAR_FACTF01 AS P ON F.CVE_DOC = P.CVE_DOC
                               WHERE        (F.FECHAELAB BETWEEN DATEADD(YEAR, - 1, GETDATE()) AND GETDATE()) AND (F.STATUS <> 'C') AND (F.CVE_CLPV = C.CLAVE)) AS PZAULTANIO, 
                         ISNULL(CL.CAMPLIB8, '') AS CL8, C.CVE_VEND
FROM            aspel_sae50.dbo.CLIE01 AS C LEFT OUTER JOIN
                         aspel_sae50.dbo.VEND01 AS V ON C.CVE_VEND = V.CVE_VEND LEFT OUTER JOIN
                         aspel_sae50.dbo.ZONA01 AS Z ON C.CVE_ZONA = Z.CVE_ZONA LEFT OUTER JOIN
                         aspel_sae50.dbo.CONTAC01 AS CT ON C.CLAVE = CT.CVE_CLIE AND CT.TIPOCONTAC = 'V' AND CT.STATUS <> 'B' LEFT OUTER JOIN
                         aspel_sae50.dbo.CLIE_CLIB01 AS CL ON C.CLAVE = CL.CVE_CLIE
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[16] 2[39] 3) )"
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
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "V"
            Begin Extent = 
               Top = 6
               Left = 299
               Bottom = 135
               Right = 469
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Z"
            Begin Extent = 
               Top = 6
               Left = 507
               Bottom = 135
               Right = 677
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CT"
            Begin Extent = 
               Top = 6
               Left = 715
               Bottom = 135
               Right = 885
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CL"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
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
', 'SCHEMA', N'dbo', 'VIEW', N'vw_Clientes', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=1
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'dbo', 'VIEW', N'vw_Clientes', NULL, NULL
GO
