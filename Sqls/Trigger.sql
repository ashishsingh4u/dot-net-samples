SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('FriendInfoInsertTrigger', 'TR') IS NOT NULL 
    BEGIN
        PRINT 'DROPPING TRIGGER FriendInfoInsertTrigger'
        DROP TRIGGER FriendInfoInsertTrigger
    END
GO
PRINT 'CREATING TRIGGER FriendInfoInsertTrigger'
GO
CREATE TRIGGER FriendInfoInsertTrigger ON FriendInfo
    AFTER INSERT
AS
    BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
        SET NOCOUNT ON ;

        INSERT  INTO FriendInfoTrace
                SELECT  NEWID() ,
                        *
                FROM    inserted

    END
GO

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('FriendInfoUpdateTrigger', 'TR') IS NOT NULL 
    BEGIN
        PRINT 'DROPPING TRIGGER FriendInfoUpdateTrigger'
        DROP TRIGGER FriendInfoUpdateTrigger
    END
GO
GO
PRINT 'CREATING TRIGGER FriendInfoUpdateTrigger'
GO
CREATE TRIGGER FriendInfoUpdateTrigger ON FriendInfo
    AFTER UPDATE
AS
    BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
        SET NOCOUNT ON ;

        INSERT  INTO FriendInfoTrace
                SELECT  NEWID() ,
                        *
                FROM    inserted

    END
GO

-------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('FriendInfoDeleteTrigger', 'TR') IS NOT NULL 
    BEGIN
        PRINT 'DROPPING TRIGGER FriendInfoDeleteTrigger'
        DROP TRIGGER FriendInfoDeleteTrigger
    END
GO
GO
PRINT 'CREATING TRIGGER FriendInfoDeleteTrigger'
GO
CREATE TRIGGER FriendInfoDeleteTrigger ON FriendInfo
    AFTER DELETE
AS
    BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
        SET NOCOUNT ON ;

        INSERT  INTO FriendInfoTrace
                SELECT  NEWID() ,
                        *
                FROM    deleted

    END
GO
