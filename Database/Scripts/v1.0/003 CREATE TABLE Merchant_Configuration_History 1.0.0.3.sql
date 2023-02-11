IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Merchant_Configuration_History]') AND type in (N'U'))
	SET NOEXEC ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Merchant_Configuration_History](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Merchant_Configuration_ID] [bigint] NOT NULL,
	[Merchant_ID] [int] NOT NULL,
	[Secret] [nvarchar](50) NOT NULL,
	[Created_By] [nvarchar](255) NOT NULL,
	[Creation_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Merchant_Configuration_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO