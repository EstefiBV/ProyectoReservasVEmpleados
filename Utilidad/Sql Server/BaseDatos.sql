USE [master]
GO
/****** Object:  Database [DB_RVuelo]    Script Date: 17/8/2024 23:08:43 ******/
CREATE DATABASE [DB_RVuelo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_HOTEL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DB_HOTEL.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_HOTEL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DB_HOTEL_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DB_RVuelo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_RVuelo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_RVuelo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_RVuelo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_RVuelo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_RVuelo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_RVuelo] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_RVuelo] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DB_RVuelo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_RVuelo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_RVuelo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_RVuelo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_RVuelo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_RVuelo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_RVuelo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_RVuelo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_RVuelo] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DB_RVuelo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_RVuelo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_RVuelo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_RVuelo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_RVuelo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_RVuelo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_RVuelo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_RVuelo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_RVuelo] SET  MULTI_USER 
GO
ALTER DATABASE [DB_RVuelo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_RVuelo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_RVuelo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_RVuelo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_RVuelo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_RVuelo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DB_RVuelo] SET QUERY_STORE = OFF
GO
USE [DB_RVuelo]
GO
/****** Object:  Table [dbo].[AEROLINEA]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AEROLINEA](
	[IdAerolínea] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Codigo] [varchar](10) NOT NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAerolínea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AEROPUERTO]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AEROPUERTO](
	[IdAeropuerto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Ciudad] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAeropuerto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BONIFICACION]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BONIFICACION](
	[IdBonificacion] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[Monto] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBonificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DEDUCCION]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEDUCCION](
	[IdDeduccion] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[Porcentaje] [decimal](5, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDeduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DIAS_LIBRES]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DIAS_LIBRES](
	[IdDiaLibre] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[TipoDiaLibre] [varchar](50) NOT NULL,
	[Motivo] [varchar](max) NULL,
	[NombreEmpleado] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDiaLibre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EQUIPAJE]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EQUIPAJE](
	[IdEquipaje] [int] IDENTITY(1,1) NOT NULL,
	[Peso] [decimal](5, 2) NULL,
	[Precio] [decimal](10, 2) NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEquipaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EVALUACION]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVALUACION](
	[IdEvaluacion] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Objetivos] [varchar](max) NULL,
	[Comentarios] [varchar](max) NULL,
	[Puntaje] [int] NOT NULL,
	[NombreEmpleado] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NOMINA]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NOMINA](
	[IdNomina] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Salario] [decimal](10, 2) NOT NULL,
	[Deducciones] [decimal](10, 2) NOT NULL,
	[Bonificaciones] [decimal](10, 2) NOT NULL,
	[SalarioNeto]  AS (([Salario]-[Deducciones])+[Bonificaciones]),
	[NombreEmpleado] [varchar](100) NULL,
	[NombreRol] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNomina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERMISO]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERMISO](
	[IdPermiso] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[PermisoDescripcion] [varchar](255) NOT NULL,
	[NombreEmpleado] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERSONA]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERSONA](
	[IdPersona] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [varchar](15) NULL,
	[Documento] [varchar](15) NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Clave] [varchar](50) NULL,
	[IdTipoPersona] [int] NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVA_VUELO]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVA_VUELO](
	[IdReserva] [int] IDENTITY(1,1) NOT NULL,
	[IdVuelo] [int] NOT NULL,
	[IdPersona] [int] NOT NULL,
	[IdEquipaje] [int] NULL,
	[IdTipoAsiento] [int] NULL,
	[PrecioVuelo] [decimal](10, 2) NOT NULL,
	[PrecioAsiento] [decimal](10, 2) NULL,
	[PrecioEquipaje] [decimal](10, 2) NULL,
	[PrecioTotal] [decimal](10, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
	[CantidadPasajeros] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REUNION]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REUNION](
	[IdReunion] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[HoraInicio] [time](7) NOT NULL,
	[HoraFin] [time](7) NOT NULL,
	[Lugar] [varchar](255) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[NombreEmpleado] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdReunion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROL]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROL](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](100) NOT NULL,
	[Descripcion] [varchar](255) NULL,
	[SalarioBase] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPO_ASIENTO]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO_ASIENTO](
	[IdTipoAsiento] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[PrecioExtra] [decimal](10, 2) NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoAsiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPO_PERSONA]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO_PERSONA](
	[IdTipoPersona] [int] NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TURNO]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TURNO](
	[IdTurno] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[Dias] [varchar](100) NOT NULL,
	[HoraInicio] [time](7) NOT NULL,
	[HoraFin] [time](7) NOT NULL,
	[NombreEmpleado] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTurno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VUELO]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VUELO](
	[IdVuelo] [int] IDENTITY(1,1) NOT NULL,
	[NumeroVuelo] [varchar](10) NULL,
	[IdAerolínea] [int] NULL,
	[Origen] [varchar](50) NULL,
	[Destino] [varchar](50) NULL,
	[FechaSalida] [datetime] NULL,
	[FechaLlegada] [datetime] NULL,
	[Precio] [decimal](10, 2) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
	[NumeroPasajeros] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVuelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AEROLINEA] ON 

INSERT [dbo].[AEROLINEA] ([IdAerolínea], [Nombre], [Codigo], [Estado], [FechaCreacion]) VALUES (2, N'Wingo', N'4820', 1, CAST(N'2024-07-02T18:59:47.610' AS DateTime))
INSERT [dbo].[AEROLINEA] ([IdAerolínea], [Nombre], [Codigo], [Estado], [FechaCreacion]) VALUES (3, N'Avianca', N'3634', 1, CAST(N'2024-07-12T17:46:22.780' AS DateTime))
INSERT [dbo].[AEROLINEA] ([IdAerolínea], [Nombre], [Codigo], [Estado], [FechaCreacion]) VALUES (5, N'United Airlines', N'3737', 1, CAST(N'2024-07-22T15:10:20.430' AS DateTime))
INSERT [dbo].[AEROLINEA] ([IdAerolínea], [Nombre], [Codigo], [Estado], [FechaCreacion]) VALUES (6, N'Volares', N'3837', 1, CAST(N'2024-07-22T15:10:41.247' AS DateTime))
SET IDENTITY_INSERT [dbo].[AEROLINEA] OFF
GO
SET IDENTITY_INSERT [dbo].[AEROPUERTO] ON 

INSERT [dbo].[AEROPUERTO] ([IdAeropuerto], [Nombre], [Ciudad], [Estado], [FechaCreacion]) VALUES (1, N'Grande', N'China', 1, CAST(N'2024-07-02T18:46:58.540' AS DateTime))
INSERT [dbo].[AEROPUERTO] ([IdAeropuerto], [Nombre], [Ciudad], [Estado], [FechaCreacion]) VALUES (2, N'Principal', N'Canada', 1, CAST(N'2024-07-12T17:45:30.950' AS DateTime))
SET IDENTITY_INSERT [dbo].[AEROPUERTO] OFF
GO
SET IDENTITY_INSERT [dbo].[BONIFICACION] ON 

INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (1, 1, N'Bono por cumplimiento de horarios', CAST(500.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (2, 2, N'Bono por horas extras', CAST(300.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (3, 3, N'Bono por servicio al cliente', CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (4, 4, N'Bono por mantenimiento preventivo', CAST(400.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (5, 5, N'Bono por manejo de tráfico', CAST(600.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (6, 6, N'Bono por planificación de vuelos', CAST(250.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (7, 7, N'Bono por ventas logradas', CAST(150.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (8, 8, N'Bono por eficiencia en carga y descarga', CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (9, 9, N'Bono por gestión de operaciones', CAST(700.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (10, 10, N'Bono por soporte administrativo', CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (11, 11, N'Bono por retención de empleados', CAST(500.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (12, 12, N'Bono por seguridad de vuelos', CAST(300.00 AS Decimal(10, 2)))
INSERT [dbo].[BONIFICACION] ([IdBonificacion], [IdRol], [Descripcion], [Monto]) VALUES (13, 13, N'Bono por entrenamiento exitoso', CAST(400.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[BONIFICACION] OFF
GO
SET IDENTITY_INSERT [dbo].[DEDUCCION] ON 

INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (1, N'Impuesto sobre la renta', CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (2, N'Seguro social', CAST(6.50 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (3, N'Ahorro para el retiro', CAST(2.00 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (4, N'Seguro médico', CAST(3.00 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (5, N'Fondo de vivienda', CAST(1.50 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (6, N'Préstamo personal', CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (7, N'Cuota sindical', CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (8, N'Seguro de vida', CAST(0.75 AS Decimal(5, 2)))
INSERT [dbo].[DEDUCCION] ([IdDeduccion], [Descripcion], [Porcentaje]) VALUES (9, N'Seguro de accidentes', CAST(0.50 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[DEDUCCION] OFF
GO
SET IDENTITY_INSERT [dbo].[DIAS_LIBRES] ON 

INSERT [dbo].[DIAS_LIBRES] ([IdDiaLibre], [IdPersona], [Fecha], [TipoDiaLibre], [Motivo], [NombreEmpleado]) VALUES (2, 13, CAST(N'2024-08-29' AS Date), N'Día de la Virgen', N'Feriado festivo', N'Lynda  Boza Villalobos')
SET IDENTITY_INSERT [dbo].[DIAS_LIBRES] OFF
GO
SET IDENTITY_INSERT [dbo].[EQUIPAJE] ON 

INSERT [dbo].[EQUIPAJE] ([IdEquipaje], [Peso], [Precio], [Estado]) VALUES (1, CAST(10.00 AS Decimal(5, 2)), CAST(3000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[EQUIPAJE] ([IdEquipaje], [Peso], [Precio], [Estado]) VALUES (2, CAST(25.00 AS Decimal(5, 2)), CAST(2345.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[EQUIPAJE] ([IdEquipaje], [Peso], [Precio], [Estado]) VALUES (4, CAST(40.00 AS Decimal(5, 2)), CAST(440.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[EQUIPAJE] OFF
GO
SET IDENTITY_INSERT [dbo].[EVALUACION] ON 

INSERT [dbo].[EVALUACION] ([IdEvaluacion], [IdPersona], [Fecha], [Objetivos], [Comentarios], [Puntaje], [NombreEmpleado]) VALUES (2, 13, CAST(N'2024-08-10' AS Date), N'Terminar la tarea de química. ', N'Hizo la tarea correctamente, pero utilizo Copilot para completarla, por lo que se le van a quitar puntos por ello.', 8, N'Lynda  Boza Villalobos')
SET IDENTITY_INSERT [dbo].[EVALUACION] OFF
GO
SET IDENTITY_INSERT [dbo].[NOMINA] ON 

INSERT [dbo].[NOMINA] ([IdNomina], [IdPersona], [Fecha], [Salario], [Deducciones], [Bonificaciones], [NombreEmpleado], [NombreRol]) VALUES (2, 19, CAST(N'0001-01-01' AS Date), CAST(2200.00 AS Decimal(10, 2)), CAST(594.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Maria Boza', N'Agente de Rampa')
INSERT [dbo].[NOMINA] ([IdNomina], [IdPersona], [Fecha], [Salario], [Deducciones], [Bonificaciones], [NombreEmpleado], [NombreRol]) VALUES (3, 13, CAST(N'0001-01-01' AS Date), CAST(2200.00 AS Decimal(10, 2)), CAST(473.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Lynda  Boza Villalobos', N'Agente de Rampa')
SET IDENTITY_INSERT [dbo].[NOMINA] OFF
GO
SET IDENTITY_INSERT [dbo].[PERMISO] ON 

INSERT [dbo].[PERMISO] ([IdPermiso], [IdPersona], [PermisoDescripcion], [NombreEmpleado]) VALUES (1, 13, N'Permiso para ingresar a áreas donde se encuentra la maquinaria pesada ', N'Lynda  Boza Villalobos')
SET IDENTITY_INSERT [dbo].[PERMISO] OFF
GO
SET IDENTITY_INSERT [dbo].[PERSONA] ON 

INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (1, N'PASAPORTE', N'45454589', N'Estefania', N'Boza', N'ebv2310@gmail.com', N'EfE?2310', 1, 1, CAST(N'2024-06-27T14:13:38.857' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (2, N'', N'4353434', N'Kim', N'Boza', N'kboza@gmail.com', N'8900', 2, 1, CAST(N'2024-06-27T14:13:38.857' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (9, N'PASAPORTE', N'34232334', N'Jorge', N'Boza', N'jboza@gmail.com', NULL, 3, 1, CAST(N'2024-06-27T14:13:38.873' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (13, N'PASAPORTE', N'56789799', N'Lynda ', N'Boza Villalobos', N'lyndaboza@gmail.com', N'8910', 2, 1, CAST(N'2024-07-21T15:27:40.697' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (19, N'DNI', N'67889990', N'Maria', N'Boza', N'Mariab@gmail.com', N'Nara8900', 3, 1, CAST(N'2024-07-22T00:27:47.800' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (20, N'', N'77868678', N'Kevin', N'Villalobos ', N'kevinL@gmail.com', N'Manchitas', 1, 1, CAST(N'2024-07-22T14:40:12.003' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (29, N'', N'6789876877', N'José', N'Perez Chavez', N'Jperez@gmail.com', N'Nara8900?2310', 2, 1, CAST(N'2024-08-15T14:16:56.610' AS DateTime))
INSERT [dbo].[PERSONA] ([IdPersona], [TipoDocumento], [Documento], [Nombre], [Apellido], [Correo], [Clave], [IdTipoPersona], [Estado], [FechaCreacion]) VALUES (30, N'', N'8797877897', N'Mario', N'Víquez Chávez', N'Mviquez@gmail.com', N'EfE?2310', 2, 1, CAST(N'2024-08-15T20:47:40.170' AS DateTime))
SET IDENTITY_INSERT [dbo].[PERSONA] OFF
GO
SET IDENTITY_INSERT [dbo].[RESERVA_VUELO] ON 

INSERT [dbo].[RESERVA_VUELO] ([IdReserva], [IdVuelo], [IdPersona], [IdEquipaje], [IdTipoAsiento], [PrecioVuelo], [PrecioAsiento], [PrecioEquipaje], [PrecioTotal], [Estado], [CantidadPasajeros]) VALUES (1, 1, 1, 1, 1, CAST(21000.00 AS Decimal(10, 2)), CAST(56000.00 AS Decimal(10, 2)), CAST(3000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1)
INSERT [dbo].[RESERVA_VUELO] ([IdReserva], [IdVuelo], [IdPersona], [IdEquipaje], [IdTipoAsiento], [PrecioVuelo], [PrecioAsiento], [PrecioEquipaje], [PrecioTotal], [Estado], [CantidadPasajeros]) VALUES (18, 4, 1, 2, 1, CAST(19.00 AS Decimal(10, 2)), CAST(56000.00 AS Decimal(10, 2)), CAST(2345.00 AS Decimal(10, 2)), CAST(226421.00 AS Decimal(10, 2)), 1, 4)
INSERT [dbo].[RESERVA_VUELO] ([IdReserva], [IdVuelo], [IdPersona], [IdEquipaje], [IdTipoAsiento], [PrecioVuelo], [PrecioAsiento], [PrecioEquipaje], [PrecioTotal], [Estado], [CantidadPasajeros]) VALUES (13, 6, 1, 2, 1, CAST(145.17 AS Decimal(10, 2)), CAST(56000.00 AS Decimal(10, 2)), CAST(2345.00 AS Decimal(10, 2)), CAST(170780.51 AS Decimal(10, 2)), 0, 3)
INSERT [dbo].[RESERVA_VUELO] ([IdReserva], [IdVuelo], [IdPersona], [IdEquipaje], [IdTipoAsiento], [PrecioVuelo], [PrecioAsiento], [PrecioEquipaje], [PrecioTotal], [Estado], [CantidadPasajeros]) VALUES (11, 3, 1, 1, 1, CAST(6789.00 AS Decimal(10, 2)), CAST(56000.00 AS Decimal(10, 2)), CAST(3000.00 AS Decimal(10, 2)), CAST(191367.00 AS Decimal(10, 2)), 0, 3)
INSERT [dbo].[RESERVA_VUELO] ([IdReserva], [IdVuelo], [IdPersona], [IdEquipaje], [IdTipoAsiento], [PrecioVuelo], [PrecioAsiento], [PrecioEquipaje], [PrecioTotal], [Estado], [CantidadPasajeros]) VALUES (15, 3, 20, 4, 2, CAST(6789.00 AS Decimal(10, 2)), CAST(18000.00 AS Decimal(10, 2)), CAST(440.00 AS Decimal(10, 2)), CAST(50018.00 AS Decimal(10, 2)), 0, 2)
INSERT [dbo].[RESERVA_VUELO] ([IdReserva], [IdVuelo], [IdPersona], [IdEquipaje], [IdTipoAsiento], [PrecioVuelo], [PrecioAsiento], [PrecioEquipaje], [PrecioTotal], [Estado], [CantidadPasajeros]) VALUES (16, 4, 20, 2, 1, CAST(19.00 AS Decimal(10, 2)), CAST(56000.00 AS Decimal(10, 2)), CAST(2345.00 AS Decimal(10, 2)), CAST(170402.00 AS Decimal(10, 2)), 1, 3)
SET IDENTITY_INSERT [dbo].[RESERVA_VUELO] OFF
GO
SET IDENTITY_INSERT [dbo].[REUNION] ON 

INSERT [dbo].[REUNION] ([IdReunion], [IdPersona], [Fecha], [HoraInicio], [HoraFin], [Lugar], [Descripcion], [NombreEmpleado]) VALUES (1, 19, CAST(N'2024-08-07' AS Date), CAST(N'22:21:00' AS Time), CAST(N'23:21:00' AS Time), N'Oficina Central', N'Reunión Prueba', N'Maria Boza')
INSERT [dbo].[REUNION] ([IdReunion], [IdPersona], [Fecha], [HoraInicio], [HoraFin], [Lugar], [Descripcion], [NombreEmpleado]) VALUES (2, 13, CAST(N'2024-08-16' AS Date), CAST(N'09:44:00' AS Time), CAST(N'10:45:00' AS Time), N'Oficina Central', N'Reunión importante para discutir la comida del gato.
', N'Lynda  Boza Villalobos')
SET IDENTITY_INSERT [dbo].[REUNION] OFF
GO
SET IDENTITY_INSERT [dbo].[ROL] ON 

INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (1, N'Piloto', N'Responsable de la operación y seguridad del vuelo.', CAST(8000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (2, N'Copiloto', N'Asiste al piloto en la operación del vuelo.', CAST(6000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (3, N'Sobrecargo', N'Proporciona servicio y seguridad a los pasajeros.', CAST(3000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (4, N'Mecánico de Avión', N'Mantiene y repara los aviones.', CAST(4000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (5, N'Controlador de Tráfico Aéreo', N'Supervisa el tráfico aéreo y asegura la seguridad.', CAST(7000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (6, N'Despachador de Vuelos', N'Planea y monitorea los vuelos.', CAST(3500.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (7, N'Agente de Ventas', N'Gestiona la venta de boletos y atención al cliente.', CAST(2500.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (8, N'Agente de Rampa', N'Carga y descarga el equipaje de los aviones.', CAST(2200.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (9, N'Gerente de Operaciones', N'Supervisa las operaciones diarias de la aerolínea.', CAST(9000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (10, N'Asistente Administrativo', N'Proporciona soporte administrativo a la aerolínea.', CAST(2000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (11, N'Gerente de Recursos Humanos', N'Gestiona el personal y las políticas de la aerolínea.', CAST(7500.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (12, N'Especialista en Seguridad', N'Asegura el cumplimiento de las normas de seguridad.', CAST(5000.00 AS Decimal(10, 2)))
INSERT [dbo].[ROL] ([IdRol], [NombreRol], [Descripcion], [SalarioBase]) VALUES (13, N'Instructor de Vuelo', N'Capacita a pilotos y personal de vuelo.', CAST(5500.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[ROL] OFF
GO
SET IDENTITY_INSERT [dbo].[TIPO_ASIENTO] ON 

INSERT [dbo].[TIPO_ASIENTO] ([IdTipoAsiento], [Descripcion], [PrecioExtra], [Estado]) VALUES (1, N'Primera clase', CAST(56000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[TIPO_ASIENTO] ([IdTipoAsiento], [Descripcion], [PrecioExtra], [Estado]) VALUES (2, N'Segunda Clase', CAST(18000.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[TIPO_ASIENTO] OFF
GO
INSERT [dbo].[TIPO_PERSONA] ([IdTipoPersona], [Descripcion], [Estado], [FechaCreacion]) VALUES (1, N'Administrador', 1, CAST(N'2024-06-27T14:13:38.830' AS DateTime))
INSERT [dbo].[TIPO_PERSONA] ([IdTipoPersona], [Descripcion], [Estado], [FechaCreacion]) VALUES (2, N'Empleado', 1, CAST(N'2024-06-27T14:13:38.830' AS DateTime))
INSERT [dbo].[TIPO_PERSONA] ([IdTipoPersona], [Descripcion], [Estado], [FechaCreacion]) VALUES (3, N'Cliente', 1, CAST(N'2024-06-27T14:13:38.830' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TURNO] ON 

INSERT [dbo].[TURNO] ([IdTurno], [IdPersona], [Dias], [HoraInicio], [HoraFin], [NombreEmpleado]) VALUES (2, 19, N'Jueves', CAST(N'18:51:00' AS Time), CAST(N'00:54:00' AS Time), N'Maria Boza')
INSERT [dbo].[TURNO] ([IdTurno], [IdPersona], [Dias], [HoraInicio], [HoraFin], [NombreEmpleado]) VALUES (3, 13, N'Sábado y Domingo', CAST(N'08:52:00' AS Time), CAST(N'16:52:00' AS Time), N'Lynda  Boza Villalobos')
SET IDENTITY_INSERT [dbo].[TURNO] OFF
GO
SET IDENTITY_INSERT [dbo].[VUELO] ON 

INSERT [dbo].[VUELO] ([IdVuelo], [NumeroVuelo], [IdAerolínea], [Origen], [Destino], [FechaSalida], [FechaLlegada], [Precio], [Estado], [FechaCreacion], [NumeroPasajeros]) VALUES (1, N'456', 2, N'ORY', N'LIS', CAST(N'2024-10-23T03:30:00.000' AS DateTime), CAST(N'2024-10-23T08:00:00.000' AS DateTime), CAST(100.00 AS Decimal(10, 2)), 1, CAST(N'2024-07-02T19:34:08.400' AS DateTime), 78)
INSERT [dbo].[VUELO] ([IdVuelo], [NumeroVuelo], [IdAerolínea], [Origen], [Destino], [FechaSalida], [FechaLlegada], [Precio], [Estado], [FechaCreacion], [NumeroPasajeros]) VALUES (2, N'346', 3, N'Singapur', N'Canada', CAST(N'2024-10-23T07:00:00.000' AS DateTime), CAST(N'2024-10-23T12:30:00.000' AS DateTime), CAST(34567.00 AS Decimal(10, 2)), 1, CAST(N'2024-07-12T17:47:58.670' AS DateTime), 78)
INSERT [dbo].[VUELO] ([IdVuelo], [NumeroVuelo], [IdAerolínea], [Origen], [Destino], [FechaSalida], [FechaLlegada], [Precio], [Estado], [FechaCreacion], [NumeroPasajeros]) VALUES (3, N'567', 2, N'Australia', N'Filipinas', CAST(N'2024-10-23T08:20:00.000' AS DateTime), CAST(N'2024-10-23T10:23:00.000' AS DateTime), CAST(6789.00 AS Decimal(10, 2)), 1, CAST(N'2024-07-16T18:34:09.043' AS DateTime), 52)
INSERT [dbo].[VUELO] ([IdVuelo], [NumeroVuelo], [IdAerolínea], [Origen], [Destino], [FechaSalida], [FechaLlegada], [Precio], [Estado], [FechaCreacion], [NumeroPasajeros]) VALUES (4, N'678', 2, N'Mexico', N'Puerto Rico', CAST(N'2024-10-24T06:50:00.000' AS DateTime), CAST(N'2024-10-25T11:59:00.000' AS DateTime), CAST(19.00 AS Decimal(10, 2)), 1, CAST(N'2024-07-16T23:48:53.327' AS DateTime), 35)
INSERT [dbo].[VUELO] ([IdVuelo], [NumeroVuelo], [IdAerolínea], [Origen], [Destino], [FechaSalida], [FechaLlegada], [Precio], [Estado], [FechaCreacion], [NumeroPasajeros]) VALUES (6, N'6789', 2, N'CDG', N'FCO', CAST(N'2024-07-25T03:32:00.000' AS DateTime), CAST(N'2024-07-25T06:38:00.000' AS DateTime), CAST(145.17 AS Decimal(10, 2)), 1, CAST(N'2024-07-22T00:32:57.560' AS DateTime), 29)
SET IDENTITY_INSERT [dbo].[VUELO] OFF
GO
ALTER TABLE [dbo].[AEROLINEA] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[AEROLINEA] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[AEROPUERTO] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[AEROPUERTO] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[EQUIPAJE] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[PERSONA] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[PERSONA] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[RESERVA_VUELO] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[RESERVA_VUELO] ADD  DEFAULT ((1)) FOR [CantidadPasajeros]
GO
ALTER TABLE [dbo].[TIPO_ASIENTO] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[TIPO_PERSONA] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[TIPO_PERSONA] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[VUELO] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[VUELO] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[BONIFICACION]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[ROL] ([IdRol])
GO
ALTER TABLE [dbo].[DIAS_LIBRES]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[EVALUACION]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[NOMINA]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[PERMISO]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[PERSONA]  WITH CHECK ADD FOREIGN KEY([IdTipoPersona])
REFERENCES [dbo].[TIPO_PERSONA] ([IdTipoPersona])
GO
ALTER TABLE [dbo].[RESERVA_VUELO]  WITH CHECK ADD FOREIGN KEY([IdEquipaje])
REFERENCES [dbo].[EQUIPAJE] ([IdEquipaje])
GO
ALTER TABLE [dbo].[RESERVA_VUELO]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[RESERVA_VUELO]  WITH CHECK ADD FOREIGN KEY([IdTipoAsiento])
REFERENCES [dbo].[TIPO_ASIENTO] ([IdTipoAsiento])
GO
ALTER TABLE [dbo].[RESERVA_VUELO]  WITH CHECK ADD FOREIGN KEY([IdVuelo])
REFERENCES [dbo].[VUELO] ([IdVuelo])
GO
ALTER TABLE [dbo].[REUNION]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[TURNO]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[PERSONA] ([IdPersona])
GO
ALTER TABLE [dbo].[VUELO]  WITH CHECK ADD FOREIGN KEY([IdAerolínea])
REFERENCES [dbo].[AEROLINEA] ([IdAerolínea])
GO
/****** Object:  StoredProcedure [dbo].[ModificarReserva]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarReserva]
    @IdReserva INT,
    @IdVuelo INT,
    @IdPersona INT,
    @IdEquipaje INT,
    @IdTipoAsiento INT,
    @PrecioVuelo DECIMAL(10, 2),
    @PrecioAsiento DECIMAL(10, 2),
    @PrecioEquipaje DECIMAL(10, 2),
    @PrecioTotal DECIMAL(10, 2),
    @Estado BIT
AS
BEGIN
    UPDATE RESERVA_VUELO
    SET IdVuelo = @IdVuelo,
        IdPersona = @IdPersona,
        IdEquipaje = @IdEquipaje,
        IdTipoAsiento = @IdTipoAsiento,
        PrecioVuelo = @PrecioVuelo,
        PrecioAsiento = @PrecioAsiento,
        PrecioEquipaje = @PrecioEquipaje,
        PrecioTotal = @PrecioTotal,
        Estado = @Estado
    WHERE IdReserva = @IdReserva;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_listarEquipajes]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_listarEquipajes]
AS
BEGIN
    SELECT IdEquipaje, Peso, Precio, Estado
    FROM EQUIPAJE
END
GO
/****** Object:  StoredProcedure [dbo].[sp_listarReservasFiltrado]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listarReservasFiltrado]
    @VueloId INT = NULL,
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    SELECT rv.IdReserva, p.Nombre, p.Apellido, p.Documento, p.Correo, v.IdVuelo, v.NumeroVuelo, v.Origen, v.Destino,
           CONVERT(char(10), rv.FechaReserva, 103) AS [FechaReserva], rv.PrecioVuelo, rv.PrecioAsiento, rv.PrecioEquipaje, rv.PrecioTotal,
           rv.TotalPagado, rv.CostoPenalidad, rv.Estado
    FROM RESERVA_VUELO rv
    INNER JOIN PERSONA p ON p.IdPersona = rv.IdPersona
    INNER JOIN VUELO v ON v.IdVuelo = rv.IdVuelo
    WHERE (@VueloId IS NULL OR v.IdVuelo = @VueloId)
      AND rv.FechaReserva BETWEEN @FechaInicio AND @FechaFin
END
GO
/****** Object:  StoredProcedure [dbo].[sp_listarTiposAsiento]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listarTiposAsiento]
AS
BEGIN
    SELECT IdTipoAsiento, Descripcion, PrecioExtra, Estado
    FROM TIPO_ASIENTO
END
GO
/****** Object:  StoredProcedure [dbo].[sp_listarVuelos]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listarVuelos]
AS
BEGIN
    SELECT v.IdVuelo, v.NumeroVuelo, v.Origen, v.Destino, v.FechaSalida, v.FechaLlegada, a.Nombre as NombreAerolinea, v.Estado
    FROM VUELO v
    INNER JOIN AEROLINEA a ON v.IdAerolínea = a.IdAerolínea
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarAerolinea]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCEDIMIENTO PARA MODIFICAR AEROLÍNEA
CREATE PROCEDURE [dbo].[sp_ModificarAerolinea](
    @IdAerolínea INT,
    @Nombre VARCHAR(50),
    @Codigo VARCHAR(10),
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM AEROLINEA WHERE (Nombre = @Nombre OR Codigo = @Codigo) AND IdAerolínea != @IdAerolínea)
    BEGIN
        UPDATE AEROLINEA SET 
            Nombre = @Nombre,
            Codigo = @Codigo,
            Estado = @Estado
        WHERE IdAerolínea = @IdAerolínea;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarAeropuerto]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCEDIMIENTO PARA MODIFICAR AEROPUERTO
CREATE PROCEDURE [dbo].[sp_ModificarAeropuerto](
    @IdAeropuerto INT,
    @Nombre VARCHAR(50),
    @Ciudad VARCHAR(50),
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM AEROPUERTO WHERE (Nombre = @Nombre) AND IdAeropuerto != @IdAeropuerto)
    BEGIN
        UPDATE AEROPUERTO SET 
            Nombre = @Nombre,
            Ciudad = @Ciudad,
            Estado = @Estado
        WHERE IdAeropuerto = @IdAeropuerto;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarCategoria]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA MODIFICAR CATEGORIA
create procedure [dbo].[sp_ModificarCategoria](
@IdCategoria int,
@Descripcion varchar(60),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion =@Descripcion and IdCategoria != @IdCategoria)
		
		update CATEGORIA set 
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdCategoria = @IdCategoria
	ELSE
		SET @Resultado = 0

end


GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarDiaLibre]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR DIA LIBRE
CREATE PROCEDURE [dbo].[sp_ModificarDiaLibre](
    @IdDiaLibre INT,
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @Fecha DATE,
    @TipoDiaLibre VARCHAR(50),
    @Motivo VARCHAR(MAX),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF EXISTS (SELECT * FROM DIAS_LIBRES WHERE IdDiaLibre = @IdDiaLibre)
    BEGIN
        UPDATE DIAS_LIBRES
        SET IdPersona = @IdPersona,
            NombreEmpleado = @NombreEmpleado,
            Fecha = @Fecha,
            TipoDiaLibre = @TipoDiaLibre,
            Motivo = @Motivo
        WHERE IdDiaLibre = @IdDiaLibre;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarEquipaje]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR EQUIPAJE
CREATE PROCEDURE [dbo].[sp_ModificarEquipaje](
    @IdEquipaje INT,
    @Peso DECIMAL(5, 2),
    @Precio DECIMAL(10, 2),
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM EQUIPAJE WHERE Peso = @Peso AND Precio = @Precio AND IdEquipaje != @IdEquipaje)
    BEGIN
        UPDATE EQUIPAJE SET 
            Peso = @Peso,
            Precio = @Precio,
            Estado = @Estado
        WHERE IdEquipaje = @IdEquipaje;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarEvaluacion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR EVALUACION
CREATE PROCEDURE [dbo].[sp_ModificarEvaluacion](
    @IdEvaluacion INT,
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @Fecha DATE,
    @Objetivos VARCHAR(Max),
    @Comentarios VARCHAR(Max),
    @Puntaje INT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF EXISTS (SELECT * FROM EVALUACION WHERE IdEvaluacion = @IdEvaluacion)
    BEGIN
        UPDATE EVALUACION
        SET IdPersona = @IdPersona,
            NombreEmpleado = @NombreEmpleado,
            Fecha = @Fecha,
            Objetivos = @Objetivos,
            Comentarios = @Comentarios,
            Puntaje = @Puntaje
        WHERE IdEvaluacion = @IdEvaluacion;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarHabitacion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA MODIFICAR HABITACION
create procedure [dbo].[sp_ModificarHabitacion](
@IdHabitacion int,
@Numero varchar(50),
@Detalle varchar(100),
@Precio decimal(10,2),
@IdPiso varchar(50),
@IdCategoria varchar(50),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM HABITACION WHERE Numero =@Numero and IdHabitacion != @IdHabitacion)
		
		update HABITACION set 
		Numero = @Numero,
		Detalle = @Detalle,
		Precio = @Precio,
		IdPiso = @IdPiso,
		IdCategoria = @IdCategoria,
		Estado = @Estado
		where IdHabitacion = @IdHabitacion
	ELSE
		SET @Resultado = 0

end


GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarNomina]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR NOMINA
Create PROCEDURE [dbo].[sp_ModificarNomina]
(
    @IdNomina INT,
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
	@NombreRol VARCHAR(100),
    @Fecha DATE,
    @Salario DECIMAL(10, 2),
    @Deducciones DECIMAL(10, 2),
    @Bonificaciones DECIMAL(10, 2),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF EXISTS (SELECT * FROM NOMINA WHERE IdNomina = @IdNomina)
    BEGIN
        UPDATE NOMINA
        SET IdPersona = @IdPersona,
            NombreEmpleado = @NombreEmpleado,
			NombreRol = @NombreRol,
            Fecha = @Fecha,
            Salario = @Salario,
            Deducciones = @Deducciones,
            Bonificaciones = @Bonificaciones
        WHERE IdNomina = @IdNomina;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarPermiso]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR PERMISO
CREATE PROCEDURE [dbo].[sp_ModificarPermiso](
    @IdPermiso INT,
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @PermisoDescripcion VARCHAR(255),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF EXISTS (SELECT * FROM PERMISO WHERE IdPermiso = @IdPermiso)
    BEGIN
        UPDATE PERMISO
        SET IdPersona = @IdPersona,
            NombreEmpleado = @NombreEmpleado,
            PermisoDescripcion = @PermisoDescripcion
        WHERE IdPermiso = @IdPermiso;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarPersona]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA MODIFICAR PERSONA
create procedure [dbo].[sp_ModificarPersona](
@IdPersona int,
@TipoDocumento varchar(50),
@Documento varchar(50),
@Nombre varchar(50),
@Apellido varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdTipoPersona int,
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM persona WHERE Documento =@Documento and IdPersona != @IdPersona)
		
		update PERSONA set
		TipoDocumento = @TipoDocumento,
		Documento = @Documento,
		Nombre = @Nombre,
		Apellido = @Apellido,
		Correo = @Correo,
		IdTipoPersona = @IdTipoPersona,
		Estado = @Estado
		where IdPersona = @IdPersona
	ELSE
		SET @Resultado = 0

end


GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarPiso]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA MODIFICAR PISOS
create procedure [dbo].[sp_ModificarPiso](
@IdPiso int,
@Descripcion varchar(60),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PISO WHERE Descripcion =@Descripcion and IdPiso != @IdPiso)
		
		update PISO set 
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdPiso = @IdPiso
	ELSE
		SET @Resultado = 0

end


GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarProducto]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA MODIFICAR PRODUCTO
create procedure [dbo].[sp_ModificarProducto](
@IdProducto int,
@Nombre varchar(50),
@Detalle varchar(50),
@Precio varchar(50),
@Cantidad varchar(50),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre =@Nombre and IdProducto != @IdProducto)
		
		update PRODUCTO set 
		Nombre = @Nombre,
		Detalle = @Detalle,
		Precio = @Precio,
		Cantidad = @Cantidad,
		Estado = @Estado
		where IdProducto = @IdProducto
	ELSE
		SET @Resultado = 0

end


GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarReserva]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCEDIMIENTO PARA MODIFICAR RESERVA
Create PROCEDURE [dbo].[sp_ModificarReserva](
    @IdReserva INT,
    @IdVuelo INT,
    @IdPersona INT,
    @IdEquipaje INT = NULL,
    @IdTipoAsiento INT = NULL,
    @PrecioVuelo DECIMAL(10, 2),
    @PrecioAsiento DECIMAL(10, 2) = NULL,
    @PrecioEquipaje DECIMAL(10, 2) = NULL,
    @PrecioTotal DECIMAL(10, 2),
    @CantidadPasajeros INT,
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    BEGIN TRY
        UPDATE RESERVA SET 
            IdVuelo = @IdVuelo,
            IdPersona = @IdPersona,
            IdEquipaje = @IdEquipaje,
            IdTipoAsiento = @IdTipoAsiento,
            PrecioVuelo = @PrecioVuelo,
            PrecioAsiento = @PrecioAsiento,
            PrecioEquipaje = @PrecioEquipaje,
            PrecioTotal = @PrecioTotal,
            CantidadPasajeros = @CantidadPasajeros,
            Estado = @Estado
        WHERE IdReserva = @IdReserva;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarReservaVuelo]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCEDIMIENTO PARA MODIFICAR RESERVA_VUELO Y ACTUALIZAR VUELO
Create PROCEDURE [dbo].[sp_ModificarReservaVuelo](
    @IdReserva INT,
    @IdVuelo INT,
    @IdPersona INT,
    @IdEquipaje INT = NULL,
    @IdTipoAsiento INT = NULL,
    @PrecioVuelo DECIMAL(10, 2),
    @PrecioAsiento DECIMAL(10, 2) = NULL,
    @PrecioEquipaje DECIMAL(10, 2) = NULL,
    @PrecioTotal DECIMAL(10, 2),
    @CantidadPasajeros INT,
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 1;

    -- Inicializar la variable para la cantidad actual de pasajeros
    DECLARE @NumeroPasajerosActual INT;
    
    BEGIN TRY
        -- Verificar si no existe otra reserva con el mismo vuelo y persona
        IF NOT EXISTS (
            SELECT * FROM RESERVA_VUELO 
            WHERE IdVuelo = @IdVuelo 
              AND IdPersona = @IdPersona 
              AND IdReserva != @IdReserva
        )
        BEGIN
            -- Obtener la cantidad actual de pasajeros para el vuelo
            SELECT @NumeroPasajerosActual = NumeroPasajeros
            FROM VUELO
            WHERE IdVuelo = @IdVuelo;

            -- Actualizar la reserva existente
            UPDATE RESERVA_VUELO
            SET IdVuelo = @IdVuelo,
                IdPersona = @IdPersona,
                IdEquipaje = @IdEquipaje,
                IdTipoAsiento = @IdTipoAsiento,
                PrecioVuelo = @PrecioVuelo,
                PrecioAsiento = @PrecioAsiento,
                PrecioEquipaje = @PrecioEquipaje,
                PrecioTotal = @PrecioTotal,
                CantidadPasajeros = @CantidadPasajeros,
                Estado = @Estado
            WHERE IdReserva = @IdReserva;

            -- Actualizar el número de pasajeros en el vuelo
            UPDATE VUELO
            SET NumeroPasajeros = @NumeroPasajerosActual - @CantidadPasajeros
            WHERE IdVuelo = @IdVuelo;

            -- Verificar si el número de pasajeros es 0 y actualizar el estado del vuelo
            IF (SELECT NumeroPasajeros FROM VUELO WHERE IdVuelo = @IdVuelo) = 0
            BEGIN
                UPDATE VUELO
                SET Estado = 0
                WHERE IdVuelo = @IdVuelo;
            END

            -- Establecer resultado a 1 (éxito)
            SET @Resultado = 1;
        END
        ELSE
        BEGIN
            -- En caso de que ya exista otra reserva con el mismo vuelo y persona
            SET @Resultado = 0;
        END
    END TRY
    BEGIN CATCH
        -- En caso de error, establecer resultado a 0 (error)
        SET @Resultado = 0;
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarReunion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR REUNION
CREATE PROCEDURE [dbo].[sp_ModificarReunion](
    @IdReunion INT,
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @Fecha DATE,
    @HoraInicio TIME,
    @HoraFin TIME,
    @Lugar VARCHAR(255),
    @Descripcion VARCHAR(MAX),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF EXISTS (SELECT * FROM REUNION WHERE IdReunion = @IdReunion)
    BEGIN
        UPDATE REUNION
        SET IdPersona = @IdPersona,
            NombreEmpleado = @NombreEmpleado,
            Fecha = @Fecha,
            HoraInicio = @HoraInicio,
            HoraFin = @HoraFin,
            Lugar = @Lugar,
            Descripcion = @Descripcion
        WHERE IdReunion = @IdReunion;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarTipoAsiento]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR TIPO_ASIENTO
CREATE PROCEDURE [dbo].[sp_ModificarTipoAsiento](
    @IdTipoAsiento INT,
    @Descripcion VARCHAR(50),
    @PrecioExtra DECIMAL(10, 2),
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM TIPO_ASIENTO WHERE Descripcion = @Descripcion AND IdTipoAsiento != @IdTipoAsiento)
    BEGIN
        UPDATE TIPO_ASIENTO SET 
            Descripcion = @Descripcion,
            PrecioExtra = @PrecioExtra,
            Estado = @Estado
        WHERE IdTipoAsiento = @IdTipoAsiento;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarTurno]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA MODIFICAR TURNO
CREATE PROCEDURE [dbo].[sp_ModificarTurno](
    @IdTurno INT,
    @IdPersona INT,
    @NombreEmpleado NVARCHAR(100),
    @Dias VARCHAR(100),
    @HoraInicio TIME,
    @HoraFin TIME,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF EXISTS (SELECT * FROM TURNO WHERE IdTurno = @IdTurno)
    BEGIN
        UPDATE TURNO
        SET IdPersona = @IdPersona,
            NombreEmpleado = @NombreEmpleado,
            Dias = @Dias,
            HoraInicio = @HoraInicio,
            HoraFin = @HoraFin
        WHERE IdTurno = @IdTurno;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarVuelo]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ModificarVuelo](
    @IdVuelo INT,
    @NumeroVuelo VARCHAR(10),
    @IdAerolínea INT = NULL,
    @Origen VARCHAR(50),
    @Destino VARCHAR(50),
    @FechaSalida DATETIME,
    @FechaLlegada DATETIME,
    @Precio DECIMAL(10, 2),
    @Estado BIT,
    @NumeroPasajeros INT,
    @Resultado BIT OUTPUT
)
AS
BEGIN
    SET @Resultado = 1

    BEGIN TRY
        UPDATE VUELO
        SET 
            NumeroVuelo = @NumeroVuelo,
            IdAerolínea = @IdAerolínea,
            Origen = @Origen,
            Destino = @Destino,
            FechaSalida = @FechaSalida,
            FechaLlegada = @FechaLlegada,
            Precio = @Precio,
            Estado = @Estado,
            NumeroPasajeros = @NumeroPasajeros
        WHERE IdVuelo = @IdVuelo
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarAerolinea]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA GUARDAR AEROLÍNEA
CREATE PROCEDURE [dbo].[sp_RegistrarAerolinea](
    @Nombre VARCHAR(50),
    @Codigo VARCHAR(10),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM AEROLINEA WHERE Nombre = @Nombre OR Codigo = @Codigo)
    BEGIN
        INSERT INTO AEROLINEA(Nombre, Codigo) VALUES (
            @Nombre, @Codigo
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarAeropuerto]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCEDIMIENTO PARA GUARDAR AEROPUERTO
CREATE PROCEDURE [dbo].[sp_RegistrarAeropuerto](
    @Nombre VARCHAR(50),
    @Ciudad VARCHAR(50),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM AEROPUERTO WHERE Nombre = @Nombre)
    BEGIN
        INSERT INTO AEROPUERTO(Nombre, Ciudad) VALUES (
            @Nombre, @Ciudad
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarCategoria]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA GUARDAR CATEGORIA
CREATE PROC [dbo].[sp_RegistrarCategoria](
@Descripcion varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)

		insert into CATEGORIA(Descripcion) values (
		@Descripcion
		)
	ELSE
		SET @Resultado = 0
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarDiaLibre]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR DIA LIBRE
CREATE PROCEDURE [dbo].[sp_RegistrarDiaLibre](
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @Fecha DATE,
    @TipoDiaLibre VARCHAR(50),
    @Motivo VARCHAR(MAX),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM DIAS_LIBRES WHERE IdPersona = @IdPersona AND Fecha = @Fecha AND TipoDiaLibre = @TipoDiaLibre)
    BEGIN
        INSERT INTO DIAS_LIBRES(IdPersona, NombreEmpleado, Fecha, TipoDiaLibre, Motivo) VALUES (
            @IdPersona, @NombreEmpleado, @Fecha, @TipoDiaLibre, @Motivo
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarelEquipaje]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA GUARDAR EQUIPAJE
CREATE PROCEDURE [dbo].[sp_RegistrarelEquipaje](
    @Peso DECIMAL(5, 2),
    @Precio DECIMAL(10, 2),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM EQUIPAJE WHERE Peso = @Peso AND Precio = @Precio)
    BEGIN
        INSERT INTO EQUIPAJE(Peso, Precio) VALUES (
            @Peso, @Precio
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarelTipoAsiento]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCEDIMIENTO PARA GUARDAR TIPO_ASIENTO
CREATE PROCEDURE [dbo].[sp_RegistrarelTipoAsiento](
    @Descripcion VARCHAR(50),
    @PrecioExtra DECIMAL(10, 2),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM TIPO_ASIENTO WHERE Descripcion = @Descripcion AND PrecioExtra = @PrecioExtra)
    BEGIN
        INSERT INTO TIPO_ASIENTO(Descripcion, PrecioExtra) VALUES (
            @Descripcion, @PrecioExtra);
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarEvaluacion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR EVALUACION
Create PROCEDURE [dbo].[sp_RegistrarEvaluacion](
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @Fecha DATE,
    @Objetivos VARCHAR(Max),
    @Comentarios VARCHAR(Max),
    @Puntaje INT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM EVALUACION WHERE IdPersona = @IdPersona AND Fecha = @Fecha AND Objetivos = @Objetivos AND Puntaje = @Puntaje)
    BEGIN
        INSERT INTO EVALUACION(IdPersona, NombreEmpleado, Fecha, Objetivos, Comentarios, Puntaje) VALUES (
            @IdPersona, @NombreEmpleado, @Fecha, @Objetivos, @Comentarios, @Puntaje
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarHabitacion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--PROCEDIMIENTO PARA GUARDAR HABITACION
create PROC [dbo].[sp_RegistrarHabitacion](
@Numero varchar(50),
@Detalle varchar(100),
@Precio decimal(10,2),
@IdPiso varchar(50),
@IdCategoria varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM HABITACION WHERE Numero = @Numero)

		insert into HABITACION(Numero,Detalle,Precio,IdPiso,IdCategoria,IdEstadoHabitacion) values (
		@Numero,@Detalle,@Precio,@IdPiso,@IdCategoria,1
		)
	ELSE
		SET @Resultado = 0
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarNomina]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR NOMINA
Create PROCEDURE [dbo].[sp_RegistrarNomina]      
(
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
	@NombreRol VARCHAR(100),
    @Fecha DATE,
    @Salario DECIMAL(10, 2),
    @Deducciones DECIMAL(10, 2),
    @Bonificaciones DECIMAL(10, 2),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM NOMINA WHERE IdPersona = @IdPersona AND Fecha = @Fecha)
    BEGIN
        INSERT INTO NOMINA(IdPersona, NombreEmpleado,NombreRol, Fecha, Salario, Deducciones, Bonificaciones) VALUES (
            @IdPersona, @NombreEmpleado,@NombreRol, @Fecha, @Salario, @Deducciones, @Bonificaciones
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPermiso]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR PERMISO
CREATE PROCEDURE [dbo].[sp_RegistrarPermiso](
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @PermisoDescripcion VARCHAR(255),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM PERMISO WHERE IdPersona = @IdPersona AND PermisoDescripcion = @PermisoDescripcion)
    BEGIN
        INSERT INTO PERMISO(IdPersona, NombreEmpleado, PermisoDescripcion) VALUES (
            @IdPersona, @NombreEmpleado, @PermisoDescripcion
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPersona]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--PROCEDIMIENTO PARA GUARDAR PERSONA
create PROC [dbo].[sp_RegistrarPersona](
@TipoDocumento varchar(50),
@Documento varchar(50),
@Nombre varchar(50),
@Apellido varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdTipoPersona int,
@Resultado bit output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM persona WHERE Documento = @Documento)
	begin
		insert into persona(TipoDocumento, Documento,Nombre,Apellido,Correo,Clave,IdTipoPersona) values (
		@TipoDocumento,@Documento,@Nombre,@Apellido,@Correo,@Clave,@IdTipoPersona)
	end
	ELSE
		SET @Resultado = 0
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPiso]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--PROCEDIMIENTO PARA GUARDAR PISOS
CREATE PROC [dbo].[sp_RegistrarPiso](
@Descripcion varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PISO WHERE Descripcion = @Descripcion)

		insert into PISO(Descripcion) values (
		@Descripcion
		)
	ELSE
		SET @Resultado = 0
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarProducto]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--PROCEDIMIENTO PARA GUARDAR PRODUCTO
CREATE PROC [dbo].[sp_RegistrarProducto](
@Nombre varchar(50),
@Detalle varchar(50),
@Precio varchar(50),
@Cantidad varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre)

		insert into PRODUCTO(Nombre,Detalle,Precio,Cantidad) values (
		@Nombre,@Detalle,@Precio,@Cantidad
		)
	ELSE
		SET @Resultado = 0
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_registrarRecepcion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_registrarRecepcion](
@IdCliente int,
@TipoDocumento varchar(50),
@Documento varchar(50),
@Nombre varchar(50),
@Apellido varchar(50),
@Correo varchar(50),
@IdHabitacion int,
@FechaSalida datetime,
@PrecioInicial decimal(10,2),
@Adelanto decimal(10,2),
@PrecioRestante decimal(10,2),
@Observacion varchar(500),
@Resultado bit output
)
as
begin
	set @Resultado = 1
	begin try
		begin transaction
		if(not exists(select * from persona where IdPersona =@IdCliente ))
		begin
			insert into PERSONA(TipoDocumento,Documento,Nombre,Apellido,Correo,IdTipoPersona) values(
			@TipoDocumento,@Documento,@Nombre,@Apellido,@Correo,3)
			set @IdCliente = SCOPE_IDENTITY()  
		end

		update HABITACION set IdEstadoHabitacion = 2 where IdHabitacion = @IdHabitacion

		insert into RECEPCION(IdCliente,IdHabitacion,FechaSalida,PrecioInicial,Adelanto,PrecioRestante,Observacion,Estado) values
		(@IdCliente,@IdHabitacion,@FechaSalida,@PrecioInicial,@Adelanto,@PrecioRestante,@Observacion,1)

		commit transaction
	end try
	begin catch
		rollback
		set @Resultado = 0
	end catch

end




GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarReservadelVuelo]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA GUARDAR RESERVA_VUELO
Create PROCEDURE [dbo].[sp_RegistrarReservadelVuelo](
    @IdVuelo INT,
    @IdPersona INT,
    @IdEquipaje INT,
    @IdTipoAsiento INT,
    @FechaReserva DATETIME,
    @PrecioVuelo DECIMAL(10, 2),
    @PrecioAsiento DECIMAL(10, 2),
    @PrecioEquipaje DECIMAL(10, 2),
    @PrecioTotal DECIMAL(10, 2),
    @Estado BIT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (
        SELECT * 
        FROM RESERVA_VUELO 
        WHERE IdVuelo = @IdVuelo 
        AND IdPersona = @IdPersona 
        AND IdEquipaje = @IdEquipaje 
        AND IdTipoAsiento = @IdTipoAsiento
        AND FechaReserva = @FechaReserva
    )
    BEGIN
        INSERT INTO RESERVA_VUELO(
            IdVuelo,
            IdPersona,
            IdEquipaje,
            IdTipoAsiento,
            FechaReserva,
            PrecioVuelo,
            PrecioAsiento,
            PrecioEquipaje,
            PrecioTotal,
            Estado
        ) VALUES (
            @IdVuelo,
            @IdPersona,
            @IdEquipaje,
            @IdTipoAsiento,
            @FechaReserva,
            @PrecioVuelo,
            @PrecioAsiento,
            @PrecioEquipaje,
            @PrecioTotal,
            @Estado
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarReservaVuelo]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA GUARDAR RESERVA_VUELO
Create PROCEDURE [dbo].[sp_RegistrarReservaVuelo](
    @IdVuelo INT,
    @IdPersona INT,
    @IdEquipaje INT = NULL,
    @IdTipoAsiento INT = NULL,
    @PrecioVuelo DECIMAL(10, 2),
    @PrecioAsiento DECIMAL(10, 2) = NULL,
    @PrecioEquipaje DECIMAL(10, 2) = NULL,
    @PrecioTotal DECIMAL(10, 2),
    @CantidadPasajeros INT, -- Nuevo parámetro
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 1;

    IF NOT EXISTS (SELECT * FROM RESERVA_VUELO WHERE IdVuelo = @IdVuelo AND IdPersona = @IdPersona)
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION;
            
            -- Insertar la nueva reserva de vuelo
            INSERT INTO RESERVA_VUELO(
                IdVuelo, IdPersona, IdEquipaje, IdTipoAsiento, PrecioVuelo, PrecioAsiento, PrecioEquipaje, PrecioTotal, 
                CantidadPasajeros
            ) VALUES (
                @IdVuelo, @IdPersona, @IdEquipaje, @IdTipoAsiento, @PrecioVuelo, @PrecioAsiento, @PrecioEquipaje, @PrecioTotal, 
                @CantidadPasajeros
            );

            -- Actualizar el número de pasajeros en la tabla VUELO
            UPDATE VUELO
            SET NumeroPasajeros = NumeroPasajeros - @CantidadPasajeros
            WHERE IdVuelo = @IdVuelo;

            -- Verificar si el número de pasajeros llega a cero y actualizar el estado del vuelo
            IF (SELECT NumeroPasajeros FROM VUELO WHERE IdVuelo = @IdVuelo) = 0
            BEGIN
                UPDATE VUELO
                SET Estado = 0
                WHERE IdVuelo = @IdVuelo;
            END

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            -- En caso de error, deshacer la transacción y establecer @Resultado en 0
            ROLLBACK TRANSACTION;
            SET @Resultado = 0;
        END CATCH
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarReunion]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR REUNION
CREATE PROCEDURE [dbo].[sp_RegistrarReunion](
    @IdPersona INT,
    @NombreEmpleado VARCHAR(100),
    @Fecha DATE,
    @HoraInicio TIME,
    @HoraFin TIME,
    @Lugar VARCHAR(255),
    @Descripcion VARCHAR(MAX),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM REUNION WHERE IdPersona = @IdPersona AND Fecha = @Fecha AND HoraInicio = @HoraInicio AND HoraFin = @HoraFin)
    BEGIN
        INSERT INTO REUNION(IdPersona, NombreEmpleado, Fecha, HoraInicio, HoraFin, Lugar, Descripcion) VALUES (
            @IdPersona, @NombreEmpleado, @Fecha, @HoraInicio, @HoraFin, @Lugar, @Descripcion
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarTipoAsiento]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[sp_RegistrarTipoAsiento](
    @Descripcion VARCHAR(50),
    @PrecioExtra DECIMAL(10, 2),
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    
    IF NOT EXISTS (SELECT 1 FROM TIPOASIENTO WHERE Descripcion = @Descripcion AND PrecioExtra = @PrecioExtra)
    BEGIN
        INSERT INTO TIPOASIENTO (Descripcion, PrecioExtra) VALUES (@Descripcion, @PrecioExtra);
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarTurno]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR TURNO
CREATE PROCEDURE [dbo].[sp_RegistrarTurno](
    @IdPersona INT,
    @NombreEmpleado NVARCHAR(100),
    @Dias VARCHAR(100),
    @HoraInicio TIME,
    @HoraFin TIME,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;
    IF NOT EXISTS (SELECT * FROM TURNO WHERE IdPersona = @IdPersona AND Dias = @Dias AND HoraInicio = @HoraInicio AND HoraFin = @HoraFin)
    BEGIN
        INSERT INTO TURNO(IdPersona, NombreEmpleado, Dias, HoraInicio, HoraFin) VALUES (
            @IdPersona, @NombreEmpleado, @Dias, @HoraInicio, @HoraFin
        );
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarVuelo]    Script Date: 17/8/2024 23:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA REGISTRAR VUELO
CREATE PROC [dbo].[sp_RegistrarVuelo](
    @NumeroVuelo VARCHAR(10),
    @IdAerolínea INT,
    @Origen VARCHAR(50),
    @Destino VARCHAR(50),
    @FechaSalida DATETIME,
    @FechaLlegada DATETIME,
    @Precio DECIMAL(10, 2),
    @NumeroPasajeros INT,
    @Resultado BIT OUTPUT
) AS
BEGIN
    SET @Resultado = 1;

    IF NOT EXISTS (SELECT * FROM VUELO WHERE NumeroVuelo = @NumeroVuelo)
    BEGIN
        INSERT INTO VUELO(NumeroVuelo, IdAerolínea, Origen, Destino, FechaSalida, FechaLlegada, Precio, NumeroPasajeros)
        VALUES (@NumeroVuelo, @IdAerolínea, @Origen, @Destino, @FechaSalida, @FechaLlegada, @Precio, @NumeroPasajeros);
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
    END
END
GO
USE [master]
GO
ALTER DATABASE [DB_RVuelo] SET  READ_WRITE 
GO
