USE [master]
IF (not EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'TSDApp' OR name = N'TSDApp')))
begin
CREATE DATABASE [TSDApp]
CONTAINMENT = NONE
end
GO
USE [TSDApp]
GO
/****** Object:  Table [dbo].[tblAllocateCounterService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblAllocateCounterService'))
BEGIN
CREATE TABLE [dbo].[tblAllocateCounterService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[counterId] [int] NULL,
	[serviceId] [int] NULL,
	[bankId] [int] NULL,
 CONSTRAINT [PK_tblAllocate_Counter_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END

GO
if not EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblAllocateCounterService' AND COLUMN_NAME = 'bankId' )
begin
ALTER TABLE [dbo].[tblAllocateCounterService]
ADD bankId [int];
end
GO
/****** Object:  Table [dbo].[tblBanks]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblBanks'))
BEGIN
CREATE TABLE [dbo].[tblBanks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK__tblBanks__3213E83F65F81309] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
/****** Object:  Table [dbo].[tblBranches]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblBranches'))
BEGIN
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
end
GO
/****** Object:  Table [dbo].[tblCounters]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblCounters'))
BEGIN
CREATE TABLE [dbo].[tblCounters](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[active] [bit] NULL,
	[type] [nvarchar](100) NULL,
	[branchId] [int] NULL,
	[bankId] [int] NULL,
 CONSTRAINT [PK_tblCounters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
if not EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblCounters' AND COLUMN_NAME = 'bankId' )
begin
ALTER TABLE [dbo].[tblCounters]
ADD bankId [int];
end
GO
/****** Object:  Table [dbo].[tblIssueTicketButton]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblIssueTicketButton'))
BEGIN
CREATE TABLE [dbo].[tblIssueTicketButton](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[serviceId] [int] NULL,
	[screenId] [int] NULL,
	[isBusy] [bit] NULL,
 CONSTRAINT [PK_tblIssueTicket] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
/****** Object:  Table [dbo].[tblScreens]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblScreens'))
BEGIN
CREATE TABLE [dbo].[tblScreens](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[bankId] [int] NULL,
	[isBusy] [bit] NULL,
 CONSTRAINT [PK__tblScree__3213E83F162B5424] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
/****** Object:  Table [dbo].[tblService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblService'))
BEGIN
CREATE TABLE [dbo].[tblService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](max) NULL,
	[arName] [nvarchar](max) NULL,
	[bankId] [int] NULL,
	[active] [bit] NULL,
	[maxNumOfTickets] [int] NULL,
 CONSTRAINT [PK__IssueTic__3213E83F111E6603] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
end
GO
/****** Object:  Table [dbo].[tblShowMessageButton]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblShowMessageButton'))
BEGIN
CREATE TABLE [dbo].[tblShowMessageButton](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enName] [nvarchar](100) NULL,
	[arName] [nvarchar](100) NULL,
	[messageEN] [nvarchar](100) NULL,
	[messageAR] [nvarchar](100) NULL,
	[screenId] [int] NULL,
	[isBusy] [bit] NULL,
 CONSTRAINT [PK_tblShowMessage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo' AND  TABLE_NAME = N'tblUsers'))
BEGIN
CREATE TABLE [dbo].[tblUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](100) NOT NULL,
	[password] [nvarchar](100) NULL,
	[bankId] [int] NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [unique_userName_bankId] UNIQUE NONCLUSTERED 
(
	[userName] ASC,
	[bankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
if EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblService' AND COLUMN_NAME = 'name' )
begin
exec sp_rename 'dbo.tblService.name', 'enName', 'COLUMN';
ALTER TABLE [dbo].[tblService]
ADD arName [nvarchar](max), bankId [int], active [bit], maxNumOfTickets [int];
end
GO
if not EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblService' AND COLUMN_NAME = 'minimumServiceTime' )
begin
ALTER TABLE [dbo].[tblService]
ADD minimumServiceTime [int], maximumServiceTime [int];
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblAllocateCounterService' AND CONSTRAINT_NAME = N'FK_tblAllocate_Counter_Service_tblCounters'))
BEGIN
ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocate_Counter_Service_tblCounters] FOREIGN KEY([counterId])
REFERENCES [dbo].[tblCounters] ([id])
ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocate_Counter_Service_tblCounters]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblAllocateCounterService' AND CONSTRAINT_NAME = N'FK_tblAllocate_Counter_Service_tblService'))
BEGIN
ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocate_Counter_Service_tblService] FOREIGN KEY([serviceId])
REFERENCES [dbo].[tblService] ([id])
ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocate_Counter_Service_tblService]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblAllocateCounterService' AND CONSTRAINT_NAME = N'FK_tblAllocateCounterService_tblBanks'))
BEGIN
ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocateCounterService_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocateCounterService_tblBanks]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblBranches' AND CONSTRAINT_NAME = N'FK_tblBranches_tblBanks'))
BEGIN
ALTER TABLE [dbo].[tblBranches]  WITH CHECK ADD  CONSTRAINT [FK_tblBranches_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
ALTER TABLE [dbo].[tblBranches] CHECK CONSTRAINT [FK_tblBranches_tblBanks]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblCounters' AND CONSTRAINT_NAME = N'FK_tblCounters_tblBranches'))
BEGIN
ALTER TABLE [dbo].[tblCounters]  WITH CHECK ADD  CONSTRAINT [FK_tblCounters_tblBranches] FOREIGN KEY([branchId])
REFERENCES [dbo].[tblBranches] ([id])
ALTER TABLE [dbo].[tblCounters] CHECK CONSTRAINT [FK_tblCounters_tblBranches]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblCounters' AND CONSTRAINT_NAME = N'FK_tblCounters_tblBanks'))
BEGIN
ALTER TABLE [dbo].[tblCounters]  WITH CHECK ADD  CONSTRAINT [FK_tblCounters_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
ALTER TABLE [dbo].[tblCounters] CHECK CONSTRAINT [FK_tblCounters_tblBanks]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblIssueTicketButton' AND CONSTRAINT_NAME = N'FK_tblIssueTicket_tblScreens'))
BEGIN
ALTER TABLE [dbo].[tblIssueTicketButton]  WITH CHECK ADD  CONSTRAINT [FK_tblIssueTicket_tblScreens] FOREIGN KEY([screenId])
REFERENCES [dbo].[tblScreens] ([id])
ALTER TABLE [dbo].[tblIssueTicketButton] CHECK CONSTRAINT [FK_tblIssueTicket_tblScreens]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblIssueTicketButton' AND CONSTRAINT_NAME = N'FK_tblIssueTicket_tblServiceType'))
BEGIN
ALTER TABLE [dbo].[tblIssueTicketButton]  WITH CHECK ADD  CONSTRAINT [FK_tblIssueTicket_tblServiceType] FOREIGN KEY([serviceId])
REFERENCES [dbo].[tblService] ([id])
ALTER TABLE [dbo].[tblIssueTicketButton] CHECK CONSTRAINT [FK_tblIssueTicket_tblServiceType]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblScreens' AND CONSTRAINT_NAME = N'FK__tblScreen__BankI__267ABA7A'))
BEGIN
ALTER TABLE [dbo].[tblScreens]  WITH CHECK ADD  CONSTRAINT [FK__tblScreen__BankI__267ABA7A] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
ALTER TABLE [dbo].[tblScreens] CHECK CONSTRAINT [FK__tblScreen__BankI__267ABA7A]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblService' AND CONSTRAINT_NAME = N'FK_tblService_tblBanks'))
BEGIN
ALTER TABLE [dbo].[tblService]  WITH CHECK ADD  CONSTRAINT [FK_tblService_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
ALTER TABLE [dbo].[tblService] CHECK CONSTRAINT [FK_tblService_tblBanks]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblShowMessageButton' AND CONSTRAINT_NAME = N'FK_tblShowMessage_tblScreens'))
BEGIN
ALTER TABLE [dbo].[tblShowMessageButton]  WITH CHECK ADD  CONSTRAINT [FK_tblShowMessage_tblScreens] FOREIGN KEY([screenId])
REFERENCES [dbo].[tblScreens] ([id])
ALTER TABLE [dbo].[tblShowMessageButton] CHECK CONSTRAINT [FK_tblShowMessage_tblScreens]
end
GO
IF (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'tblUsers' AND CONSTRAINT_NAME = N'FK_tblUsers_tblBanks'))
BEGIN
ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblBanks]
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_Allocate_Counter]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_Delete_Allocate_Counter' )
BEGIN
    DROP PROCEDURE [dbo].[sp_Delete_Allocate_Counter]
END
GO
CREATE proc [dbo].[sp_Delete_Allocate_Counter]
@branchId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @branchId))
begin
IF (EXISTS (SELECT * FROM tblBranches where bankId = @bankId and id = @branchId))
begin
delete from tblAllocateCounterService where tblAllocateCounterService.counterId in (select id from tblCounters where branchId = @branchId);
delete from tblCounters where branchId = @branchId;
select 1
end
else
select 0;
end
else
select 0
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_selectAllocateCounterService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_selectAllocateCounterService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_selectAllocateCounterService]
END
GO
CREATE proc [dbo].[sp_selectAllocateCounterService]
@counterId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters where tblcounters.id = @counterId))
begin
IF (EXISTS (SELECT * FROM tblCounters inner join tblBranches on tblCounters.branchId = tblBranches.id where tblcounters.id = @counterId and tblBranches.bankId = @bankId))
SELECT tblAllocateCounterService.*,tblService.enName as serviceEnName,tblService.arName as serviceArName FROM tblAllocateCounterService inner join tblService on tblAllocateCounterService.serviceId = tblService.id where tblAllocateCounterService.bankId = @bankId and tblAllocateCounterService.counterId = @counterId
else
select 0 as id;
end
else
select 0 as id;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_selectBranchesByBankId]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_selectBranchesByBankId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_selectBranchesByBankId]
END
GO
CREATE proc [dbo].[sp_selectBranchesByBankId]
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBanks where id = @bankId))
SELECT * FROM tblBranches where bankId = @bankId;
else
select 0 as id;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_selectCountersByBranchId]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_selectCountersByBranchId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_selectCountersByBranchId]
END
GO
CREATE proc [dbo].[sp_selectCountersByBranchId]
@branchId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @branchId and bankId = @bankId))
SELECT * FROM tblCounters where branchId = @branchId and bankId = @bankId
else
select 0 as id;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_selectServicesByBankId]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_selectServicesByBankId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_selectServicesByBankId]
END
GO
CREATE proc [dbo].[sp_selectServicesByBankId]
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBanks where id = @bankId))
SELECT * FROM tblService where bankId = @bankId;
else
select 0 as id;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertAllocateCounterService]    Script Date: 2/10/2021 11:11:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_insertAllocateCounterService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_insertAllocateCounterService]
END
GO
CREATE proc [dbo].[sp_insertAllocateCounterService]
@id int,
@serviceId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters where id = @id and bankId = @bankId))
insert into tblAllocateCounterService OUTPUT INSERTED.IDENTITYCOL  values (@id,@serviceId,@bankId)
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertAllocateCounterService]    Script Date: 11/02/2021 10:06:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_insertCounter' )
BEGIN
    DROP PROCEDURE [dbo].[sp_insertCounter]
END
GO

CREATE proc [dbo].[sp_insertCounter]
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@type nvarchar(100),
@branchId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @branchId and bankId = @bankId))
insert into tblCounters OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@type,@branchId,@bankId)
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END

GO
/****** Object:  StoredProcedure [dbo].[sp_deleteAllocateCounterServiceByServiceId]    Script Date: 14/02/2021 12:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteAllocateCounterServiceByServiceId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteAllocateCounterServiceByServiceId]
END
GO

create proc [dbo].[sp_deleteAllocateCounterServiceByServiceId]
@serviceId int,
@bankId int
as
begin
BEGIN TRY
delete from tblAllocateCounterService where serviceId = @serviceId and bankId = @bankId
select 1
END TRY  
BEGIN CATCH  
     select 0
END CATCH 
END

GO
/****** Object:  StoredProcedure [dbo].[sp_deleteAllocateCounterServiceByCounterId]    Script Date: 14/02/2021 12:01:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteAllocateCounterServiceByCounterId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteAllocateCounterServiceByCounterId]
END
GO

create proc [dbo].[sp_deleteAllocateCounterServiceByCounterId]
@counterId int,
@bankId int
as
begin
BEGIN TRY
delete from tblAllocateCounterService where counterId = @counterId and bankId = @bankId
select 1
END TRY  
BEGIN CATCH  
     select 0
END CATCH 
END

GO
update tblService set minimumServiceTime = 45, maximumServiceTime = 300 where minimumServiceTime is null

GO
USE [master]
GO
ALTER DATABASE [TSDApp] SET  READ_WRITE 
GO
