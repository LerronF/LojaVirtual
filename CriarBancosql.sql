USE [master]
GO

/****** Object:  Database [BDTransire]    Script Date: 03/08/2021 14:50:41 ******/
CREATE DATABASE [BDTransire]
 CONTAINMENT = NONE  
GO

ALTER DATABASE [BDTransire] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [BDTransire] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [BDTransire] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [BDTransire] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [BDTransire] SET ARITHABORT OFF 
GO

ALTER DATABASE [BDTransire] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [BDTransire] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [BDTransire] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [BDTransire] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [BDTransire] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [BDTransire] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [BDTransire] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [BDTransire] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [BDTransire] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [BDTransire] SET  DISABLE_BROKER 
GO

ALTER DATABASE [BDTransire] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [BDTransire] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [BDTransire] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [BDTransire] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [BDTransire] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [BDTransire] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [BDTransire] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [BDTransire] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [BDTransire] SET  MULTI_USER 
GO

ALTER DATABASE [BDTransire] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [BDTransire] SET DB_CHAINING OFF 
GO

ALTER DATABASE [BDTransire] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [BDTransire] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [BDTransire] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [BDTransire] SET QUERY_STORE = OFF
GO

ALTER DATABASE [BDTransire] SET  READ_WRITE 
GO


USE [BDTransire]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
	[Quantidade] [decimal](18, 3) NULL,
	[Valor] [decimal](18, 3) NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendasItens]   Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendasItens](
	[Pedido] [int] NOT NULL,
	[Produto] [int] NOT NULL,
	[ValUnit] [decimal](18, 0) NULL,
	[QuantProd] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_VendasItens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendas]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodCli] [int] NOT NULL,
	[Data] [datetime] NULL,
	[Total] [decimal](18, 0) NULL,
	[Cliente] [varchar](50) NULL,
 CONSTRAINT [PK_Vendas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ContProdVend]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ContProdVend] AS
SELECT  Top 10 COUNT(VI.PRODUTO) VENDAS,
	   P.DESCRICAO,
       VI.PRODUTO as IDPRODUTO
FROM VENDAS V
INNER JOIN VENDASITENS  VI ON VI.PEDIDO = V.ID
INNER JOIN PRODUTOS P ON P.ID = VI.PRODUTO
WHERE YEAR(V.DATA) = 2021
GROUP BY VI.PRODUTO,
         P.DESCRICAO
