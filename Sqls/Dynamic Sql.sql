CREATE PROCEDURE StudentRCUD
    @Function CHAR(1) = 'R' ,
    @Name VARCHAR(50) = NULL ,
    @Roll INT = NULL ,
    @City VARCHAR(50) = NULL
AS 
    IF @Function = 'R' 
        BEGIN
            SELECT  Name ,
                    Roll ,
                    City
            FROM    Student
            WHERE   ( ISNULL(@Name, '') = ''
                      OR Name = @Name
                    )
                    AND ( ISNULL(@Roll, '') = ''
                          OR Roll = @Roll
                        )
                    AND ( ISNULL(@City, '') = ''
                          OR City = @City
                        )
        END

--sp_executesql N'StudentRCUD @Name=@param1',N'@param1 varchar(50)',@param1=Ashish