CREATE TABLE [dbo].[Fines] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [UserCode]    CHAR (4)     NOT NULL,
    [CrationDate] DATETIME     NOT NULL,
    [DueDays]     INT          NOT NULL,
    [Value]       DECIMAL (18) NOT NULL,
    [PayDate]     DATETIME     NULL,
    [LineVersion] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Fines] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Fines] FOREIGN KEY ([UserCode]) REFERENCES [dbo].[Users] ([Code])
);

