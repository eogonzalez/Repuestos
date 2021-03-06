USE [master]
GO
/****** Object:  Database [Repuestos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE DATABASE [Repuestos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Repuestos', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Repuestos.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Repuestos_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Repuestos_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Repuestos] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Repuestos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Repuestos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Repuestos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Repuestos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Repuestos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Repuestos] SET ARITHABORT OFF 
GO
ALTER DATABASE [Repuestos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Repuestos] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Repuestos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Repuestos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Repuestos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Repuestos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Repuestos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Repuestos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Repuestos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Repuestos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Repuestos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Repuestos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Repuestos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Repuestos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Repuestos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Repuestos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Repuestos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Repuestos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Repuestos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Repuestos] SET  MULTI_USER 
GO
ALTER DATABASE [Repuestos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Repuestos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Repuestos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Repuestos] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Repuestos', N'ON'
GO
USE [Repuestos]
GO
/****** Object:  Table [dbo].[Categoria_Productos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categoria_Productos](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](300) NULL,
 CONSTRAINT [PK_Categoria_Productos] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clientes](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nit] [varchar](50) NULL,
	[cui] [varchar](50) NULL,
	[pasaporte] [varchar](50) NULL,
	[nombres] [varchar](100) NOT NULL,
	[apellidos] [varchar](100) NULL,
	[direccion] [varchar](150) NULL,
	[telefono] [int] NULL,
	[celular] [int] NULL,
	[correo] [varchar](50) NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[compras_detalle]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[compras_detalle](
	[correlativo] [int] IDENTITY(1,1) NOT NULL,
	[id_compra] [int] NOT NULL,
	[numero_compra] [int] NOT NULL,
	[serie] [varchar](10) NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [numeric](28, 8) NOT NULL,
	[subtotal] [numeric](28, 8) NOT NULL,
 CONSTRAINT [PK_compras_detalle] PRIMARY KEY CLUSTERED 
(
	[correlativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[compras_encabezado]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[compras_encabezado](
	[id_compra] [int] IDENTITY(1,1) NOT NULL,
	[numero_compra] [int] NOT NULL,
	[serie] [varchar](10) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[fecha_compra] [date] NOT NULL,
	[fecha_registro] [date] NULL,
	[id_usuario] [int] NULL,
	[total] [numeric](28, 8) NULL,
	[estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_compras_encabezado] PRIMARY KEY CLUSTERED 
(
	[id_compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factura_Detalle]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Factura_Detalle](
	[correlativo] [int] IDENTITY(1,1) NOT NULL,
	[id_factura] [int] NOT NULL,
	[numero_factura] [int] NOT NULL,
	[serie] [varchar](10) NOT NULL,
	[tipo] [varchar](10) NOT NULL,
	[id_producto_servicio] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [numeric](28, 8) NOT NULL,
	[subtotal] [numeric](28, 8) NOT NULL,
 CONSTRAINT [PK_Factura_Detalle] PRIMARY KEY CLUSTERED 
(
	[correlativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factura_Encabezado]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Factura_Encabezado](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[numero_factura] [int] NOT NULL,
	[serie] [varchar](10) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[fecha_factura] [date] NOT NULL,
	[fecha_registro] [date] NULL,
	[id_usuario] [int] NULL,
	[costo_servicio] [numeric](28, 8) NULL,
	[total] [numeric](28, 8) NULL,
	[estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Factura_Encabezado] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_Menu_Opcion]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_Menu_Opcion](
	[id_opcion] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](300) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[url] [varchar](300) NOT NULL,
	[target] [varchar](50) NULL,
	[comando] [varchar](50) NULL,
	[id_padre] [int] NULL,
	[orden] [int] NULL,
	[visible] [bit] NULL,
	[obligatorio] [bit] NULL,
	[login] [bit] NULL,
	[id_usuarioAutoriza] [int] NULL,
	[estado] [char](2) NULL,
 CONSTRAINT [pk_g_menu_opcion_id_opcion] PRIMARY KEY CLUSTERED 
(
	[id_opcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_PermisoTipoUsuario]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_PermisoTipoUsuario](
	[corrPermisoTipoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[id_tipousuario] [int] NOT NULL,
	[id_opcion] [int] NOT NULL,
	[insertar] [bit] NOT NULL,
	[acceder] [bit] NOT NULL,
	[editar] [bit] NOT NULL,
	[borrar] [bit] NOT NULL,
	[aprobar] [bit] NOT NULL,
	[rechazar] [bit] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_modificacion] [datetime] NOT NULL,
	[estado] [char](2) NOT NULL,
	[id_usuarioAutoriza] [int] NOT NULL,
 CONSTRAINT [PK_G_PermisoTipoUsuario] PRIMARY KEY CLUSTERED 
(
	[corrPermisoTipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_TipoUsuario]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_TipoUsuario](
	[id_tipousuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](500) NOT NULL,
	[descripcion] [varchar](500) NULL,
	[tipo_permiso] [varchar](3) NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_modificacion] [datetime] NOT NULL,
	[estado] [nchar](1) NOT NULL,
	[id_usuarioAutoriza] [int] NULL,
 CONSTRAINT [PK_G_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[id_tipousuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_UsuarioPermiso]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_UsuarioPermiso](
	[corrUsuarioPermiso] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_tipousuario] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_modificacion] [datetime] NOT NULL,
	[estado] [char](2) NOT NULL,
	[id_usuarioAutoriza] [int] NOT NULL,
 CONSTRAINT [PK_G_UsuarioPermiso] PRIMARY KEY CLUSTERED 
(
	[corrUsuarioPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UX_G_UsuarioPermiso_id_usuario_id_tipousuario] UNIQUE NONCLUSTERED 
(
	[id_usuario] ASC,
	[id_tipousuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_UsuarioRecupera]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_UsuarioRecupera](
	[corr_usuarioRecupera] [int] IDENTITY(1,1) NOT NULL,
	[correo] [varchar](50) NOT NULL,
	[codigo] [varchar](150) NOT NULL,
	[fecha_solicitud] [date] NOT NULL,
 CONSTRAINT [PK_G_UsuarioRecupera] PRIMARY KEY CLUSTERED 
(
	[corr_usuarioRecupera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_Usuarios]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_Usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](300) NOT NULL,
	[apellidos] [varchar](300) NOT NULL,
	[cui] [varchar](150) NULL,
	[telefono] [varchar](50) NULL,
	[direccion] [varchar](500) NULL,
	[correo] [varchar](150) NOT NULL,
	[password] [varchar](300) NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
	[estado] [nchar](1) NOT NULL,
	[id_usuarioAutoriza] [int] NULL,
 CONSTRAINT [pk_G_Usuarios_id_usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uk_G_Usuarios_correo] UNIQUE NONCLUSTERED 
(
	[correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_Usuarios_Seguridad]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G_Usuarios_Seguridad](
	[corr_usuarios_ingreso] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[fecha_ultimo_acceso] [datetime] NOT NULL,
	[direccion_ip] [varchar](150) NOT NULL,
 CONSTRAINT [PK_G_Usuarios_seguridad] PRIMARY KEY CLUSTERED 
(
	[corr_usuarios_ingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inventario_Configuracion]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario_Configuracion](
	[id_parametro] [int] NOT NULL,
	[porcentaje_ganancia] [numeric](28, 8) NOT NULL,
 CONSTRAINT [PK_Inventario_Configuracion] PRIMARY KEY CLUSTERED 
(
	[id_parametro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Inventarios]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventarios](
	[id_movimiento] [int] IDENTITY(1,1) NOT NULL,
	[tipo_movimiento] [int] NOT NULL,
	[id_compra_servicio] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_costo] [numeric](28, 8) NOT NULL,
	[porcentaje_ganancia] [numeric](28, 8) NOT NULL,
	[precio_venta] [numeric](28, 8) NOT NULL,
	[fecha_movimiento] [date] NOT NULL,
 CONSTRAINT [PK_Inventarios] PRIMARY KEY CLUSTERED 
(
	[id_movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lineas]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lineas](
	[id_linea] [int] IDENTITY(1,1) NOT NULL,
	[id_marca] [int] NOT NULL,
	[id_modelo] [int] NOT NULL,
	[Linea] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Lineas] PRIMARY KEY CLUSTERED 
(
	[id_linea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Marcas](
	[id_marca] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](50) NOT NULL,
	[Descripcion] [varchar](300) NULL,
 CONSTRAINT [PK_Marcas] PRIMARY KEY CLUSTERED 
(
	[id_marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Modelos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modelos](
	[id_modelo] [int] IDENTITY(1,1) NOT NULL,
	[modelo] [int] NOT NULL,
 CONSTRAINT [PK_Modelos] PRIMARY KEY CLUSTERED 
(
	[id_modelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Produtos](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[id_categoria] [int] NOT NULL,
	[id_vehiculo] [int] NULL,
	[nombre] [varchar](50) NULL,
	[marca] [varchar](50) NULL,
	[foto] [image] NULL,
	[descripcion] [varchar](500) NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[proveedores]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[proveedores](
	[id_proveedor] [int] IDENTITY(1,1) NOT NULL,
	[nit] [varchar](50) NULL,
	[nombre_proveedor] [varchar](150) NULL,
	[direccion] [varchar](150) NULL,
	[telefono] [nchar](10) NULL,
	[correo] [varchar](50) NULL,
 CONSTRAINT [PK_proveedores] PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Servicio_Encabezado]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Servicio_Encabezado](
	[id_servicio] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_vehiculo_cliente] [int] NOT NULL,
	[id_tipo_servicio] [int] NOT NULL,
	[fecha_ingreso] [date] NOT NULL,
	[kilometraje_servicio] [int] NULL,
	[costo_servicio] [numeric](28, 8) NULL,
	[costo_total] [numeric](28, 8) NULL,
	[fecha_registro] [date] NOT NULL,
	[estado] [varchar](50) NULL,
 CONSTRAINT [PK_Servicio_Encabezado] PRIMARY KEY CLUSTERED 
(
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Servicio_Externo_Detalle]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Servicio_Externo_Detalle](
	[corr_servicio_externo] [int] IDENTITY(1,1) NOT NULL,
	[id_servicio] [int] NOT NULL,
	[descripcion] [varchar](150) NULL,
	[precio] [numeric](28, 8) NULL,
 CONSTRAINT [PK_Servicio_Externo_Detalle] PRIMARY KEY CLUSTERED 
(
	[corr_servicio_externo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Servicio_Repuesto_Detalle]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio_Repuesto_Detalle](
	[corr_servicio_repuesto] [int] IDENTITY(1,1) NOT NULL,
	[id_servicio] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NULL,
	[precio_venta] [numeric](28, 8) NULL,
	[sub_total] [numeric](28, 8) NULL,
 CONSTRAINT [PK_Servicio_Repuesto_Detalle] PRIMARY KEY CLUSTERED 
(
	[corr_servicio_repuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tipo_Servicio]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tipo_Servicio](
	[id_tipo_servicio] [int] IDENTITY(1,1) NOT NULL,
	[tipo_servicio] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[costo] [numeric](28, 8) NOT NULL,
	[porcentaje_ganancia] [numeric](28, 8) NOT NULL,
 CONSTRAINT [PK_Tipo_Servicio] PRIMARY KEY CLUSTERED 
(
	[id_tipo_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tipo_Vehiculo]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tipo_Vehiculo](
	[id_tipo_vehiculo] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tipo_Vehiculo] PRIMARY KEY CLUSTERED 
(
	[id_tipo_vehiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehiculos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehiculos](
	[id_vehiculo] [int] IDENTITY(1,1) NOT NULL,
	[id_marca] [int] NOT NULL,
	[id_modelo] [int] NOT NULL,
	[id_linea] [int] NOT NULL,
	[id_tipo_vehiculo] [int] NOT NULL,
	[Descripcion] [varchar](500) NULL,
 CONSTRAINT [PK_Vehiculos] PRIMARY KEY CLUSTERED 
(
	[id_vehiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehiculos_Clientes]    Script Date: 4/11/2018 6:41:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehiculos_Clientes](
	[id_vehiculo_cliente] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_vehiculo] [int] NOT NULL,
	[placa] [varchar](50) NULL,
	[color] [varchar](50) NULL,
	[kilometraje] [numeric](28, 8) NULL,
 CONSTRAINT [PK_Vehiculos_Clientes] PRIMARY KEY CLUSTERED 
(
	[id_vehiculo_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Categoria_Productos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Categoria_Productos] ON [dbo].[Categoria_Productos]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_compras_detalle]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_compras_detalle] ON [dbo].[compras_detalle]
(
	[correlativo] ASC,
	[id_producto] ASC,
	[numero_compra] ASC,
	[serie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_compras_encabezado]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_compras_encabezado] ON [dbo].[compras_encabezado]
(
	[id_compra] ASC,
	[numero_compra] ASC,
	[serie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Factura_Encabezado]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Factura_Encabezado] ON [dbo].[Factura_Encabezado]
(
	[id_factura] ASC,
	[numero_factura] ASC,
	[serie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Lineas]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Lineas] ON [dbo].[Lineas]
(
	[id_marca] ASC,
	[id_modelo] ASC,
	[Linea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Marcas]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Marcas] ON [dbo].[Marcas]
(
	[Marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Modelos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Modelos] ON [dbo].[Modelos]
(
	[modelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Produtos]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Produtos] ON [dbo].[Produtos]
(
	[nombre] ASC,
	[marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_proveedores]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_proveedores] ON [dbo].[proveedores]
(
	[nit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Tipo_Servicio]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tipo_Servicio] ON [dbo].[Tipo_Servicio]
(
	[tipo_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Tipo_Vehiculo]    Script Date: 4/11/2018 6:41:00 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tipo_Vehiculo] ON [dbo].[Tipo_Vehiculo]
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[compras_encabezado] ADD  CONSTRAINT [DF_compras_encabezado_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[Factura_Encabezado] ADD  CONSTRAINT [DF_Factura_Encabezado_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[Inventarios] ADD  CONSTRAINT [DF_Inventarios_fecha_movimiento]  DEFAULT (getdate()) FOR [fecha_movimiento]
GO
ALTER TABLE [dbo].[Servicio_Encabezado] ADD  CONSTRAINT [DF_Servicio_Encabezado_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[compras_detalle]  WITH CHECK ADD  CONSTRAINT [FK_compras_detalle_compras_encabezado] FOREIGN KEY([id_compra])
REFERENCES [dbo].[compras_encabezado] ([id_compra])
GO
ALTER TABLE [dbo].[compras_detalle] CHECK CONSTRAINT [FK_compras_detalle_compras_encabezado]
GO
ALTER TABLE [dbo].[compras_detalle]  WITH CHECK ADD  CONSTRAINT [FK_compras_detalle_Produtos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Produtos] ([id_producto])
GO
ALTER TABLE [dbo].[compras_detalle] CHECK CONSTRAINT [FK_compras_detalle_Produtos]
GO
ALTER TABLE [dbo].[compras_encabezado]  WITH CHECK ADD  CONSTRAINT [FK_compras_encabezado_proveedores] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedores] ([id_proveedor])
GO
ALTER TABLE [dbo].[compras_encabezado] CHECK CONSTRAINT [FK_compras_encabezado_proveedores]
GO
ALTER TABLE [dbo].[Factura_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Detalle_Factura_Encabezado] FOREIGN KEY([id_factura])
REFERENCES [dbo].[Factura_Encabezado] ([id_factura])
GO
ALTER TABLE [dbo].[Factura_Detalle] CHECK CONSTRAINT [FK_Factura_Detalle_Factura_Encabezado]
GO
ALTER TABLE [dbo].[Factura_Encabezado]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Encabezado_clientes] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Factura_Encabezado] CHECK CONSTRAINT [FK_Factura_Encabezado_clientes]
GO
ALTER TABLE [dbo].[G_PermisoTipoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_G_PermisoTipoUsuario_G_Menu_Opcion] FOREIGN KEY([id_opcion])
REFERENCES [dbo].[G_Menu_Opcion] ([id_opcion])
GO
ALTER TABLE [dbo].[G_PermisoTipoUsuario] CHECK CONSTRAINT [FK_G_PermisoTipoUsuario_G_Menu_Opcion]
GO
ALTER TABLE [dbo].[G_PermisoTipoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_G_PermisoTipoUsuario_G_TipoUsuario] FOREIGN KEY([id_tipousuario])
REFERENCES [dbo].[G_TipoUsuario] ([id_tipousuario])
GO
ALTER TABLE [dbo].[G_PermisoTipoUsuario] CHECK CONSTRAINT [FK_G_PermisoTipoUsuario_G_TipoUsuario]
GO
ALTER TABLE [dbo].[G_UsuarioPermiso]  WITH CHECK ADD  CONSTRAINT [FK_G_UsuarioPermiso_G_TipoUsuario] FOREIGN KEY([id_tipousuario])
REFERENCES [dbo].[G_TipoUsuario] ([id_tipousuario])
GO
ALTER TABLE [dbo].[G_UsuarioPermiso] CHECK CONSTRAINT [FK_G_UsuarioPermiso_G_TipoUsuario]
GO
ALTER TABLE [dbo].[G_UsuarioPermiso]  WITH CHECK ADD  CONSTRAINT [FK_G_UsuarioPermiso_G_Usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[G_Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[G_UsuarioPermiso] CHECK CONSTRAINT [FK_G_UsuarioPermiso_G_Usuarios]
GO
ALTER TABLE [dbo].[G_Usuarios_Seguridad]  WITH CHECK ADD  CONSTRAINT [FK_G_Usuarios_Seguridad_G_Usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[G_Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[G_Usuarios_Seguridad] CHECK CONSTRAINT [FK_G_Usuarios_Seguridad_G_Usuarios]
GO
ALTER TABLE [dbo].[Inventarios]  WITH CHECK ADD  CONSTRAINT [FK_Inventarios_Produtos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Produtos] ([id_producto])
GO
ALTER TABLE [dbo].[Inventarios] CHECK CONSTRAINT [FK_Inventarios_Produtos]
GO
ALTER TABLE [dbo].[Lineas]  WITH CHECK ADD  CONSTRAINT [FK_Lineas_Marcas] FOREIGN KEY([id_marca])
REFERENCES [dbo].[Marcas] ([id_marca])
GO
ALTER TABLE [dbo].[Lineas] CHECK CONSTRAINT [FK_Lineas_Marcas]
GO
ALTER TABLE [dbo].[Lineas]  WITH CHECK ADD  CONSTRAINT [FK_Lineas_Modelos] FOREIGN KEY([id_modelo])
REFERENCES [dbo].[Modelos] ([id_modelo])
GO
ALTER TABLE [dbo].[Lineas] CHECK CONSTRAINT [FK_Lineas_Modelos]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Categoria_Productos] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[Categoria_Productos] ([id_categoria])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Categoria_Productos]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Vehiculos] FOREIGN KEY([id_vehiculo])
REFERENCES [dbo].[Vehiculos] ([id_vehiculo])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Vehiculos]
GO
ALTER TABLE [dbo].[Servicio_Encabezado]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Encabezado_clientes] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Servicio_Encabezado] CHECK CONSTRAINT [FK_Servicio_Encabezado_clientes]
GO
ALTER TABLE [dbo].[Servicio_Encabezado]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Encabezado_Tipo_Servicio] FOREIGN KEY([id_tipo_servicio])
REFERENCES [dbo].[Tipo_Servicio] ([id_tipo_servicio])
GO
ALTER TABLE [dbo].[Servicio_Encabezado] CHECK CONSTRAINT [FK_Servicio_Encabezado_Tipo_Servicio]
GO
ALTER TABLE [dbo].[Servicio_Encabezado]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Encabezado_Vehiculos_Clientes] FOREIGN KEY([id_vehiculo_cliente])
REFERENCES [dbo].[Vehiculos_Clientes] ([id_vehiculo_cliente])
GO
ALTER TABLE [dbo].[Servicio_Encabezado] CHECK CONSTRAINT [FK_Servicio_Encabezado_Vehiculos_Clientes]
GO
ALTER TABLE [dbo].[Servicio_Externo_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Externo_Detalle_Servicio_Encabezado] FOREIGN KEY([id_servicio])
REFERENCES [dbo].[Servicio_Encabezado] ([id_servicio])
GO
ALTER TABLE [dbo].[Servicio_Externo_Detalle] CHECK CONSTRAINT [FK_Servicio_Externo_Detalle_Servicio_Encabezado]
GO
ALTER TABLE [dbo].[Servicio_Repuesto_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Repuesto_Detalle_Produtos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Produtos] ([id_producto])
GO
ALTER TABLE [dbo].[Servicio_Repuesto_Detalle] CHECK CONSTRAINT [FK_Servicio_Repuesto_Detalle_Produtos]
GO
ALTER TABLE [dbo].[Servicio_Repuesto_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Repuesto_Detalle_Servicio_Encabezado] FOREIGN KEY([id_servicio])
REFERENCES [dbo].[Servicio_Encabezado] ([id_servicio])
GO
ALTER TABLE [dbo].[Servicio_Repuesto_Detalle] CHECK CONSTRAINT [FK_Servicio_Repuesto_Detalle_Servicio_Encabezado]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculos_Lineas] FOREIGN KEY([id_linea])
REFERENCES [dbo].[Lineas] ([id_linea])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK_Vehiculos_Lineas]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculos_Marcas] FOREIGN KEY([id_marca])
REFERENCES [dbo].[Marcas] ([id_marca])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK_Vehiculos_Marcas]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculos_Modelos] FOREIGN KEY([id_modelo])
REFERENCES [dbo].[Modelos] ([id_modelo])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK_Vehiculos_Modelos]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculos_Tipo_Vehiculo] FOREIGN KEY([id_tipo_vehiculo])
REFERENCES [dbo].[Tipo_Vehiculo] ([id_tipo_vehiculo])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK_Vehiculos_Tipo_Vehiculo]
GO
ALTER TABLE [dbo].[Vehiculos_Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculos_Clientes_clientes] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Vehiculos_Clientes] CHECK CONSTRAINT [FK_Vehiculos_Clientes_clientes]
GO
ALTER TABLE [dbo].[Vehiculos_Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculos_Clientes_Vehiculos] FOREIGN KEY([id_vehiculo])
REFERENCES [dbo].[Vehiculos] ([id_vehiculo])
GO
ALTER TABLE [dbo].[Vehiculos_Clientes] CHECK CONSTRAINT [FK_Vehiculos_Clientes_Vehiculos]
GO
USE [master]
GO
ALTER DATABASE [Repuestos] SET  READ_WRITE 
GO
