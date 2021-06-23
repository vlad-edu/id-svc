CREATE TABLE [msi].[UserClaims] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  VARCHAR (MAX)    NULL,
    [ClaimValue] VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [msi].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId]
    ON [msi].[UserClaims]([UserId] ASC);

