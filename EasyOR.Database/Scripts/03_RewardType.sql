GO

PRINT 'Inserts Rewards Type'

SET IDENTITY_INSERT [dbo].[REWARDTYPE] ON
GO

DELETE FROM [dbo].[REWARDTYPE]

MERGE INTO [dbo].[REWARDTYPE] AS Target
USING (VALUES 
	 (1, N'Coût'),
	 (2, N'Temps'),
	 (3, N'Production'),
	 (4, N'Réduction'),
	 (5, N'Consommation'),
	 (6, N'Coût + Temps'),
	 (7, N'Bonus'),
	 (8, N'Rien')
)
AS Source ([RewardTypeId], [Libelle])
ON	Target.[RewardTypeId] = Source.[RewardTypeId]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([RewardTypeId], [Libelle])
	VALUES ([RewardTypeId], [Libelle])
WHEN MATCHED THEN 
	UPDATE SET	Target.[Libelle] = Source.[Libelle]
OUTPUT	Inserted.[RewardTypeId], 
		Inserted.[Libelle]
		;
GO

SET IDENTITY_INSERT [dbo].[REWARDTYPE] OFF
GO