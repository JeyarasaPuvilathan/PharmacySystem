
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/20/2018 23:18:14
-- Generated from EDMX file: C:\Users\Jana\Desktop\Pharmacy System\PharmacySystem\AdminLTE Template1\Models\PharmacyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PharmacySystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MedicineDetails_CategoryDisease]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineDetails] DROP CONSTRAINT [FK_MedicineDetails_CategoryDisease];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Maps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Maps];
GO
IF OBJECT_ID(N'[dbo].[CategoryDiseases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryDiseases];
GO
IF OBJECT_ID(N'[dbo].[AdminLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdminLogins];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[MedicineDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicineDetails];
GO
IF OBJECT_ID(N'[dbo].[Feedbacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedbacks];
GO
IF OBJECT_ID(N'[dbo].[CustomerLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerLogins];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Maps'
CREATE TABLE [dbo].[Maps] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Latitude] decimal(18,6)  NULL,
    [Longtitude] decimal(18,6)  NULL,
    [Description] nvarchar(50)  NULL,
    [Location] nvarchar(50)  NULL
);
GO

-- Creating table 'CategoryDiseases'
CREATE TABLE [dbo].[CategoryDiseases] (
    [C_ID] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'AdminLogins'
CREATE TABLE [dbo].[AdminLogins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [DateOfBirth] datetime  NULL,
    [MobileNo] int  NULL,
    [Email] nvarchar(50)  NULL,
    [Image] nvarchar(50)  NULL,
    [CreatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'MedicineDetails'
CREATE TABLE [dbo].[MedicineDetails] (
    [M_ID] smallint IDENTITY(1,1) NOT NULL,
    [ManufacturedDate] datetime  NOT NULL,
    [ExpiredDate] datetime  NULL,
    [Dosage] nvarchar(500)  NULL,
    [Image] nvarchar(50)  NULL,
    [MedicineName] varchar(30)  NULL,
    [CategoryID] int  NULL,
    [Price] decimal(18,2)  NULL,
    [Description] nvarchar(500)  NULL,
    [Quantity] int  NULL,
    [AddMonth] varchar(50)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Remarks] nvarchar(500)  NOT NULL,
    [DateTime] datetime  NULL
);
GO

-- Creating table 'CustomerLogins'
CREATE TABLE [dbo].[CustomerLogins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Age] int  NULL,
    [IsEmailVerified] bit  NULL,
    [ActivationCode] uniqueidentifier  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Maps'
ALTER TABLE [dbo].[Maps]
ADD CONSTRAINT [PK_Maps]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [C_ID] in table 'CategoryDiseases'
ALTER TABLE [dbo].[CategoryDiseases]
ADD CONSTRAINT [PK_CategoryDiseases]
    PRIMARY KEY CLUSTERED ([C_ID] ASC);
GO

-- Creating primary key on [ID] in table 'AdminLogins'
ALTER TABLE [dbo].[AdminLogins]
ADD CONSTRAINT [PK_AdminLogins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [M_ID] in table 'MedicineDetails'
ALTER TABLE [dbo].[MedicineDetails]
ADD CONSTRAINT [PK_MedicineDetails]
    PRIMARY KEY CLUSTERED ([M_ID] ASC);
GO

-- Creating primary key on [ID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CustomerLogins'
ALTER TABLE [dbo].[CustomerLogins]
ADD CONSTRAINT [PK_CustomerLogins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryID] in table 'MedicineDetails'
ALTER TABLE [dbo].[MedicineDetails]
ADD CONSTRAINT [FK_MedicineDetails_CategoryDisease]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[CategoryDiseases]
        ([C_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineDetails_CategoryDisease'
CREATE INDEX [IX_FK_MedicineDetails_CategoryDisease]
ON [dbo].[MedicineDetails]
    ([CategoryID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------