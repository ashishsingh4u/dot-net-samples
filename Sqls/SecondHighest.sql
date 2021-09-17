SELECT TOP 1
        *
FROM    Student
WHERE   Roll IN ( SELECT TOP 2
                            roll
                  FROM      Student
                  ORDER BY  Roll DESC )
ORDER BY Roll
SELECT  *
FROM    Student
ORDER BY Roll DESC