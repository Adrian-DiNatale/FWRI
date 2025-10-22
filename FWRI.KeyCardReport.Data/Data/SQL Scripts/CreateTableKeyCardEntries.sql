USE [FWRI]
GO

/****** Object:  Table [dbo].[KeyCardEntries]    Script Date: 10/22/2025 11:43:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KeyCardEntries](
	[EntryId] [uniqueidentifier] NOT NULL,
	[EntryDateTime] [datetime] NOT NULL,
	[KeyCardId] [uniqueidentifier] NOT NULL,
	[ScannerId] [uniqueidentifier] NOT NULL,
	[EntryCategoryId] [int] NOT NULL,
	[SecurityImageId] [int] NOT NULL,
 CONSTRAINT [PK_KeyCardEntries] PRIMARY KEY CLUSTERED 
(
	[EntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_KeyCardEntries] UNIQUE NONCLUSTERED 
(
	[KeyCardId] ASC,
	[EntryDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[KeyCardEntries]  WITH CHECK ADD  CONSTRAINT [FK_KeyCardEntries_Categories] FOREIGN KEY([EntryCategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO

ALTER TABLE [dbo].[KeyCardEntries] CHECK CONSTRAINT [FK_KeyCardEntries_Categories]
GO

ALTER TABLE [dbo].[KeyCardEntries]  WITH CHECK ADD  CONSTRAINT [FK_KeyCardEntries_Employees] FOREIGN KEY([KeyCardId])
REFERENCES [dbo].[Employees] ([KeyCardId])
GO

ALTER TABLE [dbo].[KeyCardEntries] CHECK CONSTRAINT [FK_KeyCardEntries_Employees]
GO

ALTER TABLE [dbo].[KeyCardEntries]  WITH CHECK ADD  CONSTRAINT [FK_KeyCardEntries_Images] FOREIGN KEY([SecurityImageId])
REFERENCES [dbo].[Images] ([ImageId])
GO

ALTER TABLE [dbo].[KeyCardEntries] CHECK CONSTRAINT [FK_KeyCardEntries_Images]
GO


