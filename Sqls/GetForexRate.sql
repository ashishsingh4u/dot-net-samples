-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetForexRate
    (
      -- Add the parameters for the function here
      @BaseCcy CHAR(3) ,
      @QuoteCcy CHAR(3)
    )
RETURNS DECIMAL(18, 5)
AS 
    BEGIN
	-- Declare the return variable here
        DECLARE @RetVal DECIMAL(18, 5) ,
            @BaseCcyRate DECIMAL(18, 5) ,
            @QuoteCcyRate DECIMAL(18, 5)
	
	-- Add the T-SQL statements to compute the return value here
        SELECT  @BaseCcyRate = Rate
        FROM    Forex
        WHERE   QuoteCcy = @BaseCcy
        SELECT  @QuoteCcyRate = Rate
        FROM    Forex
        WHERE   QuoteCcy = @QuoteCcy
        SET @RetVal = @QuoteCcyRate / @BaseCcyRate
        
	-- Return the result of the function
        RETURN @RetVal

    END
GO

