CREATE TABLE [dbo].[ChildDeviceMapping] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DeviceId]            BIGINT         NOT NULL,
    [ChildId]             BIGINT         NOT NULL,
    [IsScreenTimeEnabled] BIT            CONSTRAINT [DF_ChildDeviceMapping_IsScreenTimeEnabled] DEFAULT ((0)) NOT NULL,
    [DeviceLimits]        NVARCHAR (MAX) CONSTRAINT [DF_ChildDeviceMapping_DeviceLimits] DEFAULT ('[]') NOT NULL,
    [IsDeleted]           BIT            NOT NULL,
    [DateCreated]         DATETIME       NOT NULL,
    [CreatedBy]           NVARCHAR (50)  NOT NULL,
    [DateModified]        DATETIME       NOT NULL,
    [ModifiedBy]          NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_ChildDeviceMapping] PRIMARY KEY CLUSTERED ([Id] ASC)
);





