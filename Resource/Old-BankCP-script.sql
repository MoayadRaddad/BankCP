USE [TSDApp]
GO
/****** Object:  Table [dbo].[tblAllocateCounterService]    Script Date: 03/02/2021 09:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAllocateCounterService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[counterId] [int] NULL,
	[serviceId] [int] NULL,
 CONSTRAINT [PK_tblAllocate_Counter_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBranches]    Script Date: 03/02/2021 09:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBranches](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[active] [bit] NULL,
	[bankId] [int] NULL,
 CONSTRAINT [PK_tblBranches] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCounters]    Script Date: 03/02/2021 09:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCounters](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[active] [bit] NULL,
	[type] [nvarchar](100) NULL,
	[branchId] [int] NULL,
 CONSTRAINT [PK_tblCounters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblService]
ADD arName [nvarchar](max), bankId [int], active [bit], tickets [int];
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](100) NULL,
	[password] [nvarchar](100) NULL,
	[bankId] [int] NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocate_Counter_Service_tblCounters] FOREIGN KEY([counterId])
REFERENCES [dbo].[tblCounters] ([id])
GO
ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocate_Counter_Service_tblCounters]
GO
ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocate_Counter_Service_tblService] FOREIGN KEY([serviceId])
REFERENCES [dbo].[tblService] ([id])
GO
ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocate_Counter_Service_tblService]
GO
ALTER TABLE [dbo].[tblBranches]  WITH CHECK ADD  CONSTRAINT [FK_tblBranches_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblBranches] CHECK CONSTRAINT [FK_tblBranches_tblBanks]
GO
ALTER TABLE [dbo].[tblCounters]  WITH CHECK ADD  CONSTRAINT [FK_tblCounters_tblBranches] FOREIGN KEY([branchId])
REFERENCES [dbo].[tblBranches] ([id])
GO
ALTER TABLE [dbo].[tblCounters] CHECK CONSTRAINT [FK_tblCounters_tblBranches]
GO
ALTER TABLE [dbo].[tblService]  WITH CHECK ADD  CONSTRAINT [FK_tblService_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblService] CHECK CONSTRAINT [FK_tblService_tblBanks]
GO
ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblBanks]
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_Allocate_Counter]    Script Date: 03/02/2021 09:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Delete_Allocate_Counter] 
@branchId int
as
begin
delete from tblAllocateCounterService where tblAllocateCounterService.counterId in (select id from tblCounters where branchId = @branchId);
delete from tblCounters where branchId = @branchId;
end
GO
USE [master]
GO
ALTER DATABASE [TSDApp] SET  READ_WRITE 
USE [TSDApp]
GO
sp_rename 'dbo.tblService.name', 'enName', 'COLUMN';
