CREATE VIEW [dbo].[UserProfileView]
	AS SELECT U.[Id]
      ,U.[Sub]
      ,U.[Username]
	  ,UP.[Id] AS ProfileId
	  ,UP.[FirstName]
	  ,UP.[LastName]
	  ,UP.[Email]
	  ,U.[IsActivated]
      ,U.[IsInvited]
      ,U.[DateCreated]
      ,U.[CreatedBy]
      ,U.[DateModified]
      ,U.[ModifiedBy]
  FROM [dbo].[User] AS U
  INNER JOIN [dbo].[UserProfile] AS UP ON UP.UserId = U.Id
