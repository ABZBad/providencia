CREATE TABLE [dbo].[COST_MSTR]
(
[NUM_REG] [int] NOT NULL IDENTITY(1, 1),
[CVE_CLPV] [varchar] (10) COLLATE Modern_Spanish_CI_AS NOT NULL,
[CVE_DOC] [varchar] (20) COLLATE Modern_Spanish_CI_AS NOT NULL,
[CAN_TOT] [float] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[COST_MSTR] ADD CONSTRAINT [PK__COST_MST__0AD0636C3CBF0154] PRIMARY KEY CLUSTERED  ([CVE_CLPV], [CVE_DOC]) ON [PRIMARY]
GO