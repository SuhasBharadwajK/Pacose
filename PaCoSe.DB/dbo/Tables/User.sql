CREATE TABLE [dbo].[User] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Sub]          NVARCHAR (MAX) NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (50)  NOT NULL,
    [DateModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

