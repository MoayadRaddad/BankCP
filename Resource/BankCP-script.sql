USE [master]
IF (not EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'TSDApp2' OR name = N'TSDApp2')))
begin
CREATE DATABASE [TSDApp2]
CONTAINMENT = NONE
end
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TSDApp2].[dbo].[sp_fulltext_database] @action = 'enable'
end
ALTER DATABASE [TSDApp2] SET ANSI_NULL_DEFAULT OFF 
ALTER DATABASE [TSDApp2] SET ANSI_NULLS OFF 
ALTER DATABASE [TSDApp2] SET ANSI_PADDING OFF 
ALTER DATABASE [TSDApp2] SET ANSI_WARNINGS OFF 
ALTER DATABASE [TSDApp2] SET ARITHABORT OFF 
ALTER DATABASE [TSDApp2] SET AUTO_CLOSE OFF 
ALTER DATABASE [TSDApp2] SET AUTO_SHRINK OFF 
ALTER DATABASE [TSDApp2] SET AUTO_UPDATE_STATISTICS ON 
ALTER DATABASE [TSDApp2] SET CURSOR_CLOSE_ON_COMMIT OFF 
ALTER DATABASE [TSDApp2] SET CURSOR_DEFAULT  GLOBAL 
ALTER DATABASE [TSDApp2] SET CONCAT_NULL_YIELDS_NULL OFF 
ALTER DATABASE [TSDApp2] SET NUMERIC_ROUNDABORT OFF 
ALTER DATABASE [TSDApp2] SET QUOTED_IDENTIFIER OFF 
ALTER DATABASE [TSDApp2] SET RECURSIVE_TRIGGERS OFF 
ALTER DATABASE [TSDApp2] SET  ENABLE_BROKER 
ALTER DATABASE [TSDApp2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
ALTER DATABASE [TSDApp2] SET DATE_CORRELATION_OPTIMIZATION OFF 
ALTER DATABASE [TSDApp2] SET TRUSTWORTHY OFF 
ALTER DATABASE [TSDApp2] SET PARAMETERIZATION SIMPLE 
ALTER DATABASE [TSDApp2] SET READ_COMMITTED_SNAPSHOT OFF 
ALTER DATABASE [TSDApp2] SET HONOR_BROKER_PRIORITY OFF 
ALTER DATABASE [TSDApp2] SET RECOVERY FULL 
ALTER DATABASE [TSDApp2] SET  MULTI_USER 
ALTER DATABASE [TSDApp2] SET PAGE_VERIFY CHECKSUM  
ALTER DATABASE [TSDApp2] SET DB_CHAINING OFF 
ALTER DATABASE [TSDApp2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
ALTER DATABASE [TSDApp2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
EXEC sys.sp_db_vardecimal_storage_format N'TSDApp2', N'ON'
GO
USE [TSDApp2]
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
 CONSTRAINT [PK_tblAllocate_Counter_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END

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
 CONSTRAINT [PK_tblCounters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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



IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteAllocateCounterService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteAllocateCounterService]
END
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



IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteAllocateCounterServiceByCounterId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteAllocateCounterServiceByCounterId]
END
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


IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteAllocateCounterServiceByServiceId' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteAllocateCounterServiceByServiceId]
END
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

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteBranch' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteBranch]
END
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



IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteCounter' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteCounter]
END
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


IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_deleteService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_deleteService]
END
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



IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_insertAllocateCounterService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_insertAllocateCounterService]
END
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

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_insertBranch' )
BEGIN
    DROP PROCEDURE [dbo].[sp_insertBranch]
END
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
begin
IF (EXISTS (SELECT * FROM tblBranches inner join tblBanks on tblBranches.bankId = tblBanks.id where tblBranches.id = @branchId and tblBanks.id = @bankId))
insert into tblCounters OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@type,@branchId)
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
/****** Object:  StoredProcedure [dbo].[sp_insertService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_insertService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_insertService]
END
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

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_selectBranchById' )
BEGIN
    DROP PROCEDURE [dbo].[sp_selectBranchById]
END
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
/****** Object:  StoredProcedure [dbo].[sp_updateBranch]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_updateBranch' )
BEGIN
    DROP PROCEDURE [dbo].[sp_updateBranch]
END
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
IF (EXISTS (SELECT * FROM tblBranches where id = @id))
begin
IF (EXISTS (SELECT * FROM tblBranches where bankId = @bankId and id = @id))
update tblBranches set enName = @enName,arName = @arName,active = @active OUTPUT INSERTED.IDENTITYCOL where id = @id;
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
/****** Object:  StoredProcedure [dbo].[sp_updateCounter]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_updateCounter' )
BEGIN
    DROP PROCEDURE [dbo].[sp_updateCounter]
END
GO
CREATE proc [dbo].[sp_updateCounter]
@id int,
@enName nvarchar(100),
@arName nvarchar(100),
@active bit,
@type nvarchar(100),
@branchId int,
@bankId int
as
begin
BEGIN TRY 
IF (EXISTS (SELECT * FROM tblCounters where id = @id))
begin
IF (EXISTS (SELECT * FROM tblBranches inner join tblBanks on tblBranches.bankId = tblBanks.id where tblBranches.id = @branchId and tblBanks.id = @bankId))
update tblCounters set enName = @enName,arName = @arName,active = @active,type = @type OUTPUT INSERTED.IDENTITYCOL where id = @id
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
/****** Object:  StoredProcedure [dbo].[sp_updateService]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'sp_updateService' )
BEGIN
    DROP PROCEDURE [dbo].[sp_updateService]
END
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
IF (EXISTS (SELECT * FROM tblService where id = @id))
begin
IF (EXISTS (SELECT * FROM tblService where bankId = @bankId and id = @id))
update tblservice set enName = @enName,arName = @arName,active = @active,maxNumOfTickets = @maxNumOfTickets OUTPUT INSERTED.IDENTITYCOL where id = @id
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
/****** Object:  StoredProcedure [dbo].[usp_GetErrorInfo]    Script Date: 09/02/2021 11:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS ( SELECT * FROM   sysobjects WHERE name = N'usp_GetErrorInfo' )
BEGIN
    DROP PROCEDURE [dbo].[usp_GetErrorInfo]
END
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
ALTER DATABASE [TSDApp2] SET  READ_WRITE 
GO
