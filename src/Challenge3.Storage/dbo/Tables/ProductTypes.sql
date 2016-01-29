CREATE TABLE [dbo].[ProductTypes] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [CanBeRented] BIT          NOT NULL,
    [RentPeriod]  INT          NOT NULL,
    [FineValue]   DECIMAL (18) NOT NULL,
    [MaxRenting]  INT          NOT NULL,
    [LineVersion] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

