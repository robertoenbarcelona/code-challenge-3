CREATE TABLE [dbo].[Rents] (
    [Id]             INT        IDENTITY (1, 1) NOT NULL,
    [ProductTypeId]  INT        NOT NULL,
    [ProductCode]    CHAR (4)   NOT NULL,
    [UserCode]       CHAR (4)   NOT NULL,
    [RentDate]       DATETIME   NOT NULL,
    [DueDate]        DATETIME   NOT NULL,
    [DevolutionDate] DATETIME   NULL,
    [LineVersion]    ROWVERSION NOT NULL,
    CONSTRAINT [PK_Rents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductTypes_Rents] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductTypes] ([Id]),
    CONSTRAINT [FK_Users_Rents] FOREIGN KEY ([UserCode]) REFERENCES [dbo].[Users] ([Code])
);

