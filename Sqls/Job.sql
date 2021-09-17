USE [msdb]
GO

/****** Object:  Job [MoveToHistory]    Script Date: 09/18/2010 23:30:01 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT  @ReturnCode = 0
/****** Object:  JobCategory [Data Collector]    Script Date: 09/18/2010 23:30:01 ******/
IF NOT EXISTS ( SELECT  name
                FROM    msdb.dbo.syscategories
                WHERE   name = N'Data Collector'
                        AND category_class = 1 ) 
    BEGIN
        EXEC @ReturnCode = msdb.dbo.sp_add_category @class = N'JOB',
            @type = N'LOCAL', @name = N'Data Collector'
        IF ( @@ERROR <> 0
             OR @ReturnCode <> 0
           ) 
            GOTO QuitWithRollback

    END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode = msdb.dbo.sp_add_job @job_name = N'MoveToHistory',
    @enabled = 1, @notify_level_eventlog = 0, @notify_level_email = 0,
    @notify_level_netsend = 0, @notify_level_page = 0, @delete_level = 0,
    @description = N'No description available.',
    @category_name = N'Data Collector', @owner_login_name = N'sa',
    @job_id = @jobId OUTPUT
IF ( @@ERROR <> 0
     OR @ReturnCode <> 0
   ) 
    GOTO QuitWithRollback
/****** Object:  Step [MoveTrace]    Script Date: 09/18/2010 23:30:02 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id = @jobId,
    @step_name = N'MoveTrace', @step_id = 1, @cmdexec_success_code = 0,
    @on_success_action = 1, @on_success_step_id = 0, @on_fail_action = 2,
    @on_fail_step_id = 0, @retry_attempts = 0, @retry_interval = 0,
    @os_run_priority = 0, @subsystem = N'TSQL', @command = N'Use Ashu 
		Go
Delete from FriendInfoHistory 
Go
Insert into FriendInfoHistory 
Select * from FriendInfoTrace 
Go 
Delete from FriendInfoTrace 
Go', @database_name = N'Ashu', @flags = 0
IF ( @@ERROR <> 0
     OR @ReturnCode <> 0
   ) 
    GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF ( @@ERROR <> 0
     OR @ReturnCode <> 0
   ) 
    GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id = @jobId,
    @name = N'CollectorSchedule_Every_10min', @enabled = 1, @freq_type = 4,
    @freq_interval = 1, @freq_subday_type = 4, @freq_subday_interval = 10,
    @freq_relative_interval = 0, @freq_recurrence_factor = 0,
    @active_start_date = 20100402, @active_end_date = 99991231,
    @active_start_time = 0, @active_end_time = 235959,
    @schedule_uid = N'41a6d121-6b6c-4899-98de-acf704d14f5d'
IF ( @@ERROR <> 0
     OR @ReturnCode <> 0
   ) 
    GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId,
    @server_name = N'(local)'
IF ( @@ERROR <> 0
     OR @ReturnCode <> 0
   ) 
    GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
IF ( @@TRANCOUNT > 0 ) 
    ROLLBACK TRANSACTION
EndSave:

GO


