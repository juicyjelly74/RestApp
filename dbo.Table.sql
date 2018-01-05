CREATE TABLE [dbo].[Menu]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] DECIMAL NULL, 
    [Image] IMAGE NULL, 
    [Category] NVARCHAR(100) NOT NULL
)
