BEGIN TRANSACTION;  
  
BEGIN TRY  
    -- Generate a constraint violation error.  
USE [TSDApp]
/****** Object:  Table [dbo].[tblAllocateCounterService]    Script Date: 03/02/2021 09:33:08 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[tblAllocateCounterService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[counterId] [int] NULL,
	[serviceId] [int] NULL,
 CONSTRAINT [PK_tblAllocate_Counter_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[tblBranches]    Script Date: 03/02/2021 09:33:08 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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

/****** Object:  Table [dbo].[tblCounters]    Script Date: 03/02/2021 09:33:08 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

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

ALTER TABLE [dbo].[tblService]
ADD arName [nvarchar](max), bankId [int], active [bit], maxNumOfTickets [int];

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

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

ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocate_Counter_Service_tblCounters] FOREIGN KEY([counterId])
REFERENCES [dbo].[tblCounters] ([id])

ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocate_Counter_Service_tblCounters]

ALTER TABLE [dbo].[tblAllocateCounterService]  WITH CHECK ADD  CONSTRAINT [FK_tblAllocate_Counter_Service_tblService] FOREIGN KEY([serviceId])
REFERENCES [dbo].[tblService] ([id])

ALTER TABLE [dbo].[tblAllocateCounterService] CHECK CONSTRAINT [FK_tblAllocate_Counter_Service_tblService]

ALTER TABLE [dbo].[tblBranches]  WITH CHECK ADD  CONSTRAINT [FK_tblBranches_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])

ALTER TABLE [dbo].[tblBranches] CHECK CONSTRAINT [FK_tblBranches_tblBanks]

ALTER TABLE [dbo].[tblCounters]  WITH CHECK ADD  CONSTRAINT [FK_tblCounters_tblBranches] FOREIGN KEY([branchId])
REFERENCES [dbo].[tblBranches] ([id])

ALTER TABLE [dbo].[tblCounters] CHECK CONSTRAINT [FK_tblCounters_tblBranches]

ALTER TABLE [dbo].[tblService]  WITH CHECK ADD  CONSTRAINT [FK_tblService_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])

ALTER TABLE [dbo].[tblService] CHECK CONSTRAINT [FK_tblService_tblBanks]

ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblBanks] FOREIGN KEY([bankId])
REFERENCES [dbo].[tblBanks] ([id])

ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblBanks]

/****** Object:  StoredProcedure [dbo].[sp_Delete_Allocate_Counter]    Script Date: 03/02/2021 09:33:09 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

exec sp_rename 'dbo.tblService.name', 'enName', 'COLUMN';

exec('CREATE proc [dbo].[sp_Delete_Allocate_Counter]
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
END')

exec('CREATE proc [dbo].[sp_deleteAllocateCounterService]
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
END')

exec('CREATE proc [dbo].[sp_deleteAllocateCounterServiceByCounterId]
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
END')


exec('CREATE proc [dbo].[sp_deleteAllocateCounterServiceByServiceId]
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
END')



exec('CREATE proc [dbo].[sp_deleteBranch]
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
END')



exec('CREATE proc [dbo].[sp_deleteCounter]
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
END')



exec('CREATE proc [dbo].[sp_deleteService]
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
END')



exec('CREATE proc [dbo].[sp_insertAllocateCounterService]
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
END')



exec('CREATE proc [dbo].[sp_insertBranch]
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
END')



exec('CREATE proc [dbo].[sp_insertCounter]
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
END')



exec('CREATE proc [dbo].[sp_insertService]
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
END')



exec('CREATE proc [dbo].[sp_selectAllocateCounterService]
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
END')



exec('CREATE proc [dbo].[sp_selectBranchById]
@branchId int
as
begin
IF (EXISTS (SELECT * FROM tblBranches))
BEGIN
SELECT * FROM tblBranches where id = @branchId
END
end')



exec('CREATE proc [dbo].[sp_selectBranchesByBankId]
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
END')



exec('CREATE proc [dbo].[sp_selectCountersByBranchId]
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
END')



exec('CREATE proc [dbo].[sp_selectServicesByBankId]
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
END')



exec('CREATE proc [dbo].[sp_updateBranch]
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
END')



exec('CREATE proc [dbo].[sp_updateCounter]
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
END')



exec('CREATE proc [dbo].[sp_updateService]
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
END')



exec('CREATE PROCEDURE [dbo].[usp_GetErrorInfo]  
AS  
SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_SEVERITY() AS ErrorSeverity  
    ,ERROR_STATE() AS ErrorState  
    ,ERROR_PROCEDURE() AS ErrorProcedure  
    ,ERROR_LINE() AS ErrorLine  
    ,ERROR_MESSAGE() AS ErrorMessage;')

USE [TSDApp]


END TRY  
BEGIN CATCH  
    SELECT   
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;  
  
    IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION;  
END CATCH;  
  
IF @@TRANCOUNT > 0  
    COMMIT TRANSACTION;  
  