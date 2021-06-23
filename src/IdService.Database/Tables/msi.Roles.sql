CREATE TABLE [msi].[Roles] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             VARCHAR (256)    NULL,
    [NormalizedName]   VARCHAR (256)    NULL,
    [ConcurrencyStamp] VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [msi].[Roles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);

