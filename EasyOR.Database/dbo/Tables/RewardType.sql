﻿CREATE TABLE [dbo].[REWARDTYPE]
(
	[RewardTypeId] INT IDENTITY (1, 1) NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	CONSTRAINT [PK_REWARD] PRIMARY KEY CLUSTERED ([RewardTypeId] ASC),
)