# LojaVirtual
Transire - Criação de uma Loja Virtual Fictícia.

Acima está criado o projeto "Loja", uma ideia de uma Loja Virtual Fictícia com apenas o cadastro de produtos.
O cadastro de produtos é básico, possui apenas a descrição e estoque.
Nesse cadastro tem o CRUD padrão, e adicionei algumas bibliotecas como por exemplo o "AlertifyJS", para notificar cada operação do CRUD, e foi criado com base no "Entity Framework".

Para rodar a aplicação, basta fazer download do projeto "Loja", e criar o banco e a tabela conforme abaixo:

#########################################################################################################################################
USE [LojaVirtual]
GO

/****** Object:  Table [dbo].[Produtos]    Script Date: 14/06/2018 10:46:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Produtos](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
	[Estoque] [int] NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
#########################################################################################################################################

E quanto as operações do cadastro são bem intuitivas e simples.

Att.


