
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/07/2018 15:01:49
-- Generated from EDMX file: C:\Users\Mina Mohsen\Documents\Visual Studio 2017\Projects\TravelAgency\TravelAgency\Models\DB\AgencyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AgencyDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Ticket__Customer__75A278F5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK__Ticket__Customer__75A278F5];
GO
IF OBJECT_ID(N'[dbo].[FK__Ticket__Trip_ID__76969D2E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK__Ticket__Trip_ID__76969D2E];
GO
IF OBJECT_ID(N'[dbo].[FK__Trip__Tour_Guide__440B1D61]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trip] DROP CONSTRAINT [FK__Trip__Tour_Guide__440B1D61];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Ticket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ticket];
GO
IF OBJECT_ID(N'[dbo].[TourGuide]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TourGuide];
GO
IF OBJECT_ID(N'[dbo].[Trip]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trip];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NULL,
    [Phone_Number] varchar(20)  NULL,
    [Date_Of_Birth] datetime  NOT NULL,
    [Language] varchar(100)  NULL,
    [Number_Of_Trips] int  NOT NULL
);
GO

-- Creating table 'Tickets'
CREATE TABLE [dbo].[Tickets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Seat_Number] int  NOT NULL,
    [Trip_ID] int  NOT NULL,
    [Customer_ID] int  NOT NULL
);
GO

-- Creating table 'TourGuides'
CREATE TABLE [dbo].[TourGuides] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NULL,
    [Phone_Number] varchar(20)  NULL,
    [Date_Of_Birth] datetime  NOT NULL,
    [Language1] varchar(100)  NULL,
    [Language2] varchar(100)  NULL,
    [Language3] varchar(100)  NULL,
    [Number_Of_Trips] int  NOT NULL
);
GO

-- Creating table 'Trips'
CREATE TABLE [dbo].[Trips] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(100)  NULL,
    [Start_Date] datetime  NOT NULL,
    [End_Date] datetime  NOT NULL,
    [Number_Of_Seats] int  NOT NULL,
    [Tour_Guide_ID] int  NOT NULL,
    [Destination] varchar(50)  NULL,
    [Number_Of_Tickets] int  NOT NULL,
    [Langauge1] varchar(50)  NULL,
    [Langauge2] varchar(50)  NULL,
    [Language3] varchar(50)  NULL,
    [Price] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [PK_Tickets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TourGuides'
ALTER TABLE [dbo].[TourGuides]
ADD CONSTRAINT [PK_TourGuides]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Trips'
ALTER TABLE [dbo].[Trips]
ADD CONSTRAINT [PK_Trips]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK__Ticket__Customer__75A278F5]
    FOREIGN KEY ([Customer_ID])
    REFERENCES [dbo].[Customers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Ticket__Customer__75A278F5'
CREATE INDEX [IX_FK__Ticket__Customer__75A278F5]
ON [dbo].[Tickets]
    ([Customer_ID]);
GO

-- Creating foreign key on [Trip_ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK__Ticket__Trip_ID__76969D2E]
    FOREIGN KEY ([Trip_ID])
    REFERENCES [dbo].[Trips]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Ticket__Trip_ID__76969D2E'
CREATE INDEX [IX_FK__Ticket__Trip_ID__76969D2E]
ON [dbo].[Tickets]
    ([Trip_ID]);
GO

-- Creating foreign key on [Tour_Guide_ID] in table 'Trips'
ALTER TABLE [dbo].[Trips]
ADD CONSTRAINT [FK__Trip__Tour_Guide__440B1D61]
    FOREIGN KEY ([Tour_Guide_ID])
    REFERENCES [dbo].[TourGuides]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Trip__Tour_Guide__440B1D61'
CREATE INDEX [IX_FK__Trip__Tour_Guide__440B1D61]
ON [dbo].[Trips]
    ([Tour_Guide_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------