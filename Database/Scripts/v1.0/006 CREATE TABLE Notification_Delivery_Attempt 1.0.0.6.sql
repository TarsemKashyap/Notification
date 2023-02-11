IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notification_Delivery_Attempt]') AND type in (N'U'))
	SET NOEXEC ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Notification_Delivery_Attempt](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Notification_ID] [bigint] NOT NULL,
	[Delivery_Timestamp] [datetime] NOT NULL,
	[Delivery_Successful] [bit] NOT NULL,
	[Delivery_Http_Response_Code] [int] NOT NULL,
	[Delivery_Failure_Reason] [nvarchar](255) NULL,
	[Created_By] [nvarchar](255) NOT NULL,
	[Creation_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_t_Notification_Delivery_Attempt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Notification_Delivery_Attempt]  WITH CHECK ADD  CONSTRAINT [FK_t_Notification_Delivery_Attempt_t_Notification] FOREIGN KEY([Notification_ID])
REFERENCES [dbo].[Notification] ([Id])
GO

ALTER TABLE [dbo].[Notification_Delivery_Attempt] CHECK CONSTRAINT [FK_t_Notification_Delivery_Attempt_t_Notification]
GO