ORDER BY Vendas
 
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vendas3MaioresClienteNome]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vendas3MaioresClienteNome] AS
Select T.Nome From(SELECT  Top 3 Sum(Vi.ValUnit) VENDAS,
	   C.Nome
FROM VENDAS V
Inner join VendasItens Vi on Vi.Pedido = V.Id
Inner join Clientes C on C.Id = V.CodCli
WHERE V.Data between '2021-04-01' and '2021-08-30'
Group By C.Nome)T


 
GO
/****** Object:  View [dbo].[Vendas2MaioresClienteValor]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vendas2MaioresClienteValor] AS
SELECT        TOP (2) SUM(Vi.ValUnit) AS VENDAS, C.Nome
FROM            dbo.Vendas AS V INNER JOIN
                         dbo.VendasItens AS Vi ON Vi.Pedido = V.Id INNER JOIN
                         dbo.Clientes AS C ON C.Id = V.CodCli
WHERE        V.Data between '2021-04-01' and '2021-08-30'
GROUP BY C.Nome
GO
/****** Object:  Table [dbo].[Estoque]    Script Date: 03/08/2021 14:50:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estoque](
	[IdProduto] [int] NOT NULL,
	[Quant] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_Estoque] PRIMARY KEY CLUSTERED 
(
	[IdProduto] ASC
)
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([Id], [Nome]) VALUES (1, N'Raimundo')
INSERT [dbo].[Clientes] ([Id], [Nome]) VALUES (2, N'Lerron')
INSERT [dbo].[Clientes] ([Id], [Nome]) VALUES (3, N'Carlos')
INSERT [dbo].[Clientes] ([Id], [Nome]) VALUES (4, N'Marcelo')
INSERT [dbo].[Clientes] ([Id], [Nome]) VALUES (5, N'Eudes')
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
INSERT [dbo].[Estoque] ([IdProduto], [Quant]) VALUES (1, CAST(10.000 AS Decimal(18, 3)))
INSERT [dbo].[Estoque] ([IdProduto], [Quant]) VALUES (2, CAST(5.000 AS Decimal(18, 3)))
INSERT [dbo].[Estoque] ([IdProduto], [Quant]) VALUES (3, CAST(2.000 AS Decimal(18, 3)))
INSERT [dbo].[Estoque] ([IdProduto], [Quant]) VALUES (4, CAST(4.000 AS Decimal(18, 3)))
INSERT [dbo].[Estoque] ([IdProduto], [Quant]) VALUES (5, CAST(12.000 AS Decimal(18, 3)))
INSERT [dbo].[Estoque] ([IdProduto], [Quant]) VALUES (6, CAST(7.000 AS Decimal(18, 3)))
GO
SET IDENTITY_INSERT [dbo].[Produtos] ON 

INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (1, N'Arroz', CAST(23.000 AS Decimal(18, 3)), CAST(55.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (2, N'Macarrão', CAST(12.000 AS Decimal(18, 3)), CAST(45.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (3, N'Feijão', CAST(10.000 AS Decimal(18, 3)), CAST(56.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (4, N'Farinha', CAST(12.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (5, N'Limão', CAST(13.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (6, N'Melão', CAST(12.000 AS Decimal(18, 3)), CAST(5.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (7, N'Produto Teste', CAST(12.000 AS Decimal(18, 3)), CAST(12.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (8, N'Farinha Seca', CAST(2.000 AS Decimal(18, 3)), CAST(9.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (9, N'Tomilho', CAST(14.000 AS Decimal(18, 3)), CAST(5.000 AS Decimal(18, 3)))
INSERT [dbo].[Produtos] ([Id], [Descricao], [Quantidade], [Valor]) VALUES (10, N'Laranja', CAST(19.000 AS Decimal(18, 3)), CAST(33.000 AS Decimal(18, 3)))
SET IDENTITY_INSERT [dbo].[Produtos] OFF
GO
SET IDENTITY_INSERT [dbo].[Vendas] ON 

INSERT [dbo].[Vendas] ([Id], [CodCli], [Data], [Total], [Cliente]) VALUES (57, 1, CAST(N'2020-06-16T00:00:00.000' AS DateTime), CAST(56 AS Decimal(18, 0)), N'Carlos')
INSERT [dbo].[Vendas] ([Id], [CodCli], [Data], [Total], [Cliente]) VALUES (58, 1, CAST(N'2020-04-10T00:00:00.000' AS DateTime), CAST(50 AS Decimal(18, 0)), N'Marcelo')
INSERT [dbo].[Vendas] ([Id], [CodCli], [Data], [Total], [Cliente]) VALUES (62, 1, CAST(N'2020-05-15T00:00:00.000' AS DateTime), CAST(60 AS Decimal(18, 0)), N'Lerron')
INSERT [dbo].[Vendas] ([Id], [CodCli], [Data], [Total], [Cliente]) VALUES (76, 1, CAST(N'2020-06-10T00:00:00.000' AS DateTime), CAST(100 AS Decimal(18, 0)), N'Eudes')
SET IDENTITY_INSERT [dbo].[Vendas] OFF
GO
SET IDENTITY_INSERT [dbo].[VendasItens] ON 

INSERT [dbo].[VendasItens] ([Pedido], [Produto], [ValUnit], [QuantProd], [Id]) VALUES (57, 2, CAST(55 AS Decimal(18, 0)), 23, 21)
INSERT [dbo].[VendasItens] ([Pedido], [Produto], [ValUnit], [QuantProd], [Id]) VALUES (58, 3, CAST(33 AS Decimal(18, 0)), 3, 22)
INSERT [dbo].[VendasItens] ([Pedido], [Produto], [ValUnit], [QuantProd], [Id]) VALUES (62, 6, CAST(35 AS Decimal(18, 0)), 2, 26)
INSERT [dbo].[VendasItens] ([Pedido], [Produto], [ValUnit], [QuantProd], [Id]) VALUES (62, 1, CAST(51 AS Decimal(18, 0)), 13, 27)
INSERT [dbo].[VendasItens] ([Pedido], [Produto], [ValUnit], [QuantProd], [Id]) VALUES (76, 8, CAST(45 AS Decimal(18, 0)), 14, 33)

SET IDENTITY_INSERT [dbo].[VendasItens] OFF
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_Estoque_Produtos] FOREIGN KEY([IdProduto])
REFERENCES [dbo].[Produtos] ([Id])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_Estoque_Produtos]
GO
ALTER TABLE [dbo].[Vendas]  WITH CHECK ADD  CONSTRAINT [FK_Vendas_Clientes] FOREIGN KEY([CodCli])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Vendas] CHECK CONSTRAINT [FK_Vendas_Clientes]
GO
ALTER TABLE [dbo].[VendasItens]  WITH CHECK ADD  CONSTRAINT [FK_VendasItens_Vendas] FOREIGN KEY([Pedido])
REFERENCES [dbo].[Vendas] ([Id])
GO
ALTER TABLE [dbo].[VendasItens] CHECK CONSTRAINT [FK_VendasItens_Vendas]
GO
USE [master]
GO
ALTER DATABASE [BDTransire] SET  READ_WRITE 
GO
