The following query can be used to have the db locally, I went for the database first approcah:

CREATE DATABASE InternalResourceBookingSystem
USE [InternalResourceBookingSystem]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Resource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Description] [varchar](500) NULL,
	[Location] [varchar](500) NULL,
	[Capacity] [int] NULL,
	[IsAvailable] [bit] NULL,
	[IsUnderMaintenance] [bit] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[BookedBy] [varchar](500) NULL,
	[Purpose] [varchar](500) NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([Id])
GO

ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Resource]
GO
**************************************************************************************************************************************************************************

API
To update the scarfold you can run the following line on the Package Manager Console: Replace YOURCONNECTIONNAME with your local server name
Scaffold-DbContext "Data Source=YOURCONNECTIONNAME; Initial Catalog=InternalResourceBookingSystem; Trusted_Connection=True; Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities\IRBSEntities -Context IRBS_dataContext -NoOnConfiguring -Force

**************************************************************************************************************************************************************************

Images are also attached on the project in a folder called IMAGEPROJECT

**************************************************************************************************************************************************************************

How the system works, a user creates a recourse & can be able to update, view & delete the changes. From the if there's a booking linked to that recourse then it will show
in a table inside a dropdown.

The user can then create bookings & attach them to a recourse, the user can create, update, view & delete a booking
