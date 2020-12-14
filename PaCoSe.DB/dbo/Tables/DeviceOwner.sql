CREATE TABLE [dbo].[DeviceOwner] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [DeviceId]     BIGINT        NOT NULL,
    [OwnerId]      BIGINT        NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [CreatedBy]    NVARCHAR (50) NOT NULL,
    [DateModified] DATETIME      NOT NULL,
    [ModifiedBy]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_DeviceOwner] PRIMARY KEY CLUSTERED ([Id] ASC)
);

