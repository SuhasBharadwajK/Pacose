CREATE VIEW [dbo].[DeviceOwnerView]
	AS SELECT D.[Id]
      ,D.[Name]
      ,D.[IdentifierHash]
      ,D.[IsScreenTimeEnabled]
      ,D.[DeviceLimits]
	  ,DO.[OwnerId]
      ,D.[DateCreated]
      ,D.[CreatedBy]
      ,D.[DateModified]
      ,D.[ModifiedBy]
  FROM [dbo].[Device] AS D
  INNER JOIN [dbo].[DeviceOwner] AS DO ON DO.DeviceId = D.Id AND DO.IsDeleted = 0
