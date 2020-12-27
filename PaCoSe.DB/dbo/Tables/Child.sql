CREATE TABLE [dbo].[Child] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (50) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [MiddleName]   NVARCHAR (50) NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [IsDeleted]    BIT           NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [CreatedBy]    NVARCHAR (50) NOT NULL,
    [DateModified] DATETIME      NOT NULL,
    [ModifiedBy]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED ([Id] ASC)
);

