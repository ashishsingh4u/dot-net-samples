sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO
ALTER DATABASE Ashu SET TRUSTWORTHY ON
GO

drop procedure senddata
GO
drop function Intialize
GO
drop function UnIntialize
GO
drop Assembly SocketManager
GO

CREATE ASSEMBLY SocketManager 
FROM 'C:\Projects\Main\MySample\SocketManager\bin\Release\SocketManager.dll'
WITH PERMISSION_SET = Unsafe;
GO

GO
CREATE FUNCTION Intialize ( @bindingAddress NVARCHAR(4000),@logPath nvarchar(4000) ) 
RETURNS nvarchar(4000)
AS EXTERNAL NAME 
SocketManager.SqlPush.Intialize
GO
CREATE FUNCTION UnIntialize () 
RETURNS nvarchar(4000)
AS EXTERNAL NAME 
SocketManager.SqlPush.UnIntialize
GO
CREATE Procedure SendData (@data nvarchar(4000)) 
AS EXTERNAL NAME 
SocketManager.SqlPush.SendData
GO

select dbo.Intialize ('127.0.0.1:8085','C:\Projects\SocketDemo\SocketManager\log.txt')
exec dbo.senddata 'Hel43534lf'
select dbo.UnIntialize()