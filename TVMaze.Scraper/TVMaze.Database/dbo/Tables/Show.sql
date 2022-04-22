CREATE TABLE [dbo].[Show] (
    [Id]       INT            NOT NULL,
    [Url]      NVARCHAR (255) NULL,
    [Name]     NVARCHAR (255) NULL,
    [Type]     NVARCHAR (255) NULL,
    [Language] NVARCHAR (255) NULL,
    [Genre]    NVARCHAR (255) NULL,
    [Network]  NVARCHAR (255) NULL,
    [Summary]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Show] PRIMARY KEY CLUSTERED ([Id] ASC)
);

