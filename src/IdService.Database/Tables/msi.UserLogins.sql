CREATE TABLE [msi].[UserLogins] (
    [LoginProvider]       VARCHAR (900)    NOT NULL,
    [ProviderKey]         VARCHAR (900)    NOT NULL,
    [ProviderDisplayName] VARCHAR (MAX)    NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [msi].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId]
    ON [msi].[UserLogins]([UserId] ASC);

