IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Merchant_Configuration]') AND type in (N'U'))
	SET NOEXEC ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Merchant_Configuration](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Merchant_ID] [int] NOT NULL,
	[Secret] [nvarchar](50) NOT NULL,
	[Created_By] [nvarchar](255) NOT NULL,
	[Creation_Date] [datetime] NOT NULL,
	[Last_Modified_By] [nvarchar](255) NULL,
	[Last_Modified_Date] [datetime] NULL,
 CONSTRAINT [PK_Merchant_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO