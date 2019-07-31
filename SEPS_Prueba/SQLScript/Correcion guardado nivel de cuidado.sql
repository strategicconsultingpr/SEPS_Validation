use [SEPS_Test-Val]

INSERT INTO [dbo].[SA_LKP_TEDS_EPISODIO_PREVIO]
           ([PK_EpisodiosPrevios]
           ,[CO_TEDS]
           ,[DE_EpisodiosPrevios]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,0
           ,''
           ,0
           , null)
 

 USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_SALUD_MENTAL]
           ([PK_SaludMental]
           ,[DE_SaludMental]
           ,[CO_TEDS_MH]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,''
           ,0
           ,0
           ,null)
GO

USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TEDS_ESTADO_LEGAL]
           ([PK_EstadoLegal]
           ,[CO_TEDS]
           ,[DE_EstadoLegal]
           ,[CO_TEDS_MH]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,00
           ,''
           ,'00'
           ,0
           ,null)
GO



USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TIEMPO_ULT_TRAT]
           ([PK_TiempoUltTrat]
           ,[DE_TiempoUltTrat]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,''
           ,0
           ,null)
GO

USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_ABUSO_SUSTANCIAS]
           ([PK_AbusoSustancias]
           ,[CO_TEDS]
           ,[DE_AbusoSustancias]
           ,[Active]
           ,[Ordered]
           ,[ServiceReportsToTEDS_SA])
     VALUES
           (0
           ,0
		   ,''
           ,0
           ,null
           ,0)
GO



USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TEDS_SEGURO_SALUD]
           ([PK_SeguroSalud]
           ,[CO_TEDS]
           ,[DE_SeguroSalud]
           ,[Active]
           ,[Ordered]
           ,[CO_TEDS_MH])
     VALUES
           (0
           ,0
           ,''
           ,0
           ,0
           ,0)
GO

USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TEDS_PAGO]
           ([PK_Pago]
           ,[CO_TEDS]
           ,[DE_Pago]
           ,[Active]
           ,[Ordered]
           ,[CO_TEDS_MH])
     VALUES
           (0
           ,''
           ,''
           ,0
           ,0
           ,0)
GO

USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TEDS_FUENTE_INGRESO]
           ([PK_FuenteIngreso]
           ,[CO_TEDS]
           ,[DE_FuenteIngreso]
           ,[CO_TEDS_HM]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,''
           ,''
           ,''
		   ,0
           ,0)
GO

USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TIEMPO_RESIDENCIA]
           ([PK_TiempoResidencia]
           ,[DE_TiempoResidencia]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,''
           ,0
           ,0)
GO

USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_MUNICIPIO_RESIDENCIA]
           ([PK_Municipio]
           ,[DE_Municipio]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,''
           ,0
           ,0)
GO


USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TEDS_ETAPA_SERVICIO]
           ([PK_EtapaServicio]
           ,[CO_TEDS]
           ,[DE_EtapaServicio]
           ,[CO_TEDS_MH]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,0
           ,''
           ,''
           ,0
           ,0)
GO


USE [SEPS_Test-Val]
GO

INSERT INTO [dbo].[SA_LKP_TEDS_REFERIDO]
           ([PK_Referido]
           ,[CO_TEDS]
           ,[DE_Referido]
           ,[Active]
           ,[Ordered])
     VALUES
           (0
           ,''
           ,''
           ,0
           ,0)
GO



