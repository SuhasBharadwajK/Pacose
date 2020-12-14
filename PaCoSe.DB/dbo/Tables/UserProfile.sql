CREATE TABLE [dbo].[UserProfile] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [Email]        NVARCHAR (50) NOT NULL,
    [UserId]       BIGINT        NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [CreatedBy]    NVARCHAR (50) NOT NULL,
    [DateModified] DATETIME      NOT NULL,
    [ModifiedBy]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

