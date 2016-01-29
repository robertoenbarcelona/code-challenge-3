CREATE TABLE [dbo].[Books] (
    [Code]        CHAR (4)     NOT NULL,
    [TypeId]      INT          NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [LineVersion] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([Code] ASC),
    CONSTRAINT [FK_ProductTypes_Books] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[ProductTypes] ([Id])
);

