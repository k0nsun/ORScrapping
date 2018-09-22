﻿CREATE TABLE [dbo].[PLAYER]
(
	[PlayerId] INT IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR(200) NULL,
	[IsAFK] BIT NOT NULL DEFAULT 0, 
    [IsVacation] BIT NOT NULL DEFAULT 0, 
    [InternalIdOR] INT NULL, 
    CONSTRAINT [PK_PLAYER] PRIMARY KEY CLUSTERED ([PlayerId] ASC),
)
