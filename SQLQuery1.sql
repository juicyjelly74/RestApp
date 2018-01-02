CREATE TABLE [dbo].[Restaurant_Users]
(
	[Id] INT NOT NULL IDENTITY ,
	[Username] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[RegDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[Email] NVARCHAR(50) NOT NULL,
	PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_Restaurant_Users_Username] ON [dbo].[Restaurant_Users] ([Username])
GO
INSERT INTO [dbo].[Restaurant_Users]
	([Username], [Password], [Email])
VALUES
	('test', 'test', 'test@test.test')
GO