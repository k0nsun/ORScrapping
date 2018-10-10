﻿CREATE TABLE [dbo].[QUEST]
(
	[QuestId] INT IDENTITY (1, 1) NOT NULL,
	[Name] NVARCHAR(200) NOT NULL, 
    [RewardTypeId] INT NULL, 
    [ProfitId] INT NULL, 
    [ProfitTypeId] INT NULL, 
    [ValueProfit] SMALLINT NULL, 
    [DurationProfil] SMALLINT NULL, 
    [HasSoldier] BIT DEFAULT 0, 
    [HasSpaceship] BIT DEFAULT 0, 
    [HasExploration] BIT DEFAULT 0, 
    [HasDefense] BIT DEFAULT 0, 
    [Comment] NVARCHAR(1000) NULL, 
    [Visible] BIT NOT NULL DEFAULT 0,
	[IsCheckName] BIT NOT NULL DEFAULT 0,
	[ProfilOldDatabase] NVARCHAR(1000) NULL, 
	[PlayerId] INT NULL,
    CONSTRAINT [PK_QUEST] PRIMARY KEY CLUSTERED ([QuestId] ASC),
	CONSTRAINT [FK_QUEST_REWARDTYPE] FOREIGN KEY ([RewardTypeId]) REFERENCES [dbo].[REWARDTYPE] ([RewardTypeId]),
	CONSTRAINT [FK_QUEST_PROFIT] FOREIGN KEY ([ProfitId]) REFERENCES [dbo].[PROFIT] ([ProfitId]),
	CONSTRAINT [FK_QUEST_PROFITTYPE] FOREIGN KEY ([ProfitTypeId]) REFERENCES [dbo].[PROFITTYPE] ([ProfitTypeId]),
	CONSTRAINT [FK_QUEST_PLAYER] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[PLAYER] ([PlayerId]),
)
