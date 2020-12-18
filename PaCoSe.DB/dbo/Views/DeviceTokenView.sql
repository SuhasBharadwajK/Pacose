CREATE VIEW [dbo].[DeviceTokenView]
	AS SELECT D.[Id]
      ,D.[Name]
      ,D.[IdentifierHash]
	  ,DT.[TokenString]
	  ,DT.[ValidTill]
      ,D.[IsScreenTimeEnabled]
      ,D.[DeviceLimits]
      ,D.[DateCreated]
      ,D.[CreatedBy]
      ,D.[DateModified]
      ,D.[ModifiedBy]
  FROM [dbo].[Device] AS D
  INNER JOIN [dbo].[DeviceToken] AS DT ON DT.[DeviceId] = D.[Id] AND DT.[IsDeleted] = 0
