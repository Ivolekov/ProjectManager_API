IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Projects] (
    [Id] nvarchar(36) NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(250) NULL,
    [CreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Tasks] (
    [Id] nvarchar(36) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(250) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [WorkHours] real NOT NULL,
    [ProjectId] nvarchar(36) NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tasks_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Tasks_ProjectId] ON [Tasks] ([ProjectId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190426110708_InitialCreate', N'2.1.8-servicing-32085');

GO

 ALTER TABLE Projects
                                    ALTER COLUMN CreateDate datetime NULL

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190426125141_UpdateFieldDateTimeToNullable', N'2.1.8-servicing-32085');

GO

