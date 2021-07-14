CREATE TABLE [dpk].[DataProtectionKeys] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FriendlyName] VARCHAR (MAX) NULL,
    [Xml]          VARCHAR (MAX) NULL,
    CONSTRAINT [PK_DataProtectionKeys] PRIMARY KEY CLUSTERED ([Id] ASC)
);

