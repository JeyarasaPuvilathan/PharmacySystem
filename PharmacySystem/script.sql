USE [master]
GO
/****** Object:  Database [PharmacySystem]    Script Date: 3/26/2017 6:30:47 PM ******/
CREATE DATABASE [PharmacySystem] ON  PRIMARY 
( NAME = N'PharmacySystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\PharmacySystem.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PharmacySystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\PharmacySystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PharmacySystem] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PharmacySystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PharmacySystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PharmacySystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PharmacySystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PharmacySystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PharmacySystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PharmacySystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PharmacySystem] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [PharmacySystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PharmacySystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PharmacySystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PharmacySystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PharmacySystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PharmacySystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PharmacySystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PharmacySystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PharmacySystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PharmacySystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PharmacySystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PharmacySystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PharmacySystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PharmacySystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PharmacySystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PharmacySystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PharmacySystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PharmacySystem] SET  MULTI_USER 
GO
ALTER DATABASE [PharmacySystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PharmacySystem] SET DB_CHAINING OFF 
GO
USE [PharmacySystem]
GO
/****** Object:  Table [dbo].[AdminLogin]    Script Date: 3/26/2017 6:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminLogin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[MobileNo] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Image] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdminLogin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryDisease]    Script Date: 3/26/2017 6:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryDisease](
	[C_ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CategoryDisease] PRIMARY KEY CLUSTERED 
(
	[C_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Login]    Script Date: 3/26/2017 6:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailModel]    Script Date: 3/26/2017 6:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[To] [nvarchar](50) NOT NULL,
	[Subject] [nvarchar](50) NULL,
	[Body] [nvarchar](50) NULL,
 CONSTRAINT [PK_MailModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Map]    Script Date: 3/26/2017 6:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Latitude] [numeric](18, 6) NULL,
	[Longtitude] [numeric](18, 6) NULL,
	[Description] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
 CONSTRAINT [PK_Map] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicineDetails]    Script Date: 3/26/2017 6:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MedicineDetails](
	[M_ID] [smallint] IDENTITY(1,1) NOT NULL,
	[ManufacturedDate] [date] NOT NULL,
	[ExpiredDate] [date] NULL,
	[Dosage] [nvarchar](500) NULL,
	[Image] [nvarchar](50) NULL,
	[MedicineName] [varchar](30) NULL,
	[CategoryID] [int] NULL,
	[Price] [decimal](6, 2) NULL,
	[Description] [nvarchar](500) NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_MedicineDetails] PRIMARY KEY CLUSTERED 
(
	[M_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[AdminLogin] ADD  CONSTRAINT [DF_SomeName]  DEFAULT ('~/Content/img/medi2.jpg') FOR [Image]
GO
ALTER TABLE [dbo].[MedicineDetails]  WITH CHECK ADD  CONSTRAINT [FK_MedicineDetails_CategoryDisease] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[CategoryDisease] ([C_ID])
GO
ALTER TABLE [dbo].[MedicineDetails] CHECK CONSTRAINT [FK_MedicineDetails_CategoryDisease]
GO
USE [master]
GO
ALTER DATABASE [PharmacySystem] SET  READ_WRITE 
GO
