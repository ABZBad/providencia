SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vw_Pedido
AS
SELECT        CLIE.CLAVE AS CCLIE, CLIE.NOMBRE, 'DIRECCION: ' + ISNULL(CLIE.CALLE, '') + ' ' + ISNULL(CLIE.NUMEXT, '') + ' ' + ISNULL(CLIE.NUMINT, '') 
                         + ' ' + ISNULL(CLIE.COLONIA, '') + ' ' + ' CIUDAD: ' + ISNULL(CLIE.MUNICIPIO, 'N/A') + ' ' + '   C.P. ' + ISNULL(CLIE.CODIGO, 'N/A') 
                         + ' ' + ' TEL:  ' + ISNULL(CLIE.TELEFONO, 'N/A') AS DIRECCION, CLIE.RFC, CT.NOMBRE AS ATENCION, ped.PEDIDO, ped.CLIENTE, ped.FECHA, ped.DESCUENTO, 
                         ped.TERMINOS, ped.COMISION, ped.AGENTE, ped.ESTATUS, ped.REMITIDO, ped.CONSIGNADO, ped.OBSERVACIONES, ped.IMPORTE, ped.PRENDAS, 
                         ped.DESC_DADO, ped.OC, ped_t.MODELO, ped_t.DESCRIPCION, ped_t.PRENDAS AS t_PRENDAS, ped_t.TALLAS, ped_t.PEDIDO AS t_PEDIDO, ped_t.PRECIO, 
                         ped_t.IMPORTE AS t_IMPORTE, ped_t.AGRUPADOR, ped_t.PRE_PROCESOS, cmt.CMT_PEDIDO, 
                         cmt.CMT_CMMT + ' ' + cmt.CMT_COMO + ' ' + cmt.CMT_DONDE AS CMT_CMMT, cmt.CMT_PROCESO, cmt.CMT_PRE_PROCESO, cmt.CMT_AGRUPADOR
FROM            aspel_sae50.dbo.CLIE01 AS CLIE INNER JOIN
                         aspel_sae50.dbo.PED_MSTR AS ped ON LTRIM(RTRIM(CLIE.CLAVE)) = LTRIM(RTRIM(ped.CLIENTE)) LEFT OUTER JOIN
                         aspel_sae50.dbo.PED_TEMP AS ped_t ON ped_t.PEDIDO = ped.PEDIDO LEFT OUTER JOIN
                         aspel_sae50.dbo.CMT_DET AS cmt ON cmt.CMT_PEDIDO = ped_t.PEDIDO AND cmt.CMT_AGRUPADOR = ped_t.AGRUPADOR LEFT OUTER JOIN
                         aspel_sae50.dbo.CONTAC01 AS CT ON CLIE.CLAVE = CT.CVE_CLIE AND CT.TIPOCONTAC = 'V' AND CT.STATUS = 'A'
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[11] 2[12] 3) )"
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
         Begin Table = "CLIE"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ped"
            Begin Extent = 
               Top = 6
               Left = 299
               Bottom = 135
               Right = 491
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ped_t"
            Begin Extent = 
               Top = 6
               Left = 737
               Bottom = 135
               Right = 909
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cmt"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CT"
            Begin Extent = 
               Top = 6
               Left = 529
               Bottom = 135
               Right = 699
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
       ', 'SCHEMA', N'dbo', 'VIEW', N'vw_Pedido', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_DiagramPane2', N'  Output = 720
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
', 'SCHEMA', N'dbo', 'VIEW', N'vw_Pedido', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=2
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'dbo', 'VIEW', N'vw_Pedido', NULL, NULL
GO
