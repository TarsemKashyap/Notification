IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notification]') AND type in (N'U'))
	SET NOEXEC ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Notification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Event_ID] [uniqueidentifier] NOT NULL,
	[Subscription_ID] [bigint] NOT NULL,
	[Delivery_Method_ID] [int] NOT NULL,
	[Delivery_Address] [nvarchar](4000) NOT NULL,
	[Notification_Sent] [datetime] NOT NULL,
	[Notification_Status_ID] [int] NOT NULL,
	[Comms_Tracking_ID] [nvarchar](50) NULL,
	[Created_By] [nvarchar](255) NOT NULL,
	[Creation_Date] [datetime] NOT NULL,
	[Last_Modified_By] [nvarchar](255) NULL,
	[Last_Modified_Date] [datetime] NULL,
 CONSTRAINT [PK_t_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Event] ([Id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Event]
GO

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_t_Notification_t_Subscription] FOREIGN KEY([Subscription_ID])
REFERENCES [dbo].[Subscription] ([Id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_t_Notification_t_Subscription]
GO
