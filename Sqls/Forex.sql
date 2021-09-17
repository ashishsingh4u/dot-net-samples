WITH    [Abc]
          AS ( SELECT   x.QuoteCcy AS BaseCcy ,
                        y.QuoteCcy AS QuoteCcy ,
                        dbo.GetForexRate(x.QuoteCcy, y.QuoteCcy) AS Rate ,
                        y.Category AS Category
               FROM     Forex x ,
                        Forex y
               WHERE    x.QuoteCcy <> y.QuoteCcy
               UNION ALL
               SELECT   BaseCcy ,
                        QuoteCcy ,
                        Rate ,
                        Category
               FROM     Forex
               WHERE    BaseCcy <> QuoteCcy
             )
    SELECT  *
    FROM    Abc
    ORDER BY BaseCcy