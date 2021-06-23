CREATE TABLE [msi].[Users] (
    [Id]                   UNIQUEIDENTIFIER   NOT NULL,
    [FirstName]            NVARCHAR (256)     NULL,
    [LastName]             NVARCHAR (256)     NULL,
    [Created]              DATETIME           NOT NULL,
    [Status]               TINYINT            NOT NULL,
    [Description]          VARCHAR (MAX)      NULL,
    [UserName]             VARCHAR (256)      NULL,
    [NormalizedUserName]   VARCHAR (256)      NULL,
    [Email]                VARCHAR (256)      NULL,
    [NormalizedEmail]      VARCHAR (256)      NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         VARCHAR (MAX)      NULL,
    [SecurityStamp]        VARCHAR (MAX)      NULL,
    [ConcurrencyStamp]     VARCHAR (MAX)      NULL,
    [PhoneNumber]          VARCHAR (256)      NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE CLUSTERED INDEX [IX_Users_Created]
    ON [msi].[Users]([Created] ASC);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [msi].[Users]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [msi].[Users]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);

