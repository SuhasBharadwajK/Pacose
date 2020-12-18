CREATE TABLE [dbo].[DeviceToken] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [DeviceId]     BIGINT         NOT NULL,
    [TokenString]  NVARCHAR (MAX) NOT NULL,
    [ValidTill]    DATETIME       NOT NULL,
    [IsDeleted]    BIT            NOT NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (50)  NOT NULL,
    [DateModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_DeviceToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);



