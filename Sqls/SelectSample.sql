DECLARE @data VARCHAR(MAX)
SET @data = ''
SELECT  @data = @data + name + '|'
FROM    Student
SELECT  SUBSTRING(@data, 0, LEN(@data))
