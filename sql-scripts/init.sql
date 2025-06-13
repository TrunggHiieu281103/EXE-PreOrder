
/****** Object:  Database [EXE_PreOrder]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE DATABASE [EXE_PreOrder]
 
GO
ALTER DATABASE [EXE_PreOrder] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EXE_PreOrder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EXE_PreOrder] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET ARITHABORT OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EXE_PreOrder] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EXE_PreOrder] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EXE_PreOrder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EXE_PreOrder] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EXE_PreOrder] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET RECOVERY FULL 
GO
ALTER DATABASE [EXE_PreOrder] SET  MULTI_USER 
GO
ALTER DATABASE [EXE_PreOrder] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EXE_PreOrder] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EXE_PreOrder] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EXE_PreOrder] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EXE_PreOrder] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EXE_PreOrder] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EXE_PreOrder', N'ON'
GO
ALTER DATABASE [EXE_PreOrder] SET QUERY_STORE = ON
GO
ALTER DATABASE [EXE_PreOrder] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EXE_PreOrder]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProducts]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProducts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[DepositPrice] [decimal](18, 2) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserAddressId] [bigint] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[DepositPrice] [decimal](18, 2) NULL,
	[ShippingFee] [decimal](18, 2) NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[IsPreorder] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[PaymentCode] [nvarchar](450) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[PaymentType] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentStatus] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAssets]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAssets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[MediaKey] [nvarchar](max) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[PublicId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProductAssets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCommentAssets]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCommentAssets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[MediaKey] [nvarchar](max) NOT NULL,
	[ProductCommentId] [bigint] NOT NULL,
	[PublicId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProductCommentAssets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductComments]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductComments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[Rating] [float] NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[UserId] [bigint] NULL,
	[ProductId] [bigint] NULL,
	[OrderId] [bigint] NULL,
 CONSTRAINT [PK_ProductComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[ProductCode] [nvarchar](450) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CategoryId] [bigint] NULL,
	[BrandId] [bigint] NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Size] [nvarchar](max) NOT NULL,
	[StockQuantity] [int] NULL,
	[ProductDetails] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[OpenedAt] [bigint] NULL,
	[IsPreOrder] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RefreshToken] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shippings]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shippings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[TrackingNumber] [nvarchar](450) NOT NULL,
	[CarrierName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[EstimatedDeliveryAt] [bigint] NULL,
	[ShippedAt] [bigint] NULL,
	[DeliveredAt] [bigint] NULL,
 CONSTRAINT [PK_Shippings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAddresses]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddresses](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Province] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[Ward] [nvarchar](max) NOT NULL,
	[AddressDetail] [nvarchar](max) NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_UserAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/13/2025 4:07:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[AvatarKey] [nvarchar](max) NULL,
	[AvatarPublicId] [nvarchar](max) NULL,
	[Phone] [nvarchar](450) NOT NULL,
	[DateOfBirth] [bigint] NULL,
	[IsFirstLogin] [bit] NOT NULL,
	[IsEnableTwoFactor] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514080554_UpdateShippingRelation', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250529081232_RemovePhoneUniqueConstraint', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250611052831_ModifyProperty', N'8.0.4')
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (1, 1, 1, 1747800171139, 1747800171139, N'High Grade (HG)', N'Tỉ lệ: 1/144, Phổ biến nhất, dễ lắp ráp, giá rẻ')
INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (2, 2, 1, 1747800176812, 1747815992208, N'High Grade', N'string')
INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (3, 1, 1, 1747800294107, 1747800294107, N'Real Grade', N'Rất chi tiết và có khung xương nội bộ')
INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (4, 1, 1, 1747800341025, 1747800341025, N'Master Grade', N'Chi tiết cao, có khung xương và cơ chế chuyển động')
INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (5, 1, 1, 1747800378595, 1747800378595, N'Perfect Grade', N'Cực kỳ chi tiết, phức tạp nhất, thường đi kèm đèn LED')
INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (6, 1, 1, 1747800420039, 1747800420039, N'Entry Grade', N'Cực kỳ đơn giản, không cần keo hoặc sơn, lắp nhanh')
INSERT [dbo].[Brands] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Name], [Description]) VALUES (7, 1, 1, 1747800465519, 1747800465519, N'Mega Size Model', N'Tỉ lệ: 1/48, Rất lớn, ít chi tiết nhưng gây ấn tượng mạnh')
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [CategoryName], [Description]) VALUES (1, 1, 1, 1749047355038, 1749047355038, N'Gundam', N'Mobile Suit Gundam model kits and collectibles')
INSERT [dbo].[Categories] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [CategoryName], [Description]) VALUES (2, 1, 1, 1749048002728, 1749048002728, N'Gundam', N'Mobile Suit Gundam model kits and collectibles')
INSERT [dbo].[Categories] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [CategoryName], [Description]) VALUES (3, 1, 1, 1749048002728, 1749048002728, N'Gunpla Tools', N'Modeling tools for assembling and detailing Gundam kits')
INSERT [dbo].[Categories] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [CategoryName], [Description]) VALUES (4, 1, 1, 1749048002728, 1749048002728, N'Gundam Accessories', N'Weapons packs, effect parts, and display stands for Gundam kits')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProducts] ON 

INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (1, 1, 1, 1749620895599, 1749620895599, 6, 1, NULL, CAST(200000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (2, 1, 1, 1749622276815, 1749622276815, 7, 2, NULL, CAST(180000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (3, 1, 1, 1749622276815, 1749622276815, 7, 3, NULL, CAST(600000.00 AS Decimal(18, 2)), 4)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (4, 1, 1, 1749622477561, 1749622477561, 8, 2, NULL, CAST(180000.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (5, 1, 1, 1749622477561, 1749622477561, 8, 3, NULL, CAST(600000.00 AS Decimal(18, 2)), 4)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (6, 1, 1, 1749801864958, 1749801864958, 9, 1, NULL, CAST(200000.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (7, 1, 1, 1749801864958, 1749801864958, 9, 2, NULL, CAST(180000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[OrderProducts] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [OrderId], [ProductId], [DepositPrice], [Price], [Quantity]) VALUES (8, 1, 1, 1749804024915, 1749804024915, 10, 3, NULL, CAST(600000.00 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [dbo].[OrderProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [UserAddressId], [Status], [DepositPrice], [ShippingFee], [TotalPrice], [IsPreorder]) VALUES (6, 1, 1, 1749620895599, 1749620895599, 6, 15, N'Comfirmed', NULL, NULL, NULL, 0)
INSERT [dbo].[Orders] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [UserAddressId], [Status], [DepositPrice], [ShippingFee], [TotalPrice], [IsPreorder]) VALUES (7, 1, 1, 1749622276815, 1749622276815, 6, 15, N'CONFIRMED', CAST(0.00 AS Decimal(18, 2)), CAST(34000.00 AS Decimal(18, 2)), CAST(2760000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Orders] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [UserAddressId], [Status], [DepositPrice], [ShippingFee], [TotalPrice], [IsPreorder]) VALUES (8, 1, 1, 1749622477561, 1749622477561, 4, 13, N'CONFIRMED', CAST(0.00 AS Decimal(18, 2)), CAST(34000.00 AS Decimal(18, 2)), CAST(2760000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Orders] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [UserAddressId], [Status], [DepositPrice], [ShippingFee], [TotalPrice], [IsPreorder]) VALUES (9, 1, 1, 1749801864958, 1749801864958, 1, 18, N'PENDING', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(780000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Orders] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [UserAddressId], [Status], [DepositPrice], [ShippingFee], [TotalPrice], [IsPreorder]) VALUES (10, 1, 1, 1749804024915, 1749804024915, 1, 19, N'CONFIRM', CAST(100000.00 AS Decimal(18, 2)), CAST(17000.00 AS Decimal(18, 2)), CAST(1200000.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [PaymentCode], [OrderId], [PaymentType], [Content], [Amount], [PaymentStatus]) VALUES (1, 1, 1, 1749801865136, 1749801865136, N'PAY-48DD3BD7', 9, N'COD', N'Thanh toán đơn hàng #9', CAST(780000.00 AS Decimal(18, 2)), N'PENDING')
INSERT [dbo].[Payments] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [PaymentCode], [OrderId], [PaymentType], [Content], [Amount], [PaymentStatus]) VALUES (2, 1, 1, 1749804025095, 1749804025095, N'PAY-8EFCFEB9', 10, N'COD', N'Thanh toán đơn hàng #10', CAST(1117000.00 AS Decimal(18, 2)), N'PENDING')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductAssets] ON 

INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (1, 1, 1, 1749047856058, 1749047856058, N'615e87c5d45155c4b860ee9c2ccf8cbc', 1, N'products/fbd883gj77lb3wfoqmyf')
INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (2, 1, 1, 1749047885504, 1749047885504, N'170241a89c79bed3996b366b8f04cf1c', 2, N'products/fjukpzgzp5r5v4bwriyj')
INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (3, 1, 1, 1749048081875, 1749048081875, N'e457ca05a896ca1af1696054b8bcf54e', 3, N'products/ibi23ygfdqhgilxgbqfm')
INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (4, 1, 1, 1749048099930, 1749048099930, N'eeb005ca3b6f326e4a06bcd58e9aa3c6', 4, N'products/febbkiobjw8yvsviysfr')
INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (5, 1, 1, 1749048868829, 1749048868829, N'c706ac9e6f24e7e596b8a2a993bb67b6', 4, N'products/ziyw57wzplxzhzolhyyc')
INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (6, 1, 1, 1749048878902, 1749048878902, N'5b258e32759393b51b1103e622af246a', 4, N'products/yzi1g2b2zs9fhdogn8c5')
INSERT [dbo].[ProductAssets] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [MediaKey], [ProductId], [PublicId]) VALUES (7, 1, 1, 1749048889859, 1749048889859, N'f2a8f7200e3ad0a2e95ebc7488527baa', 4, N'products/qg7lc9pcmrnv2cajitxk')
SET IDENTITY_INSERT [dbo].[ProductAssets] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [ProductCode], [ProductName], [Description], [CategoryId], [BrandId], [Type], [Size], [StockQuantity], [ProductDetails], [Price], [OpenedAt], [IsPreOrder]) VALUES (1, 1, 1, 1749047375279, 1749047375279, N'GDM-001', N'RX-78-2 Gundam HG 1/144', N'High Grade 1/144 scale RX-78-2 Gundam plastic model kit', 1, 1, N'HG', N'1/144', 100, N'Includes Beam Rifle, Shield, and Beam Saber. Articulated joints.', CAST(200000.00 AS Decimal(18, 2)), NULL, 0)
INSERT [dbo].[Products] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [ProductCode], [ProductName], [Description], [CategoryId], [BrandId], [Type], [Size], [StockQuantity], [ProductDetails], [Price], [OpenedAt], [IsPreOrder]) VALUES (2, 1, 1, 1749047375279, 1749047375279, N'GDM-002', N'Zaku II HG 1/144', N'High Grade 1/144 scale MS-06 Zaku II', 1, 2, N'HG', N'1/144', 80, N'Includes Machine Gun, Heat Hawk. Articulated limbs and mono-eye.', CAST(180000.00 AS Decimal(18, 2)), NULL, 0)
INSERT [dbo].[Products] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [ProductCode], [ProductName], [Description], [CategoryId], [BrandId], [Type], [Size], [StockQuantity], [ProductDetails], [Price], [OpenedAt], [IsPreOrder]) VALUES (3, 1, 1, 1749048016634, 1749048016634, N'GDM-003', N'Unicorn Gundam MG 1/100', N'Master Grade Unicorn Gundam model kit with transformation gimmick', 1, NULL, N'MG', N'1/100', 60, N'Includes beam magnum, shield, beam sabers, and Psycho Frame mode.', CAST(600000.00 AS Decimal(18, 2)), NULL, 0)
INSERT [dbo].[Products] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [ProductCode], [ProductName], [Description], [CategoryId], [BrandId], [Type], [Size], [StockQuantity], [ProductDetails], [Price], [OpenedAt], [IsPreOrder]) VALUES (4, 1, 1, 1749048016634, 1749048016634, N'GDM-004', N'Wing Gundam Zero EW RG 1/144', N'Real Grade version of Wing Gundam Zero EW from Endless Waltz', 1, NULL, N'RG', N'1/144', 75, N'Includes Twin Buster Rifle, Wing binders, and great articulation.', CAST(450000.00 AS Decimal(18, 2)), NULL, 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [RoleName]) VALUES (1, 1, 1, 1748526960378, 1748526960378, N'USER')
INSERT [dbo].[Roles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [RoleName]) VALUES (2, 1, 1, 1748526960378, 1748526960378, N'ADMIN')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAddresses] ON 

INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (10, 6, 1, 1749620301200, 1749802603947, 1, N'Hà Nội', N'Ba Đình', N'Phúc Xá', N'123 Phố X', 0)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (11, 1, 1, 1749620301200, 1749620301200, 2, N'Hồ Chí Minh', N'Quận 1', N'Bến Nghé', N'456 Phố Y', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (12, 1, 1, 1749620301200, 1749620301200, 3, N'Đà Nẵng', N'Hải Châu', N'Thạch Thang', N'789 Phố Z', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (13, 1, 1, 1749620301200, 1749620301200, 4, N'Cần Thơ', N'Ninh Kiều', N'An Cư', N'101 Phố A', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (14, 1, 1, 1749620301200, 1749620301200, 5, N'Hải Phòng', N'Lê Chân', N'An Biên', N'202 Phố B', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (15, 1, 1, 1749620301200, 1749620301200, 6, N'Bình Dương', N'Thủ Dầu Một', N'Phú Cường', N'303 Phố C', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (16, 1, 1, 1749620301200, 1749620301200, 7, N'Bắc Ninh', N'TP Bắc Ninh', N'Ninh Xá', N'404 Phố D', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (17, 1, 1, 1749620301200, 1749620301200, 8, N'Hưng Yên', N'Văn Lâm', N'Nghĩa Trụ', N'505 Phố E', 1)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (18, 4, 1, 1749627185284, 1749802604051, 1, N'string', N'string', N'string', N'string', 0)
INSERT [dbo].[UserAddresses] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [Province], [District], [Ward], [AddressDetail], [IsDefault]) VALUES (19, 4, 1, 1749633501320, 1749802604056, 1, N'Tp. Hồ Chí Minh', N'Quận Gò Vấp', N'Phường 10', N'236/43, Thống Nhất', 1)
SET IDENTITY_INSERT [dbo].[UserAddresses] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (1, 1, 1, 1748927426447, 1748927426447, 2, 1)
INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (4, 1, 1, 1749025904823, 1749025904823, 3, 2)
INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (5, 1, 1, 1749026157997, 1749026157997, 4, 1)
INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (6, 1, 1, 1749026632841, 1749026632841, 5, 1)
INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (7, 1, 1, 1749027189943, 1749027189943, 6, 1)
INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (8, 1, 1, 1749027421699, 1749027421699, 7, 1)
INSERT [dbo].[UserRoles] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [UserId], [RoleId]) VALUES (9, 1, 1, 1749045808935, 1749045808935, 8, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (1, 1, 1, 1748526960378, 1748526960378, N'string', N'AQAAAAIAAYagAAAAEBbPXOJidiy+1YEwwJUs+oTBYJqRyaOIPx1sh+1ULPzf21GhIuKv77arASXIIybxrg==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'1483006709', 0, 1, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (2, 2, 1, 1748927426279, 1748927570264, N'khanhtpse173570@fpt.edu.vn', N'AQAAAAIAAYagAAAAEIY7KCTy41bz9ujBWQeHSP4eefxhg4mlYEy3a5SKLzlczt77RWbAoeDrL5xXJq9wYA==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'0766942380', 0, 0, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (3, 2, 1, 1749025904673, 1749026051477, N'adminstore@yopmail.com', N'AQAAAAIAAYagAAAAEAQEAP5djAFyVX8+bjc7p4rbL6n+JBPpj/Oi95Ljk4Ql6tEqKOVO7BGCGjMvVlc4Uw==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'9385313723', 0, 0, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (4, 2, 1, 1749026157976, 1749026581584, N'userstore@yopmail.com', N'AQAAAAIAAYagAAAAEBPWpLwqsX2enepOrWxcq0DPshsjvWLE9HANVXQx16hLPUiTck4Ra4P12MEz5B4M9w==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'9385315723', 0, 0, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (5, 3, 1, 1749026632779, 1749028825874, N'ancara@yopmail.com', N'AQAAAAIAAYagAAAAEIncDfzkn2kh5NSzr8N95xIvUrozuOAvMAL5vKXB4KokNZPDqrq2pfJxaI8EdafWeA==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'4403404650', 0, 0, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (6, 2, 1, 1749027189801, 1749027316708, N'heungkangkook@yopmail.com', N'AQAAAAIAAYagAAAAECXSPCeTHwSwxbbZF3QftKgg1CsGWsC0yxGWe1nJ1HT/7t4yzmOwjVFxFdBdB7xHfQ==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'7164290383', 0, 0, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (7, 1, 1, 1749027421692, 1749027421692, N'user@example.com', N'AQAAAAIAAYagAAAAEGIA3k8VI9hXzD5aLheAQ1Jto7872KKRoEY4JQOWQ+CSZYsybK6sO7kX57W772nF+w==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'9366309043', 0, 1, 0)
INSERT [dbo].[Users] ([Id], [Version], [IsActive], [CreatedAt], [UpdatedAt], [Email], [Password], [FirstName], [LastName], [Gender], [AvatarKey], [AvatarPublicId], [Phone], [DateOfBirth], [IsFirstLogin], [IsEnableTwoFactor]) VALUES (8, 2, 1, 1749045808812, 1749045884595, N'khanhtranphuong2003@gmail.com', N'AQAAAAIAAYagAAAAEPqlRwIbOnBy3MM+AHb+S33rpO4mSRMoxLLJh+yzdiqu90xaIfHG0d86/qSrhKCiRg==', N'string', N'string', N'string', N'samples/man-portrait', N'samples/man-portrait', N'7020947789', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_OrderProducts_OrderId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderProducts_OrderId] ON [dbo].[OrderProducts]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderProducts_ProductId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderProducts_ProductId] ON [dbo].[OrderProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserAddressId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserAddressId] ON [dbo].[Orders]
(
	[UserAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Payments_OrderId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Payments_OrderId] ON [dbo].[Payments]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Payments_PaymentCode]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Payments_PaymentCode] ON [dbo].[Payments]
(
	[PaymentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductAssets_ProductId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductAssets_ProductId] ON [dbo].[ProductAssets]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductCommentAssets_ProductCommentId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductCommentAssets_ProductCommentId] ON [dbo].[ProductCommentAssets]
(
	[ProductCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductComments_OrderId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductComments_OrderId] ON [dbo].[ProductComments]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductComments_ProductId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductComments_ProductId] ON [dbo].[ProductComments]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductComments_UserId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductComments_UserId] ON [dbo].[ProductComments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_BrandId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_BrandId] ON [dbo].[Products]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Products_ProductCode]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Products_ProductCode] ON [dbo].[Products]
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RefreshTokens_UserId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_RefreshTokens_UserId] ON [dbo].[RefreshTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shippings_OrderId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Shippings_OrderId] ON [dbo].[Shippings]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Shippings_TrackingNumber]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Shippings_TrackingNumber] ON [dbo].[Shippings]
(
	[TrackingNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserAddresses_UserId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserAddresses_UserId] ON [dbo].[UserAddresses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_UserId]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [dbo].[UserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Phone]    Script Date: 6/13/2025 4:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_Phone] ON [dbo].[Users]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Brands] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[OrderProducts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProductAssets] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProductCommentAssets] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProductComments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[RefreshTokens] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Shippings] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserAddresses] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsFirstLogin]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsEnableTwoFactor]
GO
ALTER TABLE [dbo].[OrderProducts]  WITH CHECK ADD  CONSTRAINT [FK_OrderProducts_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderProducts] CHECK CONSTRAINT [FK_OrderProducts_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderProducts]  WITH CHECK ADD  CONSTRAINT [FK_OrderProducts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderProducts] CHECK CONSTRAINT [FK_OrderProducts_Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_UserAddresses_UserAddressId] FOREIGN KEY([UserAddressId])
REFERENCES [dbo].[UserAddresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_UserAddresses_UserAddressId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Orders_OrderId]
GO
ALTER TABLE [dbo].[ProductAssets]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssets_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductAssets] CHECK CONSTRAINT [FK_ProductAssets_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductCommentAssets]  WITH CHECK ADD  CONSTRAINT [FK_ProductCommentAssets_ProductComments_ProductCommentId] FOREIGN KEY([ProductCommentId])
REFERENCES [dbo].[ProductComments] ([Id])
GO
ALTER TABLE [dbo].[ProductCommentAssets] CHECK CONSTRAINT [FK_ProductCommentAssets_ProductComments_ProductCommentId]
GO
ALTER TABLE [dbo].[ProductComments]  WITH CHECK ADD  CONSTRAINT [FK_ProductComments_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[ProductComments] CHECK CONSTRAINT [FK_ProductComments_Orders_OrderId]
GO
ALTER TABLE [dbo].[ProductComments]  WITH CHECK ADD  CONSTRAINT [FK_ProductComments_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductComments] CHECK CONSTRAINT [FK_ProductComments_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductComments]  WITH CHECK ADD  CONSTRAINT [FK_ProductComments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProductComments] CHECK CONSTRAINT [FK_ProductComments_Users_UserId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands_BrandId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users_UserId]
GO
ALTER TABLE [dbo].[Shippings]  WITH CHECK ADD  CONSTRAINT [FK_Shippings_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shippings] CHECK CONSTRAINT [FK_Shippings_Orders_OrderId]
GO
ALTER TABLE [dbo].[UserAddresses]  WITH CHECK ADD  CONSTRAINT [FK_UserAddresses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserAddresses] CHECK CONSTRAINT [FK_UserAddresses_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [EXE_PreOrder] SET  READ_WRITE 
GO
