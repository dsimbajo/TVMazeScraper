CREATE TABLE [dbo].[Cast] (
    [Id]           INT            NOT NULL,
    [ShowId]       INT            NULL,
    [Url]          NVARCHAR (MAX) NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [Birthday]     NVARCHAR (MAX) NULL,
    [Deathday]     NVARCHAR (MAX) NULL,
    [Gender]       NCHAR (10)     NULL,
    [Character]    NVARCHAR (MAX) NULL,
    [CharacterUrl] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Cast] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cast_Show] FOREIGN KEY ([ShowId]) REFERENCES [dbo].[Show] ([Id])
);

