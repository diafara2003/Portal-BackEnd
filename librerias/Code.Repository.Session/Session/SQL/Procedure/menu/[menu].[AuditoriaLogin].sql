SET XACT_ABORT ON;
IF OBJECT_ID('[menu].[AuditoriaLogin]',N'P') IS NOT NULL BEGIN
	DROP PROCEDURE [menu].[AuditoriaLogin]
END 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [menu].[AuditoriaLogin]
	@proveedor varchar(90)='',
	@fechai varchar(90)='',
	@fechaf varchar(90)='',
	@usuario varchar(90)='',

	@rows varchar(90)='50',
	@page varchar(90)='1'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE  @sql varchar(4000)


	SET @SQL =
	'
	SELECT  UserNombre,UserDoc,UserCorreo,
			COUNT(UserNombre)OVER (PARTITION BY UserNombre) as countLogin,
			CONVERT(VARCHAR(15),fecha,103) + '' - ''+CONVERT(VARCHAR(8),fecha,114) fecha,
			COUNT(1) over (partition by '''')as countTotal
	FROM	[dbo].[AuditoriaLogin] WITH(NOLOCK)
			INNER JOIN dbo.usuario WITH(NOLOCK) on UserId=usuario
	WHERE	tercero='+@proveedor


	IF ISDATE(@fechai)=1 AND ISDATE (@fechaf)=1 BEGIN           
		SET @SQL = @SQL + +' AND fecha BETWEEN '+CHAR(39)+ @fechai +CHAR(39)+' AND '+CHAR(39)+ @fechaf +CHAR(39)
	END        
	ELSE  IF @fechai NOT IN('','-1') AND ISDATE (@fechai)=1 BEGIN              
		SET @SQL = @SQL + +' AND fecha >=' +CHAR(39)+ @fechai +CHAR(39)           
	END                
	ELSE IF @fechaf NOT IN('','-1') AND ISDATE (@fechaf)=1 BEGIN              
		SET @SQL = @SQL + +' AND fecha <='+CHAR(39)+ @fechaf +CHAR(39)        
	END 


	SET @sql +='
	GROUP BY UserNombre,UserDoc,UserCorreo,fecha
	ORDER BY fecha DESC	OFFSET('+@rows+' - 1) * '+@page+' ROWS
			FETCH NEXT '+@page+' ROWS ONLY '


	PRINT(@sql)
	EXEC (@sql)



END

