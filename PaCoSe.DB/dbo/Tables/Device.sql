CREATE TABLE [dbo].[Device] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (50)  NOT NULL,
    [IdentifierHash]      NVARCHAR (MAX) NOT NULL,
    [DateCreated]         DATETIME       NOT NULL,
    [CreatedBy]           NVARCHAR (50)  NOT NULL,
    [DateModified]        DATETIME       NOT NULL,
    [ModifiedBy]          NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED ([Id] ASC)
);

