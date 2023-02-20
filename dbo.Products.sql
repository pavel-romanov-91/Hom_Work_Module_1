CREATE TABLE [dbo].[Products] (
    [Id]    INT           NOT NULL IDENTITY,
    [Name]  NVARCHAR (50) NULL,
    [Price] MONEY         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

