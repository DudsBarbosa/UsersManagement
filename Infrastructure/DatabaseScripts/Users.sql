USE [WebContextDb]
GO

/****** Object: Table [dbo].[Users] Script Date: 28/11/2024 12:04:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX)  NOT NULL,
    [HourValue] DECIMAL (18, 2) NOT NULL,
    [AddDate]   DATETIME2 (7)   NOT NULL,
    [Active]    BIT             NOT NULL
);


