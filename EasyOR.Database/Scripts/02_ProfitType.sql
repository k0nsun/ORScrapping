GO

PRINT 'Inserts Profits Type'

SET IDENTITY_INSERT [dbo].[PROFITTYPE] ON
GO

DELETE FROM [dbo].[PROFITTYPE]

MERGE INTO [dbo].[PROFITTYPE] AS Target
USING (VALUES 
	(1, N'Artéfact'),
	(2, N'Scientifique'),
	(3, N'Rien')
)
AS Source ([ProfitTypeId], [Libelle])
ON	Target.[ProfitTypeId] = Source.[ProfitTypeId]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ProfitTypeId], [Libelle])
	VALUES ([ProfitTypeId], [Libelle])
WHEN MATCHED THEN 
	UPDATE SET	Target.[Libelle] = Source.[Libelle]
OUTPUT	Inserted.[ProfitTypeId], 
		Inserted.[Libelle]
		;
GO

SET IDENTITY_INSERT [dbo].[PROFITTYPE] OFF
GO