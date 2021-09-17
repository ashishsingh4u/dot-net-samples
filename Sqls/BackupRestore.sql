BACKUP DATABASE Ashu TO DISK = 'C:\Projects\Sqls\Ashu.bak'
WITH
INIT

RESTORE DATABASE Ashu FROM DISK = 'C:\Projects\Sqls\Ashu.bak'
WITH
FILE=1,
NOUNLOAD,
REPLACE,
STATS=10


DECLARE @SQL VARCHAR(MAX)
SET @SQL = ''
SELECT  @SQL = @SQL + 'Kill ' + CONVERT(VARCHAR, SPId) + ';'
FROM    MASTER..SysProcesses
WHERE   DBId = DB_ID('Ashu')
        AND SPId <> @@SPId
SELECT  @SQL

--sp_executesql N'SELECT spid
--FROM MASTER..SysProcesses
--WHERE DBId = 7 AND SPId <> @@SPId'


DECLARE @DatabaseName NVARCHAR(50)
DECLARE @SPId INT

SET @DatabaseName = N'Ashu'
--SET @DatabaseName = DB_NAME()
DECLARE my_cursor CURSOR FAST_FORWARD
FOR
    SELECT  SPId
    FROM    MASTER..SysProcesses
    WHERE   DBId = DB_ID(@DatabaseName)
            AND SPId <> @@SPId

OPEN my_cursor 

FETCH NEXT FROM my_cursor INTO @SPId

WHILE @@FETCH_STATUS = 0 
    BEGIN
        PRINT @SPId
        EXEC ('KILL ' + @SPId )
        FETCH NEXT FROM my_cursor INTO @SPId
    END

CLOSE my_cursor 
DEALLOCATE my_cursor 







CREATE PROCEDURE KillUserProcess @user NVARCHAR(128)
AS --Kills one process from a certain user, assuming only one process is active

    DECLARE @spid INT

    SELECT  @spid = spid
    FROM    master.dbo.sysprocesses
    WHERE   RTRIM(loginame) = @user

    EXEC ('KILL '+ @spid)



    CREATE PROCEDURE general_select1
        @tblname SYSNAME ,
        @key VARCHAR(10)
    AS 
        DECLARE @sql NVARCHAR(4000)
        SELECT  @sql = ' SELECT col1, col2, col3 ' + ' FROM dbo.'
                + QUOTENAME(@tblname) + ' WHERE keycol = @key'
        EXEC sp_executesql @sql, N'@key varchar(10)', @key
        CREATE PROCEDURE general_select2
            @tblname NVARCHAR(127) ,
            @key VARCHAR(10)
        AS 
            EXEC('SELECT col1, col2, col3
            FROM ' + @tblname + '
            WHERE keycol = ''' + @key + '''')
      
            SELECT  'SELECT col1, col2, col3
      FROM ' + 'abc' + '
      WHERE keycol = ''' + 'def' + ''''


