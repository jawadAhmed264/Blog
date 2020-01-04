USE [master]
GO
/****** Object:  Database [Blog]    Script Date: 1/4/2020 6:17:31 PM ******/
CREATE DATABASE [Blog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Blog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SA\MSSQL\DATA\Blog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Blog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SA\MSSQL\DATA\Blog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Blog] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Blog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Blog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Blog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Blog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Blog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Blog] SET ARITHABORT OFF 
GO
ALTER DATABASE [Blog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Blog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Blog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Blog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Blog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Blog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Blog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Blog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Blog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Blog] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Blog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Blog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Blog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Blog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Blog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Blog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Blog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Blog] SET RECOVERY FULL 
GO
ALTER DATABASE [Blog] SET  MULTI_USER 
GO
ALTER DATABASE [Blog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Blog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Blog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Blog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Blog] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Blog', N'ON'
GO
ALTER DATABASE [Blog] SET QUERY_STORE = OFF
GO
USE [Blog]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Contact] [nvarchar](15) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[JoinDate] [datetime] NULL,
	[LeaveDate] [datetime] NULL,
	[AspnetUser] [nvarchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogContent]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogContent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Content] [text] NULL,
	[BlogPostId] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[BlogPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogPost]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPost](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Summary] [nvarchar](max) NULL,
	[AutherId] [int] NULL,
	[CategoryId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](200) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CSSStyle] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentId] [bigint] NULL,
	[Comment] [nvarchar](max) NULL,
	[UserId] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logger]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logger](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Controller] [nvarchar](100) NULL,
	[Action] [nvarchar](100) NULL,
	[Exception] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [datetime] NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](200) NULL,
	[Url] [nvarchar](max) NULL,
	[BlogPostId] [bigint] NULL,
	[MediaTypeId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediaType]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediaType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MediaType] [nvarchar](200) NULL,
	[FileType] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](100) NULL,
	[BlogPostId] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[JoinDate] [datetime] NULL,
	[AspnetUser] [nvarchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Views]    Script Date: 1/4/2020 6:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Views](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserIP] [nvarchar](max) NULL,
	[UserId] [bigint] NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](200) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [nvarchar](200) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 1/4/2020 6:17:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 1/4/2020 6:17:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 1/4/2020 6:17:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 1/4/2020 6:17:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 1/4/2020 6:17:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 1/4/2020 6:17:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT (NULL) FOR [ParentId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Author]  WITH CHECK ADD FOREIGN KEY([AspnetUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BlogContent]  WITH CHECK ADD FOREIGN KEY([BlogPostId])
REFERENCES [dbo].[BlogPost] ([Id])
GO
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD FOREIGN KEY([AutherId])
REFERENCES [dbo].[Author] ([Id])
GO
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Media]  WITH CHECK ADD FOREIGN KEY([BlogPostId])
REFERENCES [dbo].[BlogPost] ([Id])
GO
ALTER TABLE [dbo].[Media]  WITH CHECK ADD FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MediaType] ([Id])
GO
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD FOREIGN KEY([BlogPostId])
REFERENCES [dbo].[BlogPost] ([Id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([AspnetUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Views]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
USE [master]
GO
ALTER DATABASE [Blog] SET  READ_WRITE 
GO
