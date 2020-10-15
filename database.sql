USE [master]
GO
/****** Object:  Database [pruebademo]    Script Date: 10/1/2020 1:53:26 AM ******/
CREATE DATABASE [pruebademo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'pruebademo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\pruebademo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'pruebademo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\pruebademo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [pruebademo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pruebademo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pruebademo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pruebademo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pruebademo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pruebademo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pruebademo] SET ARITHABORT OFF 
GO
ALTER DATABASE [pruebademo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [pruebademo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pruebademo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pruebademo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pruebademo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pruebademo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pruebademo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pruebademo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pruebademo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pruebademo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [pruebademo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pruebademo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pruebademo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pruebademo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pruebademo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pruebademo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pruebademo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pruebademo] SET RECOVERY FULL 
GO
ALTER DATABASE [pruebademo] SET  MULTI_USER 
GO
ALTER DATABASE [pruebademo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pruebademo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pruebademo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pruebademo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pruebademo] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'pruebademo', N'ON'
GO
ALTER DATABASE [pruebademo] SET QUERY_STORE = OFF
GO
USE [pruebademo]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 10/1/2020 1:53:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cliente] [varchar](255) NOT NULL,
	[Telefono] [varchar](255) NULL,
	[Correo] [varchar](255) NULL,
	[Eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productos]    Script Date: 10/1/2020 1:53:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Precio] [float] NULL,
	[Categoria] [varchar](255) NULL,
	[Eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ventas]    Script Date: 10/1/2020 1:53:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ventas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCliente] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[Total] [float] NULL,
	[Eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ventasitems]    Script Date: 10/1/2020 1:53:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ventasitems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDVenta] [int] NOT NULL,
	[IDProducto] [int] NOT NULL,
	[PrecioUnitario] [float] NULL,
	[Cantidad] [float] NULL,
	[PrecioTotal] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[clientes] ADD  CONSTRAINT [DF_clientes_Eliminado]  DEFAULT ((0)) FOR [Eliminado]
GO
ALTER TABLE [dbo].[productos] ADD  CONSTRAINT [DF_productos_Eliminado]  DEFAULT ((0)) FOR [Eliminado]
GO
ALTER TABLE [dbo].[ventas] ADD  CONSTRAINT [DF_ventas_Eliminado]  DEFAULT ((0)) FOR [Eliminado]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_productos] FOREIGN KEY([ID])
REFERENCES [dbo].[productos] ([ID])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_productos]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_clientes] FOREIGN KEY([IDCliente])
REFERENCES [dbo].[clientes] ([ID])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_clientes]
GO
ALTER TABLE [dbo].[ventasitems]  WITH CHECK ADD  CONSTRAINT [FK_ventasitems_productos] FOREIGN KEY([IDProducto])
REFERENCES [dbo].[productos] ([ID])
GO
ALTER TABLE [dbo].[ventasitems] CHECK CONSTRAINT [FK_ventasitems_productos]
GO
ALTER TABLE [dbo].[ventasitems]  WITH CHECK ADD  CONSTRAINT [FK_ventasitems_ventas] FOREIGN KEY([IDVenta])
REFERENCES [dbo].[ventas] ([ID])
GO
ALTER TABLE [dbo].[ventasitems] CHECK CONSTRAINT [FK_ventasitems_ventas]
GO
USE [master]
GO
ALTER DATABASE [pruebademo] SET  READ_WRITE 
GO
