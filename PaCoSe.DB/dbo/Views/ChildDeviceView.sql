CREATE VIEW [dbo].[ChildDeviceView]
	AS SELECT D.[Id]
      ,D.[Name]
      ,D.[IdentifierHash]
      ,CD.[DeviceId]
	  ,CD.[ChildId]
      ,CD.[IsScreenTimeEnabled]
      ,CD.[DeviceLimits]
      ,D.[DateCreated]
      ,D.[CreatedBy]
      ,D.[DateModified]
      ,D.[ModifiedBy]
  FROM [dbo].[Device] AS D
  INNER JOIN [dbo].[ChildDeviceMapping] AS CD ON CD.[DeviceId] = D.[Id] AND CD.[IsDeleted] = 0
