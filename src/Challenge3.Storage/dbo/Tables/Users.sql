CREATE TABLE [dbo].[Users] (
    [Code]        CHAR (4)     NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [LineVersion] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Code] ASC)
);

