sp_configure 'clr enabled', 1 
GO
RECONFIGURE
GO
ALTER DATABASE Ashu SET TRUSTWORTHY ON
GO

DROP PROCEDURE senddata
GO
DROP FUNCTION Intialize
GO
DROP FUNCTION UnIntialize
GO
DROP ASSEMBLY SocketManager
GO

CREATE ASSEMBLY SocketManager 
FROM 'C:\Projects\Main\MySample\SocketManager\bin\Release\SocketManager.dll'
WITH PERMISSION_SET = UNSAFE ;
GO

GO
CREATE FUNCTION Intialize
    (
      @bindingAddress NVARCHAR(4000) ,
      @logPath NVARCHAR(4000)
    )
RETURNS NVARCHAR(4000)
AS EXTERNAL NAME 
    SocketManager.SqlPush.Intialize
GO
CREATE FUNCTION UnIntialize ( )
RETURNS NVARCHAR(4000)
AS EXTERNAL NAME 
    SocketManager.SqlPush.UnIntialize
GO
CREATE PROCEDURE SendData ( @data NVARCHAR(4000) )
AS EXTERNAL NAME 
    SocketManager.SqlPush.SendData
GO

SELECT  dbo.Intialize('127.0.0.1:8085',
                      'C:\Projects\SocketDemo\SocketManager\log.txt')
EXEC dbo.senddata 'Hel43534lf'
SELECT  dbo.UnIntialize()