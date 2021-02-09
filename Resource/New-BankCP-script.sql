DECLARE @dbname nvarchar(128)
SET @dbname = N'TSDApp'
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
drop database [TSDApp]

CREATE DATABASE [TSDApp]
 CONTAINMENT = NONE

USE [master]
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TSDApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TSDApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TSDApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TSDApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TSDApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TSDApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [TSDApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TSDApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TSDApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TSDApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TSDApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TSDApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TSDApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TSDApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TSDApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TSDApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TSDApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TSDApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TSDApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TSDApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TSDApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TSDApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TSDApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TSDApp] SET RECOVERY FULL 
GO
ALTER DATABASE [TSDApp] SET  MULTI_USER 
GO
ALTER DATABASE [TSDApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TSDApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TSDApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TSDApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TSDApp', N'ON'
GO
USE [TSDApp]
GO
/****** Object:  Table [dbo].[tblAllocateCounterService]    Script Date: 09/02/2021 11:30:24 ******/
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
/****** Object:  Table [dbo].[tblBanks]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBanks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK__tblBanks__3213E83F65F81309] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBranches]    Script Date: 09/02/2021 11:30:24 ******/
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
/****** Object:  Table [dbo].[tblCounters]    Script Date: 09/02/2021 11:30:24 ******/
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
/****** Object:  Table [dbo].[tblIssueTicketButton]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[tblScreens]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[tblService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[tblShowMessageButton]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
ALTER TABLE [dbo].[tblIssueTicketButton]  WITH CHECK ADD  CONSTRAINT [FK_tblIssueTicket_tblScreens] FOREIGN KEY([screenId])
REFERENCES [dbo].[tblScreens] ([id])
GO
ALTER TABLE [dbo].[tblIssueTicketButton] CHECK CONSTRAINT [FK_tblIssueTicket_tblScreens]
GO
ALTER TABLE [dbo].[tblIssueTicketButton]  WITH CHECK ADD  CONSTRAINT [FK_tblIssueTicket_tblServiceType] FOREIGN KEY([serviceId])
REFERENCES [dbo].[tblService] ([id])
GO
ALTER TABLE [dbo].[tblIssueTicketButton] CHECK CONSTRAINT [FK_tblIssueTicket_tblServiceType]
GO
ALTER TABLE [dbo].[tblScreens]  WITH CHECK ADD  CONSTRAINT [FK__tblScreen__BankI__267ABA7A] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblScreens] CHECK CONSTRAINT [FK__tblScreen__BankI__267ABA7A]
GO
ALTER TABLE [dbo].[tblService]  WITH CHECK ADD  CONSTRAINT [FK_tblService_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblService] CHECK CONSTRAINT [FK_tblService_tblBanks]
GO
ALTER TABLE [dbo].[tblShowMessageButton]  WITH CHECK ADD  CONSTRAINT [FK_tblShowMessage_tblScreens] FOREIGN KEY([screenId])
REFERENCES [dbo].[tblScreens] ([id])
GO
ALTER TABLE [dbo].[tblShowMessageButton] CHECK CONSTRAINT [FK_tblShowMessage_tblScreens]
GO
ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])
GO
ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblBanks]
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_Allocate_Counter]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
select -1;
end
else
select 0
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteAllocateCounterService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_deleteAllocateCounterService]
@id int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblAllocateCounterService where id = @id))
begin
IF (EXISTS (SELECT * FROM tblAllocateCounterService inner join tblService on tblAllocateCounterService.serviceId = tblService.id inner join tblBanks on tblService.bankId = tblBanks.id where tblBanks.id = @bankId and tblAllocateCounterService.id = @id))
delete from tblAllocateCounterService OUTPUT DELETED.IDENTITYCOL where id = @id;
else
select -1;
end
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteAllocateCounterServiceByCounterId]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_deleteAllocateCounterServiceByCounterId]
@counterId int,
@branchId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters where id = @counterId))
begin
IF (EXISTS (SELECT * FROM tblCounters where id = @counterId and branchId = @branchId))
delete from tblAllocateCounterService OUTPUT DELETED.IDENTITYCOL where counterId = @counterId
else
select -1;
end
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteAllocateCounterServiceByServiceId]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_deleteAllocateCounterServiceByServiceId]
@serviceId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblService where id = @serviceId))
begin
IF (EXISTS (SELECT * FROM tblService where bankId = @bankId and id = @serviceId))
delete from tblAllocateCounterService OUTPUT DELETED.IDENTITYCOL where serviceId = @serviceId
else
select -1;
end
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteBranch]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_deleteBranch]
@id int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @id))
begin
IF (EXISTS (SELECT * FROM tblBranches where bankId = @bankId and id = @id))
delete from tblBranches OUTPUT DELETED.IDENTITYCOL where id = @id;
else
select -1;
end
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteCounter]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_deleteCounter]
@id int,
@branchId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters where id = @id))
begin
IF (EXISTS (SELECT * FROM tblCounters where branchId = @branchId and id = @id))
delete from tblCounters OUTPUT DELETED.IDENTITYCOL where id = @id
else
select -1;
end
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_deleteService]
@id int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblService where id = @id))
begin
IF (EXISTS (SELECT * FROM tblService where bankId = @bankId and id = @id))
delete from tblservice OUTPUT DELETED.IDENTITYCOL where id = @id
else
select -1;
end
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertAllocateCounterService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_insertAllocateCounterService]
@counterId int,
@serviceId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters inner join tblBranches on tblCounters.branchId = tblBranches.id inner join tblBanks on tblBranches.bankId = tblBanks.id where tblCounters.id = @counterId))
insert into tblAllocateCounterService OUTPUT INSERTED.IDENTITYCOL  values (@counterId,@serviceId)
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertBranch]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_insertBranch]
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBanks where id = @bankId))
insert into tblBranches OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@bankId)
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertCounter]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_insertCounter]
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@type nvarchar(100),
@branchId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @branchId))
insert into tblCounters OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@type,@branchId)
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_insertService]
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@bankId int,
@maxNumOfTickets int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBanks where id = @bankId))
insert into tblService OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@bankId,@active,@maxNumOfTickets)
else
select 0;
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


CREATE proc [dbo].[sp_selectAllocateCounterService]
@counterId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters where tblcounters.id = @counterId))
begin
IF (EXISTS (SELECT * FROM tblCounters inner join tblBranches on tblCounters.branchId = tblBranches.id where tblcounters.id = @counterId and tblBranches.bankId = @bankId))
SELECT tblAllocateCounterService.*,tblService.enName as serviceEnName,tblService.arName as serviceArName FROM tblAllocateCounterService inner join tblService on tblAllocateCounterService.serviceId = tblService.id inner join tblBanks on tblService.bankId = tblBanks.id where tblBanks.id = @bankId and tblAllocateCounterService.counterId = @counterId
else
select -1 as id;
end
else
select 0 as id;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_selectBranchById]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_selectBranchById]
@branchId int
as
begin
IF (EXISTS (SELECT * FROM tblBranches))
BEGIN
SELECT * FROM tblBranches where id = @branchId
END
end
GO
/****** Object:  StoredProcedure [dbo].[sp_selectBranchesByBankId]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


CREATE proc [dbo].[sp_selectCountersByBranchId]
@branchId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @branchId and bankId = @bankId))
SELECT * FROM tblCounters where branchId = @branchId
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
/****** Object:  StoredProcedure [dbo].[sp_updateBranch]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_updateBranch]
@id int,
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where bankId = @bankId))
update tblBranches set enName = @enName,arName = @arName,active = @active OUTPUT INSERTED.IDENTITYCOL where id = @id;
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_updateCounter]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_updateCounter]
@id int,
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@type nvarchar(100),
@branchId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblBranches where id = @branchId))
update tblCounters set enName = @enName,arName = @arName,active = @active,type = @type OUTPUT INSERTED.IDENTITYCOL where id = @id
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_updateService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_updateService]
@id int,
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@maxNumOfTickets int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblService where bankId = @bankId))
update tblservice set enName = @enName,arName = @arName,active = @active,maxNumOfTickets = @maxNumOfTickets OUTPUT INSERTED.IDENTITYCOL where id = @id
else
select 0;
END TRY  
BEGIN CATCH  
     THROW; 
END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetErrorInfo]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetErrorInfo]  
AS  
SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_SEVERITY() AS ErrorSeverity  
    ,ERROR_STATE() AS ErrorState  
    ,ERROR_PROCEDURE() AS ErrorProcedure  
    ,ERROR_LINE() AS ErrorLine  
    ,ERROR_MESSAGE() AS ErrorMessage;  
GO
USE [master]
GO
ALTER DATABASE [TSDApp] SET  READ_WRITE 
GO
