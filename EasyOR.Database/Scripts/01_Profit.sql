GO

PRINT 'Inserts Profits'

SET IDENTITY_INSERT [dbo].[PROFIT] ON
GO

DELETE FROM [dbo].[PROFIT]

MERGE INTO [dbo].[PROFIT] AS Target
USING (VALUES 
	(1, N'Construction des appareils spécialisés'),
	(2, N'Evolution des technologies'),
	(3, N'Production de l''extracteur d''hydrogène'),
	(4, N'Production des cellules'),
	(5, N'Construction des bâtiments'),
	(6, N'Attaque / Bouclier des vaisseaux'),
	(7, N'Attaque / Coque des défenses'),
	(8, N'Bouclier des vaisseaux'),
	(9, N'Consommation d''hydrogène'),
	(10, N'Attaque / Coque des vaisseaux'),
	(11, N'Production des mines'),
	(12, N'Construction des vaisseaux'),
	(13, N'Production des militaires'),
	(14, N'Production de la mine de cristal'),
	(15, N'Production des centrales'),
	(16, N'Puissance des vaisseaux'),
	(17, N'Construction des défenses'),
	(18, N'Production de l''extracteur d''or'),
	(19, N'Production de l''extracteur de fer'),
	(20, N'Production de la mine de fer'),
	(21, N'Production de la mine d''or'),
	(23, N'Consommation du portail spatial'),
	(24, N'Coque des vaisseaux'),
	(25, N'Vitesse des vaisseaux'),
	(26, N'Rien')
)
AS Source ([ProfitId], [Libelle])
ON	Target.[ProfitId] = Source.[ProfitId]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ProfitId], [Libelle])
	VALUES ([ProfitId], [Libelle])
WHEN MATCHED THEN 
	UPDATE SET	Target.[Libelle] = Source.[Libelle]
OUTPUT	Inserted.[ProfitId], 
		Inserted.[Libelle]
		;
GO

SET IDENTITY_INSERT [dbo].[PROFIT] OFF
GO