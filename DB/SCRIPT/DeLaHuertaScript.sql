USE [master]
GO
/****** Object:  Database [DeLaHuertaDB]    Script Date: 19/8/2021 17:17:34 ******/
CREATE DATABASE [DeLaHuertaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DeLaHuertaDB', FILENAME = N'D:\SQL2019INSTALADOR\SQL2019\1\MSSQL15.MSSQLSERVER\MSSQL\DATA\DeLaHuertaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DeLaHuertaDB_log', FILENAME = N'D:\SQL2019INSTALADOR\SQL2019\1\MSSQL15.MSSQLSERVER\MSSQL\DATA\DeLaHuertaDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DeLaHuertaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DeLaHuertaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DeLaHuertaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DeLaHuertaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DeLaHuertaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DeLaHuertaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DeLaHuertaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DeLaHuertaDB] SET  MULTI_USER 
GO
ALTER DATABASE [DeLaHuertaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DeLaHuertaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DeLaHuertaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DeLaHuertaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DeLaHuertaDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DeLaHuertaDB', N'ON'
GO
ALTER DATABASE [DeLaHuertaDB] SET QUERY_STORE = OFF
GO
USE [DeLaHuertaDB]
GO
/****** Object:  Table [dbo].[Agente]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agente](
	[idAgente] [int] NOT NULL,
	[idProveedor] [int] NOT NULL,
	[nombre] [nvarchar](20) NOT NULL,
	[telefono] [nvarchar](8) NOT NULL,
	[email] [nvarchar](30) NOT NULL,
	[activo] [int] NOT NULL,
 CONSTRAINT [PK_Agente] PRIMARY KEY CLUSTERED 
(
	[idAgente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Botanica]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Botanica](
	[idBotanica] [int] NOT NULL,
	[descripcion] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Botanica] PRIMARY KEY CLUSTERED 
(
	[idBotanica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetMovimiento]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetMovimiento](
	[idMovimiento] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[idProveedor] [int] NOT NULL,
	[idSucursal] [int] NOT NULL,
	[idUsuario] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[observacion] [nvarchar](max) NULL,
 CONSTRAINT [PK_DetMovimiento_1] PRIMARY KEY CLUSTERED 
(
	[idMovimiento] ASC,
	[idProducto] ASC,
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimiento]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimiento](
	[idMovimiento] [int] IDENTITY(1,1) NOT NULL,
	[idTipoMovimiento] [int] NOT NULL,
	[idConcepto] [int] NOT NULL,
	[fecha] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Movimiento_1] PRIMARY KEY CLUSTERED 
(
	[idMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [int] NOT NULL,
	[idBotanica] [int] NOT NULL,
	[nombre] [nvarchar](20) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[costoUnitario] [int] NOT NULL,
	[cantidadMinima] [int] NOT NULL,
	[cantidadTotal] [int] NOT NULL,
	[activo] [int] NOT NULL,
 CONSTRAINT [PK_Producto_1] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto_proveedor]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto_proveedor](
	[idProducto] [int] NOT NULL,
	[idProveedor] [int] NOT NULL,
	[activo] [int] NULL,
 CONSTRAINT [PK_Inventario] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[idProveedor] [int] NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[direccion] [nvarchar](max) NOT NULL,
	[pais] [nvarchar](50) NOT NULL,
	[activo] [int] NOT NULL,
 CONSTRAINT [PK_Proveedor_1] PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroMovimiento]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroMovimiento](
	[idRegistro] [int] IDENTITY(1,1) NOT NULL,
	[idMovimiento] [int] NOT NULL,
 CONSTRAINT [PK_RegistroMovimiento] PRIMARY KEY CLUSTERED 
(
	[idRegistro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[idRol] [int] NOT NULL,
	[descripcion] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[idSucursal] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](20) NOT NULL,
	[ubicacion] [nvarchar](30) NOT NULL,
	[activa] [int] NOT NULL,
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[idSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoMovimiento]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoMovimiento](
	[idTipoMovimiento] [int] NOT NULL,
	[idConcepto] [int] NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TipoMovimiento_1] PRIMARY KEY CLUSTERED 
(
	[idTipoMovimiento] ASC,
	[idConcepto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ubicacion]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ubicacion](
	[idProducto] [int] NOT NULL,
	[idSucursal] [int] NOT NULL,
	[activo] [int] NULL,
 CONSTRAINT [PK_Ubicacion] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[idSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 19/8/2021 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] NOT NULL,
	[idRol] [int] NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
	[clave] [nvarchar](max) NOT NULL,
	[habilitado] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Agente] ([idAgente], [idProveedor], [nombre], [telefono], [email], [activo]) VALUES (1, 1, N'Valeria', N'87441566', N'val@gmail.com', 0)
INSERT [dbo].[Agente] ([idAgente], [idProveedor], [nombre], [telefono], [email], [activo]) VALUES (3, 90999099, N'Andrea', N'89778885', N'c@gmail.com', 1)
INSERT [dbo].[Agente] ([idAgente], [idProveedor], [nombre], [telefono], [email], [activo]) VALUES (12458692, 256689756, N'Francis Leordina', N'63246987', N'franledina@hotmail.com', 1)
INSERT [dbo].[Agente] ([idAgente], [idProveedor], [nombre], [telefono], [email], [activo]) VALUES (247785963, 849305736, N'Daniela Venegas', N'78855669', N'daniv@gmail.com', 1)
INSERT [dbo].[Agente] ([idAgente], [idProveedor], [nombre], [telefono], [email], [activo]) VALUES (257496339, 90999099, N'Roberto Deluque', N'87459666', N'deluquerobert@hotmail.es', 1)
INSERT [dbo].[Agente] ([idAgente], [idProveedor], [nombre], [telefono], [email], [activo]) VALUES (978245230, 445655689, N'Lourdes Trabal', N'24487455', N'ltrabal@yahoo.com', 1)
GO
INSERT [dbo].[Botanica] ([idBotanica], [descripcion]) VALUES (1, N'Fruta')
INSERT [dbo].[Botanica] ([idBotanica], [descripcion]) VALUES (2, N'Verdura')
GO
INSERT [dbo].[DetMovimiento] ([idMovimiento], [idProducto], [idProveedor], [idSucursal], [idUsuario], [cantidad], [observacion]) VALUES (4, 12458692, 256689756, 1, 207870114, 5, N'Compra de 5 manzanas para la sucursal principal')
INSERT [dbo].[DetMovimiento] ([idMovimiento], [idProducto], [idProveedor], [idSucursal], [idUsuario], [cantidad], [observacion]) VALUES (4, 509987145, 256689756, 1, 1, 7, N'Compra de 7 manzanas de agua para la sucursal de Cartago')
INSERT [dbo].[DetMovimiento] ([idMovimiento], [idProducto], [idProveedor], [idSucursal], [idUsuario], [cantidad], [observacion]) VALUES (5, 257496339, 445655689, 2, 207870114, 10, N'Se tienen 10 peras como inventariado inicial')
INSERT [dbo].[DetMovimiento] ([idMovimiento], [idProducto], [idProveedor], [idSucursal], [idUsuario], [cantidad], [observacion]) VALUES (6, 247785963, 445655689, 3, 1, 23, N'Se facturan 23 zanahorias')
INSERT [dbo].[DetMovimiento] ([idMovimiento], [idProducto], [idProveedor], [idSucursal], [idUsuario], [cantidad], [observacion]) VALUES (7, 745605236, 849305736, 3, 207870114, 3, N'Se devuelven 3 tomates al proveedor')
GO
SET IDENTITY_INSERT [dbo].[Movimiento] ON 

INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (4, 1, 1, N'22/JUN/2021')
INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (5, 1, 5, N'22/JUN/2021')
INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (6, 2, 1, N'21/JUN/2021')
INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (7, 2, 3, N'24/JUN/2021')
INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (8, 1, 1, N'08/19/2021 16:21:49')
INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (9, 1, 1, N'08/19/2021 16:36:44')
INSERT [dbo].[Movimiento] ([idMovimiento], [idTipoMovimiento], [idConcepto], [fecha]) VALUES (10, 1, 1, N'08/19/2021 16:37:52')
SET IDENTITY_INSERT [dbo].[Movimiento] OFF
GO
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (1, 1, N'Uva', N'Ideal para desestresar y descansar el cuerpo', 75, 7, 60, 0)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (2, 1, N'asdsdad', N'adsaads', 2, 2, 60, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (12458692, 1, N'Manzana', N'La manzana es el fruto ideal para tomar a cualquier hora y participa positivamente en la consecución del equilibrio alimentario.', 100, 10, 60, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (247785963, 2, N'Zanahoria', N'El contenido de vitaminas y minerales de la zanahoria hacen que sea una verdura con varios beneficios para tu salud.', 75, 10, 15, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (257496339, 1, N'Pera', N'Los componentes antioxidantes y flavonoides de las peras también pueden inducir efectos antiinflamatorios en el cuerpo y reducir el dolor asociado a enfermedades de inflamación.', 125, 10, 50, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (478569325, 1, N'Papaya', N'El saber popular cataloga a la papaya como un potente antiparasitario intestinal.', 800, 10, 70, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (509987145, 1, N'Manzana de agua', N'Deliciosa y beneficiosa ayuda frente a la diabetes, digestión entre mucho más.', 75, 10, 70, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (745605236, 2, N'Tomate', N'El tomate es rico en vitaminas y minerales, además de proteger la vista.', 100, 10, 35, 1)
INSERT [dbo].[Producto] ([idProducto], [idBotanica], [nombre], [descripcion], [costoUnitario], [cantidadMinima], [cantidadTotal], [activo]) VALUES (978245230, 1, N'Melon', N'Ideal para perder peso son bajos en calorías, grasa y colesterol.', 650, 10, 90, 1)
GO
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (1, 90999099, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (12458692, 256689756, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (247785963, 445655689, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (257496339, 445655689, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (478569325, 90999099, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (509987145, 256689756, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (745605236, 849305736, NULL)
INSERT [dbo].[Producto_proveedor] ([idProducto], [idProveedor], [activo]) VALUES (978245230, 849305736, NULL)
GO
INSERT [dbo].[Proveedor] ([idProveedor], [nombre], [direccion], [pais], [activo]) VALUES (1, N'sin asignar', N'.', N'.', 0)
INSERT [dbo].[Proveedor] ([idProveedor], [nombre], [direccion], [pais], [activo]) VALUES (2, N'asdasdasd', N'asdasddas', N'Costa Rica', 1)
INSERT [dbo].[Proveedor] ([idProveedor], [nombre], [direccion], [pais], [activo]) VALUES (90999099, N'Perico Frutas&Verduras', N'La Uruca, 600 mts Norte de la Kia', N'Costa Rica', 1)
INSERT [dbo].[Proveedor] ([idProveedor], [nombre], [direccion], [pais], [activo]) VALUES (256689756, N'Manzanas La Trinidad', N'Diagonal al supermercado la carreta San Pablo de Leon Cortés, San José, Provincia de San José, Costa Rica', N'Costa Rica', 1)
INSERT [dbo].[Proveedor] ([idProveedor], [nombre], [direccion], [pais], [activo]) VALUES (445655689, N'Estilorganico', N'Calle Jesús Ocaña Rojas (12,83 km) 20101 Canoas, Alajuela Province, Costa Rica', N'Costa Rica', 1)
INSERT [dbo].[Proveedor] ([idProveedor], [nombre], [direccion], [pais], [activo]) VALUES (849305736, N'Verduleria LaBaratita', N'Avenida Central 100 oeste del bar el bulevar', N'Costa Rica', 1)
GO
SET IDENTITY_INSERT [dbo].[RegistroMovimiento] ON 

INSERT [dbo].[RegistroMovimiento] ([idRegistro], [idMovimiento]) VALUES (4, 4)
INSERT [dbo].[RegistroMovimiento] ([idRegistro], [idMovimiento]) VALUES (5, 5)
INSERT [dbo].[RegistroMovimiento] ([idRegistro], [idMovimiento]) VALUES (6, 6)
INSERT [dbo].[RegistroMovimiento] ([idRegistro], [idMovimiento]) VALUES (7, 7)
SET IDENTITY_INSERT [dbo].[RegistroMovimiento] OFF
GO
INSERT [dbo].[Rol] ([idRol], [descripcion]) VALUES (1, N'Administrador')
INSERT [dbo].[Rol] ([idRol], [descripcion]) VALUES (2, N'Bodeguero')
INSERT [dbo].[Rol] ([idRol], [descripcion]) VALUES (3, N'Reportes')
INSERT [dbo].[Rol] ([idRol], [descripcion]) VALUES (4, N'Sin asignar')
GO
SET IDENTITY_INSERT [dbo].[Sucursal] ON 

INSERT [dbo].[Sucursal] ([idSucursal], [descripcion], [ubicacion], [activa]) VALUES (1, N'DeLaHuerta', N'Poas, Alajuela', 1)
INSERT [dbo].[Sucursal] ([idSucursal], [descripcion], [ubicacion], [activa]) VALUES (2, N'DeLaHuertaCartago', N'Paraiso, Cartago', 0)
INSERT [dbo].[Sucursal] ([idSucursal], [descripcion], [ubicacion], [activa]) VALUES (3, N'DeLaHuertaSanJose', N'Desamparados, San Jose', 1)
SET IDENTITY_INSERT [dbo].[Sucursal] OFF
GO
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (1, 1, N'Compra')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (1, 2, N'Devolucion')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (1, 3, N'Ajuste a inventario')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (1, 4, N'Traspaso')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (1, 5, N'Inventario inicial')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (2, 1, N'Factura')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (2, 2, N'Cancelacion de compra')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (2, 3, N'Devolucion de compra a proveedor')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (2, 4, N'Ajuste de inventario')
INSERT [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto], [descripcion]) VALUES (2, 5, N'Traspaso')
GO
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (1, 2, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (12458692, 1, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (12458692, 2, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (247785963, 3, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (257496339, 2, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (478569325, 1, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (509987145, 1, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (745605236, 3, NULL)
INSERT [dbo].[Ubicacion] ([idProducto], [idSucursal], [activo]) VALUES (978245230, 2, NULL)
GO
INSERT [dbo].[Usuario] ([idUsuario], [idRol], [nombre], [clave], [habilitado]) VALUES (1, 2, N'Juan', N'u6QXcpElu3jBzi22FNFdrw==', 1)
INSERT [dbo].[Usuario] ([idUsuario], [idRol], [nombre], [clave], [habilitado]) VALUES (2, 3, N'Pedro', N'u6QXcpElu3jBzi22FNFdrw==', 1)
INSERT [dbo].[Usuario] ([idUsuario], [idRol], [nombre], [clave], [habilitado]) VALUES (3, 2, N'Gustavo', N'vSTN9jp+ES+Jim405hh55w==', 1)
INSERT [dbo].[Usuario] ([idUsuario], [idRol], [nombre], [clave], [habilitado]) VALUES (207870114, 1, N'Richard', N'u6QXcpElu3jBzi22FNFdrw==', 1)
GO
ALTER TABLE [dbo].[Agente]  WITH CHECK ADD  CONSTRAINT [FK_Agente_Proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[Agente] CHECK CONSTRAINT [FK_Agente_Proveedor]
GO
ALTER TABLE [dbo].[DetMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_DetMovimiento_Movimiento1] FOREIGN KEY([idMovimiento])
REFERENCES [dbo].[Movimiento] ([idMovimiento])
GO
ALTER TABLE [dbo].[DetMovimiento] CHECK CONSTRAINT [FK_DetMovimiento_Movimiento1]
GO
ALTER TABLE [dbo].[DetMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_DetMovimiento_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[DetMovimiento] CHECK CONSTRAINT [FK_DetMovimiento_Producto]
GO
ALTER TABLE [dbo].[DetMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_DetMovimiento_Proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[DetMovimiento] CHECK CONSTRAINT [FK_DetMovimiento_Proveedor]
GO
ALTER TABLE [dbo].[DetMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_DetMovimiento_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([idSucursal])
GO
ALTER TABLE [dbo].[DetMovimiento] CHECK CONSTRAINT [FK_DetMovimiento_Sucursal]
GO
ALTER TABLE [dbo].[DetMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_DetMovimiento_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[DetMovimiento] CHECK CONSTRAINT [FK_DetMovimiento_Usuario]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_TipoMovimiento] FOREIGN KEY([idTipoMovimiento], [idConcepto])
REFERENCES [dbo].[TipoMovimiento] ([idTipoMovimiento], [idConcepto])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_TipoMovimiento]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Botanica] FOREIGN KEY([idBotanica])
REFERENCES [dbo].[Botanica] ([idBotanica])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Botanica]
GO
ALTER TABLE [dbo].[Producto_proveedor]  WITH CHECK ADD  CONSTRAINT [FK_Inventario_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[Producto_proveedor] CHECK CONSTRAINT [FK_Inventario_Producto]
GO
ALTER TABLE [dbo].[Producto_proveedor]  WITH CHECK ADD  CONSTRAINT [FK_Inventario_Proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[Producto_proveedor] CHECK CONSTRAINT [FK_Inventario_Proveedor]
GO
ALTER TABLE [dbo].[RegistroMovimiento]  WITH CHECK ADD  CONSTRAINT [FK_RegistroMovimiento_Movimiento] FOREIGN KEY([idMovimiento])
REFERENCES [dbo].[Movimiento] ([idMovimiento])
GO
ALTER TABLE [dbo].[RegistroMovimiento] CHECK CONSTRAINT [FK_RegistroMovimiento_Movimiento]
GO
ALTER TABLE [dbo].[Ubicacion]  WITH CHECK ADD  CONSTRAINT [FK_Ubicacion_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[Ubicacion] CHECK CONSTRAINT [FK_Ubicacion_Producto]
GO
ALTER TABLE [dbo].[Ubicacion]  WITH CHECK ADD  CONSTRAINT [FK_Ubicacion_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([idSucursal])
GO
ALTER TABLE [dbo].[Ubicacion] CHECK CONSTRAINT [FK_Ubicacion_Sucursal]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
USE [master]
GO
ALTER DATABASE [DeLaHuertaDB] SET  READ_WRITE 
GO
