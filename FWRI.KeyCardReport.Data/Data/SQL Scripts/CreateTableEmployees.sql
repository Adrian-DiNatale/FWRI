USE [FWRI]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 10/22/2025 11:43:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[WorkEmail] [nvarchar](100) NOT NULL,
	[WorkTitle] [nvarchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[BirthDate] [date] NOT NULL,
	[ProfilePictureId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[KeyCardId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Employees] UNIQUE NONCLUSTERED 
(
	[KeyCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Images] FOREIGN KEY([ProfilePictureId])
REFERENCES [dbo].[Images] ([ImageId])
GO

ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Images]
GO


