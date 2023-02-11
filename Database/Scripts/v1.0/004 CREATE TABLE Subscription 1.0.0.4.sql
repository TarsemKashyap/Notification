IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subscription]') AND type in (N'U'))
	SET NOEXEC ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Subscription](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Merchant_ID] [int] NOT NULL,
	[Event_Type_ID] [int] NOT NULL,
	[Delivery_Method_ID] [int] NOT NULL,
	[Delivery_Address] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Subscribed_By] [nvarchar](255) NOT NULL,
	[Subscription_Date] [datetime] NOT NULL,
	[Subscription_Terminated] [bit] NOT NULL,
	[Termination_Date] [datetime] NULL,
	[Created_By] [nvarchar](255) NOT NULL,
	[Creation_Date] [datetime] NOT NULL,
	[Last_Modified_By] [nvarchar](255) NULL,
	[Last_Modified_Date] [datetime] NULL,
 CONSTRAINT [PK_t_Subscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO