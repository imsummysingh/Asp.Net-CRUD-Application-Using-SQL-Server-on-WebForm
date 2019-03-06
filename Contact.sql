CREATE TABLE [dbo].[Contact] (
    [ContactID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50)  NULL,
    [MobileNo]  NVARCHAR (50)  NULL,
    [Address]   NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([ContactID] ASC)
);
