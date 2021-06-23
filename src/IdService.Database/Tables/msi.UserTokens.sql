CREATE TABLE [msi].[UserTokens] (
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [LoginProvider] VARCHAR (900)    NOT NULL,
    [Name]          VARCHAR (900)    NOT NULL,
    [Value]         VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [msi].[Users] ([Id]) ON DELETE CASCADE
);

