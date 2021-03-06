USE [master]
GO
/****** Object:  Database [PixelTown]    Script Date: 05/01/2022 12:15:17 AM ******/
CREATE DATABASE [PixelTown]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PixelTown', FILENAME = N'E:\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PixelTown.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PixelTown_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PixelTown_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PixelTown] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PixelTown].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PixelTown] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PixelTown] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PixelTown] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PixelTown] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PixelTown] SET ARITHABORT OFF 
GO
ALTER DATABASE [PixelTown] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PixelTown] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PixelTown] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PixelTown] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PixelTown] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PixelTown] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PixelTown] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PixelTown] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PixelTown] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PixelTown] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PixelTown] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PixelTown] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PixelTown] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PixelTown] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PixelTown] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PixelTown] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PixelTown] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PixelTown] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PixelTown] SET  MULTI_USER 
GO
ALTER DATABASE [PixelTown] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PixelTown] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PixelTown] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PixelTown] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PixelTown] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PixelTown] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PixelTown] SET QUERY_STORE = OFF
GO
USE [PixelTown]
GO
/****** Object:  Table [dbo].[Access]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Access](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](200) NULL,
	[Time] [datetime] NULL,
 CONSTRAINT [PK_Access] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [varchar](200) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](max) NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](max) NULL,
	[Type] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[Avatar] [nvarchar](max) NULL,
	[SignalrID] [varchar](200) NULL,
 CONSTRAINT [PK_Account_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Character]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Character](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CharacterName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Character] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileMessage]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](200) NULL,
	[RoomID] [varchar](200) NULL,
	[UrlFile] [nvarchar](max) NULL,
	[Time] [bigint] NULL,
 CONSTRAINT [PK_FileMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoogleAuth]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoogleAuth](
	[ID] [int] NOT NULL,
	[UserID] [varchar](200) NOT NULL,
	[Token] [varchar](50) NULL,
 CONSTRAINT [PK_GoogleAuth] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Map]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MapName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Map] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](200) NULL,
	[RoomID] [varchar](200) NULL,
	[Message] [nvarchar](max) NULL,
	[Time] [bigint] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[ID] [varchar](200) NOT NULL,
	[UserID] [varchar](200) NOT NULL,
	[MapID] [int] NULL,
	[RoomName] [nvarchar](1000) NULL,
	[RoomPass] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccessRoom]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccessRoom](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](200) NULL,
	[RoomID] [varchar](200) NULL,
	[Time] [datetime] NULL,
 CONSTRAINT [PK_UserAccessRoom] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserJoinRoom]    Script Date: 05/01/2022 12:15:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserJoinRoom](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NULL,
	[RoomID] [varchar](200) NULL,
	[UserID] [varchar](200) NULL,
	[CharacterID] [int] NULL,
	[State] [varchar](40) NULL,
 CONSTRAINT [PK_UserJoinRoom] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([ID], [Name], [Email], [Password], [Birthday], [Address], [Type], [Active], [Avatar], [SignalrID]) VALUES (N'60f6b6e918104a64965e1e6783b7d7c2', N'BlueNight', N'henrypoter22@gmail.com', N'$2a$11$dbkyShhAm4yqDYHmU09ZmeRqWL3wGcKBwlIwpQGCZepJRQWt5/E7O', CAST(N'1999-01-01' AS Date), NULL, N'User', 1, N'/public/users/u11.jfif', NULL)
INSERT [dbo].[Account] ([ID], [Name], [Email], [Password], [Birthday], [Address], [Type], [Active], [Avatar], [SignalrID]) VALUES (N'9ccf171755b84addb7835408e6da0768', N'Văn', N'test3@gmail.com', N'$2a$11$BvbWWIA2qgMaP9NCY30CTeaVQ7aCY8VFmFeo27uCR1NbRDXbkMefS', CAST(N'1990-12-19' AS Date), N'Quận 8', N'User', 1, N'/public/users/u16.jfif', NULL)
INSERT [dbo].[Account] ([ID], [Name], [Email], [Password], [Birthday], [Address], [Type], [Active], [Avatar], [SignalrID]) VALUES (N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'Nguyễn Văn Huy', N'test@gmail.com', N'$2a$11$zFepZi3HRmRGyWPNjLYmaemFw9ZL238Iu8fZO599yu6Uowdt17ble', CAST(N'2000-11-18' AS Date), N'Quận 7 Thanh Pho Ho Chi Minh', N'User', 1, N'/public/users/u32(1).jfif', NULL)
INSERT [dbo].[Account] ([ID], [Name], [Email], [Password], [Birthday], [Address], [Type], [Active], [Avatar], [SignalrID]) VALUES (N'b31f1295545948b486d4bda4bf295711', N'Hải Đăng', N'test2@gmail.com', N'$2a$11$8CC48cLbIKHQPVKHWjo0xOINf2prHQlbhUXea0Pylosdm03uhfvum', CAST(N'2000-04-22' AS Date), N'Quận 7', N'User', 1, N'/public/users/u35.jfif', NULL)
INSERT [dbo].[Account] ([ID], [Name], [Email], [Password], [Birthday], [Address], [Type], [Active], [Avatar], [SignalrID]) VALUES (N'd4521e8d95bc48de91cc1d24867a463b', N'HuyDepzai', N'test1@gmail.com', N'$2a$11$L/fTEj4nmsnhcVnT5wx0zOzZBuBsK9Y0btngbofN6Os4FgXg2dEHW', CAST(N'1999-01-01' AS Date), NULL, N'User', 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Character] ON 

INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (1, N'misa')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (2, N'john')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (3, N'lili')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (4, N'lisa')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (5, N'nat')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (6, N'violet')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (7, N'aither')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (8, N'captain')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (9, N'captainAmerica')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (10, N'chenny')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (11, N'daniel')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (12, N'davis')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (13, N'kirin')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (14, N'linlin')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (15, N'logan')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (16, N'lora')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (17, N'phoenix')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (18, N'silver')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (19, N'storm')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (20, N'timmy')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (21, N'vision')
INSERT [dbo].[Character] ([ID], [CharacterName]) VALUES (22, N'wendy')
SET IDENTITY_INSERT [dbo].[Character] OFF
GO
SET IDENTITY_INSERT [dbo].[FileMessage] ON 

INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (1, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/result_easyOCR.txt', 1641265780875)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (2, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/Đội 3 Ngân+Nguyệt.csv', 1641265922205)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (3, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/Đội 3 Ngân+Nguyệt(1).csv', 1641265946201)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (4, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/An-efficient-mutual-authentication-and-privacy-prevention-scheme-for-e-healthcare-monitoring.pdf', 1641266549302)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (5, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/An-efficient-mutual-authentication-and-privacy-prevention-scheme-for-e-healthcare-monitoring(1).pdf', 1641266629191)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (6, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/300-cau-bai-tap-tu-vung-toeic-ets-2019-co-dap-an-2135575146.pdf', 1641266713838)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (7, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/300-cau-bai-tap-tu-vung-toeic-ets-2019-co-dap-an-2135575146(1).pdf', 1641266760521)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (8, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/_MG_8884.jpg', 1641266795575)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (9, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/14-Damiano-Baldoni-The-Torch-Of-Knowledge.mp3', 1641267347134)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (10, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'/public/files/794c5005f1c54855b520a3939b74b31b/300-cau-bai-tap-tu-vung-toeic-ets-2019-co-dap-an-2135575146.pdf', 1641269122641)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (11, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/14-Damiano-Baldoni-The-Torch-Of-Knowledge(1).mp3', 1641272933188)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (12, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/An-efficient-mutual-authentication-and-privacy-prevention-scheme-for-e-healthcare-monitoring(2).pdf', 1641272960245)
INSERT [dbo].[FileMessage] ([ID], [UserID], [RoomID], [UrlFile], [Time]) VALUES (13, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'/public/files/36ad8b10343f4d3c96be8c764fe99f26/BMTT_Nhom04_G6.docx', 1641273015160)
SET IDENTITY_INSERT [dbo].[FileMessage] OFF
GO
SET IDENTITY_INSERT [dbo].[Map] ON 

INSERT [dbo].[Map] ([ID], [MapName]) VALUES (1, N'Map 1')
INSERT [dbo].[Map] ([ID], [MapName]) VALUES (2, N'Map 2')
INSERT [dbo].[Map] ([ID], [MapName]) VALUES (3, N'Map 3')
INSERT [dbo].[Map] ([ID], [MapName]) VALUES (4, N'Map 4')
SET IDENTITY_INSERT [dbo].[Map] OFF
GO
SET IDENTITY_INSERT [dbo].[Message] ON 

INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (1, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'aaaa', 1641151751256)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (2, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'fgh', 1641151831372)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (3, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'h', 1641151834264)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (4, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'jjjh', 1641152127720)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (5, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'gg', 1641152336638)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (6, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sss', 1641152379339)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (7, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641152424552)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (8, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'w', 1641152436041)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (9, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N's', 1641152465287)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (10, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N's', 1641152471414)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (11, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641152528091)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (12, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641152533102)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (13, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641152571800)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (14, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641152611520)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (15, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dff', 1641152615755)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (16, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641152619904)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (17, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ff', 1641152623294)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (18, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'hello can you hear me', 1641152663343)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (19, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'aaa', 1641152866473)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (20, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641152870369)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (21, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641152877878)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (22, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ww', 1641152881587)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (23, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sadsf', 1641152886018)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (24, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sdd', 1641152941481)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (25, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641152947556)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (26, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641153474766)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (27, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sdfsdf fsd', 1641153663841)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (28, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sd
sd
dsd', 1641153734317)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (29, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'adds
sdsd
ddd', 1641153741933)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (30, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sdasdsdsd', 1641153760972)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (31, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sdads dsahjgfds  hjgfj    shfhjsdf  fsdfs  ', 1641153787971)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (32, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641154388794)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (33, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641154462467)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (34, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641154467489)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (35, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ddd', 1641154530533)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (36, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ddd', 1641154535525)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (37, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ffee', 1641154544629)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (38, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ee', 1641154548440)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (39, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641154576655)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (40, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'dd', 1641154614194)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (41, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ff', 1641154683721)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (42, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'd', 1641154808453)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (43, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ddd', 1641154810968)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (44, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ssd', 1641154833280)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (45, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'd', 1641154841644)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (46, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ss', 1641156303018)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (47, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'ddd', 1641156311601)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (48, N'b31f1295545948b486d4bda4bf295711', N'36ad8b10343f4d3c96be8c764fe99f26', N'Heello', 1641158761990)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (49, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sda', 1641163580350)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (50, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'fgdg', 1641164681986)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (51, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'Dat dau moi', 1641164691503)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (52, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'v', 1641164728501)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (53, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'v', 1641164731533)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (54, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'c', 1641164760387)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (55, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'nani core', 1641164777502)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (56, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'dfdf', 1641166896627)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (57, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'df', 1641166899178)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (58, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'gjg', 1641167935782)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (59, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'gfh', 1641169181074)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (60, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'794c5005f1c54855b520a3939b74b31b', N'ppp', 1641169184841)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (61, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'sf chao cau to la huy dep zai day', 1641169473698)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (62, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'059a82561ad442df913d65b8ebf074db', N'fdfs', 1641172641555)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (63, N'b31f1295545948b486d4bda4bf295711', N'059a82561ad442df913d65b8ebf074db', N'hjhj', 1641172651544)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (64, N'b31f1295545948b486d4bda4bf295711', N'059a82561ad442df913d65b8ebf074db', N'fhg', 1641172654401)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (65, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'059a82561ad442df913d65b8ebf074db', N'gh', 1641172656980)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (66, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'059a82561ad442df913d65b8ebf074db', N'eto con ga', 1641172669644)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (67, N'b31f1295545948b486d4bda4bf295711', N'059a82561ad442df913d65b8ebf074db', N'ewrr', 1641172672277)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (68, N'9ccf171755b84addb7835408e6da0768', N'00f9c910fada41328733337278824c88', N'sfdfdd', 1641175447823)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (69, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'059a82561ad442df913d65b8ebf074db', N'Hai Dang ga qua z ban oi', 1641247773771)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (70, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'059a82561ad442df913d65b8ebf074db', N'Danh nhau khong', 1641247785787)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (71, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'36ad8b10343f4d3c96be8c764fe99f26', N'hello', 1641249267925)
INSERT [dbo].[Message] ([ID], [UserID], [RoomID], [Message], [Time]) VALUES (72, N'9d1bf3ef1bbe4972af2d8ecf70b8438b', N'fb416020d3d74ce3a0fd0fa05921aad5', N'stupid', 1641273081108)
SET IDENTITY_INSERT [dbo].[Message] OFF
GO
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'00f9c910fada41328733337278824c88', N'9ccf171755b84addb7835408e6da0768', 2, N'ABC D', NULL, 5, N'adsf')
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'059a82561ad442df913d65b8ebf074db', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 2, N'My room 5', NULL, 25, N'eewewfgdgd
gfh')
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'27dabe8b97004fab83127f6d5ecb3cc0', N'9ccf171755b84addb7835408e6da0768', 2, N'My room 23333', NULL, 25, NULL)
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'36ad8b10343f4d3c96be8c764fe99f26', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 1, N'Room test', N'123456', 100, N'quá đẹp')
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'794c5005f1c54855b520a3939b74b31b', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 1, N'Room của Đăng', N'123', 10, N'Học tập')
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'7d754a1e929241dfaad6885ca764460d', N'9ccf171755b84addb7835408e6da0768', 2, N'My room 10', N'123', 25, NULL)
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'91a36c391c2746d3a60f3e1c185f8427', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 2, N'My room 3', N'333', 25, NULL)
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'a3129abbe23744be8eec564cca657b2f', N'd4521e8d95bc48de91cc1d24867a463b', 2, N'Huy Room', NULL, 25, NULL)
INSERT [dbo].[Room] ([ID], [UserID], [MapID], [RoomName], [RoomPass], [Quantity], [Description]) VALUES (N'fb416020d3d74ce3a0fd0fa05921aad5', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 2, N'My room 4', N'rrrrr', 25, N'retretyyy')
GO
SET IDENTITY_INSERT [dbo].[UserJoinRoom] ON 

INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (16, CAST(N'2021-12-29T01:58:44.057' AS DateTime), N'36ad8b10343f4d3c96be8c764fe99f26', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 6, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (17, CAST(N'2022-01-02T05:38:25.237' AS DateTime), N'36ad8b10343f4d3c96be8c764fe99f26', N'9ccf171755b84addb7835408e6da0768', 14, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (18, CAST(N'2022-01-02T21:17:31.150' AS DateTime), N'36ad8b10343f4d3c96be8c764fe99f26', N'd4521e8d95bc48de91cc1d24867a463b', 20, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (19, CAST(N'2022-01-03T04:25:08.583' AS DateTime), N'36ad8b10343f4d3c96be8c764fe99f26', N'b31f1295545948b486d4bda4bf295711', 20, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (20, CAST(N'2022-01-03T06:04:31.863' AS DateTime), N'794c5005f1c54855b520a3939b74b31b', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 15, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (21, CAST(N'2022-01-03T07:03:39.490' AS DateTime), N'794c5005f1c54855b520a3939b74b31b', N'b31f1295545948b486d4bda4bf295711', 15, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (22, CAST(N'2022-01-03T07:46:52.420' AS DateTime), N'91a36c391c2746d3a60f3e1c185f8427', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 15, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (23, CAST(N'2022-01-03T07:47:13.133' AS DateTime), N'91a36c391c2746d3a60f3e1c185f8427', N'b31f1295545948b486d4bda4bf295711', 16, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (24, CAST(N'2022-01-03T07:49:19.460' AS DateTime), N'fb416020d3d74ce3a0fd0fa05921aad5', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 10, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (25, CAST(N'2022-01-03T07:49:33.727' AS DateTime), N'fb416020d3d74ce3a0fd0fa05921aad5', N'b31f1295545948b486d4bda4bf295711', 12, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (26, CAST(N'2022-01-03T07:52:17.840' AS DateTime), N'059a82561ad442df913d65b8ebf074db', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 13, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (27, CAST(N'2022-01-03T07:52:25.480' AS DateTime), N'059a82561ad442df913d65b8ebf074db', N'b31f1295545948b486d4bda4bf295711', 15, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (28, CAST(N'2022-01-03T07:55:44.423' AS DateTime), N'27dabe8b97004fab83127f6d5ecb3cc0', N'9d1bf3ef1bbe4972af2d8ecf70b8438b', 1, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (29, CAST(N'2022-01-03T07:55:52.637' AS DateTime), N'27dabe8b97004fab83127f6d5ecb3cc0', N'b31f1295545948b486d4bda4bf295711', 20, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (30, CAST(N'2022-01-03T09:03:54.993' AS DateTime), N'00f9c910fada41328733337278824c88', N'9ccf171755b84addb7835408e6da0768', 19, N'Offline')
INSERT [dbo].[UserJoinRoom] ([ID], [Time], [RoomID], [UserID], [CharacterID], [State]) VALUES (31, CAST(N'2022-01-04T11:47:10.443' AS DateTime), N'36ad8b10343f4d3c96be8c764fe99f26', N'60f6b6e918104a64965e1e6783b7d7c2', 15, N'Offline')
SET IDENTITY_INSERT [dbo].[UserJoinRoom] OFF
GO
ALTER TABLE [dbo].[Access]  WITH CHECK ADD  CONSTRAINT [FK_Access_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Access] CHECK CONSTRAINT [FK_Access_Account]
GO
ALTER TABLE [dbo].[FileMessage]  WITH CHECK ADD  CONSTRAINT [FK_FileMessage_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
GO
ALTER TABLE [dbo].[FileMessage] CHECK CONSTRAINT [FK_FileMessage_Account]
GO
ALTER TABLE [dbo].[FileMessage]  WITH CHECK ADD  CONSTRAINT [FK_FileMessage_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([ID])
GO
ALTER TABLE [dbo].[FileMessage] CHECK CONSTRAINT [FK_FileMessage_Room]
GO
ALTER TABLE [dbo].[GoogleAuth]  WITH CHECK ADD  CONSTRAINT [FK_GoogleAuth_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GoogleAuth] CHECK CONSTRAINT [FK_GoogleAuth_Account]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Account]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Room]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Account1] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Account1]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Map] FOREIGN KEY([MapID])
REFERENCES [dbo].[Map] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Map]
GO
ALTER TABLE [dbo].[UserAccessRoom]  WITH CHECK ADD  CONSTRAINT [FK_UserAccessRoom_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAccessRoom] CHECK CONSTRAINT [FK_UserAccessRoom_Account]
GO
ALTER TABLE [dbo].[UserAccessRoom]  WITH CHECK ADD  CONSTRAINT [FK_UserAccessRoom_Room1] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAccessRoom] CHECK CONSTRAINT [FK_UserAccessRoom_Room1]
GO
ALTER TABLE [dbo].[UserJoinRoom]  WITH CHECK ADD  CONSTRAINT [FK_UserJoinRoom_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[Account] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserJoinRoom] CHECK CONSTRAINT [FK_UserJoinRoom_Account]
GO
ALTER TABLE [dbo].[UserJoinRoom]  WITH CHECK ADD  CONSTRAINT [FK_UserJoinRoom_Character1] FOREIGN KEY([CharacterID])
REFERENCES [dbo].[Character] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserJoinRoom] CHECK CONSTRAINT [FK_UserJoinRoom_Character1]
GO
ALTER TABLE [dbo].[UserJoinRoom]  WITH CHECK ADD  CONSTRAINT [FK_UserJoinRoom_Room1] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserJoinRoom] CHECK CONSTRAINT [FK_UserJoinRoom_Room1]
GO
USE [master]
GO
ALTER DATABASE [PixelTown] SET  READ_WRITE 
GO
