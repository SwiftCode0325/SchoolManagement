﻿BEGIN TRANSACTION;
GO

ALTER TABLE [Master].[AcademicLevel] ADD [CreatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

UPDATE [Account].[User] SET [CreatedOn] = '2021-08-12T12:38:36.5567000Z', [UpdatedOn] = '2021-08-12T12:38:36.5567624Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Account].[User] SET [CreatedOn] = '2021-08-12T12:38:36.5569656Z', [UpdatedOn] = '2021-08-12T12:38:36.5569662Z'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Account].[UserRole] SET [CreatedOn] = '2021-08-12T12:38:36.5867244Z', [UpdatedOn] = '2021-08-12T12:38:36.5868351Z'
WHERE [RoleId] = 1 AND [UserId] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Account].[UserRole] SET [CreatedOn] = '2021-08-12T12:38:36.5872736Z', [UpdatedOn] = '2021-08-12T12:38:36.5872747Z'
WHERE [RoleId] = 2 AND [UserId] = 2;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210812123839_Schoolmanagement00009', N'5.0.8');
GO

COMMIT;
GO

