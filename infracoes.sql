CREATE DATABASE [Infracoes]

USE [Infracoes]

CREATE TABLE [dbo].[Agente](
	[IdAgente] [int] IDENTITY(1,1) NOT NULL,
	[NomeAgente] [varchar](50) NULL,
	[Efetivacao] [datetime] NOT NULL,
	[TempoServico] [int] NOT NULL,
	[Matricula] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Agente] PRIMARY KEY CLUSTERED 
(
	[IdAgente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Infracao](
	[IdInfracao] [int] IDENTITY(1,1) NOT NULL,
	[Velocidade] [float] NOT NULL,
	[IdVeiculo] [int] NOT NULL,
	[IdLogradouro] [int] NOT NULL,
	[IdAgente] [int] NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Infracao] PRIMARY KEY CLUSTERED 
(
	[IdInfracao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logradouro]    Script Date: 20/12/2019 10:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logradouro](
	[IdLogradouro] [int] IDENTITY(1,1) NOT NULL,
	[Rua] [varchar](50) NOT NULL,
	[Bairro] [varchar](50) NOT NULL,
	[Cidade] [varchar](50) NOT NULL,
	[Estado] [varchar](50) NOT NULL,
	[Cep] [varchar](50) NOT NULL,
	[VelocidadeMax] [float] NOT NULL,
 CONSTRAINT [PK_Logradouro] PRIMARY KEY CLUSTERED 
(
	[IdLogradouro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modelo]    Script Date: 20/12/2019 10:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modelo](
	[IdModelo] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Modelo] PRIMARY KEY CLUSTERED 
(
	[IdModelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proprietario]    Script Date: 20/12/2019 10:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proprietario](
	[IdProprietario] [int] IDENTITY(1,1) NOT NULL,
	[NomeProprietario] [varchar](50) NULL,
	[CpfProprietario] [varchar](50) NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[Telefone] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Proprietario] PRIMARY KEY CLUSTERED 
(
	[IdProprietario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Veiculo]    Script Date: 20/12/2019 10:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veiculo](
	[IdVeiculo] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [varchar](50) NOT NULL,
	[Uf] [varchar](50) NOT NULL,
	[IdInfracao] [int] NULL,
	[IdModelo] [int] NOT NULL,
	[IdProprietario] [int] NOT NULL,
 CONSTRAINT [PK_Veiculo] PRIMARY KEY CLUSTERED 
(
	[IdVeiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Infracao]  WITH CHECK ADD  CONSTRAINT [FK_Infracao_Agente] FOREIGN KEY([IdAgente])
REFERENCES [dbo].[Agente] ([IdAgente])
GO
ALTER TABLE [dbo].[Infracao] CHECK CONSTRAINT [FK_Infracao_Agente]
GO
ALTER TABLE [dbo].[Infracao]  WITH CHECK ADD  CONSTRAINT [FK_Infracao_Logradouro] FOREIGN KEY([IdLogradouro])
REFERENCES [dbo].[Logradouro] ([IdLogradouro])
GO
ALTER TABLE [dbo].[Infracao] CHECK CONSTRAINT [FK_Infracao_Logradouro]
GO
ALTER TABLE [dbo].[Infracao]  WITH CHECK ADD  CONSTRAINT [FK_Infracao_Veiculo] FOREIGN KEY([IdVeiculo])
REFERENCES [dbo].[Veiculo] ([IdVeiculo])
GO
ALTER TABLE [dbo].[Infracao] CHECK CONSTRAINT [FK_Infracao_Veiculo]
GO
ALTER TABLE [dbo].[Veiculo]  WITH CHECK ADD  CONSTRAINT [FK_Veiculo_Modelo] FOREIGN KEY([IdModelo])
REFERENCES [dbo].[Modelo] ([IdModelo])
GO
ALTER TABLE [dbo].[Veiculo] CHECK CONSTRAINT [FK_Veiculo_Modelo]
GO
ALTER TABLE [dbo].[Veiculo]  WITH CHECK ADD  CONSTRAINT [FK_Veiculo_Proprietario] FOREIGN KEY([IdProprietario])
REFERENCES [dbo].[Proprietario] ([IdProprietario])
GO
ALTER TABLE [dbo].[Veiculo] CHECK CONSTRAINT [FK_Veiculo_Proprietario]
GO
USE [master]
GO
ALTER DATABASE [Infracoes] SET  READ_WRITE 
GO
