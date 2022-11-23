SET XACT_ABORT ON;
IF OBJECT_ID('[menu].[AuditPaginasVisitadas]',N'P') IS NOT NULL BEGIN
	DROP PROCEDURE [menu].[AuditPaginasVisitadas]
END 
GO
-- ================================================
-- Template generated from Template Explorer using:

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [menu].[AuditPaginasVisitadas]	
	@proveedor varchar(90) ='',
	@fechai varchar(90)='',	
	@fechaf varchar(90)='',
	@usuario varchar(90)='',
	@menu int = 0,

	
	@xPagina int = 0,
	@xUsuario int = 0,
	@xFecha int = 0,
	@xHora int = 0,
	

	@rows varchar(90)='50',
	@page varchar(90)='1'
AS
BEGIN

set dateformat dmy

Declare @sql nvarchar (4000), @where nvarchar (1000), @and nvarchar(6)

create table #visitas (pagina varchar(100), usuario varchar(150), visitas int, fecha varchar(50),
						hora varchar(20), orden tinyint default(0), totalRegistros int)

declare @select nvarchar(1000), @insertar nvarchar(1000), @Orden nvarchar(1000) = '1'

set @select = ''
set @insertar = ''


if @xUsuario='1'
BEGIN
	set @select += 'usuario.UserNombre ,'
	set @insertar += 'usuario, '
	
END

if @xPagina='1'
BEGIN
	set @select +=  'Descripcion ,'
	set @insertar += 'pagina, '
END
if @xFecha='1'
BEGIN
	set @select = @select + ' CONVERT(varchar(10), Navegacion.NavFecha, 103) ,'
	set @insertar = @insertar + ' fecha,'
	set @Orden = '  CONVERT(varchar(10), Navegacion.NavFecha, 103) DESC, '
END

if @xHora='1'
BEGIN
	set @select = @select + ' CONVERT(varchar(8), Navegacion.NavFecha, 108) ,'
	set @insertar = @insertar + ' hora,'
	IF (@Orden = '1')
	BEGIN
		SET @Orden = '  CONVERT(varchar(8), Navegacion.NavFecha, 108)  DESC, '
	END
	ELSE BEGIN
		SET @Orden += '  CONVERT(varchar(8), Navegacion.NavFecha, 108)  DESC, '
	END
	
END

set @sql = '  
	INSERT INTO #visitas (' + @insertar + 'visitas, totalRegistros)
	SELECT ' + @select + 'COUNT(*) AS visitas, COUNT(*) OVER (PARTITION BY '''') as totalRegistros
	FROM   menu.Navegacion 
		   INNER JOIN Usuario ON NavUsu =UserId 
	       INNER JOIN menu.Menu ON NavPagina = IdMenu '

set @where = ''

set @and = ''



IF @usuario <> '-1' and isnumeric(@usuario)= 1
BEGIN
	set @where = @where + @and + '  Usuario.UserId = ' + @usuario 
	set @and = ' and '
END

IF (@proveedor > 0 )
BEGIN
	set @where = @where + @and + ' Usuario.UserIdPpal = ' + @proveedor 
	set @and = ' and '
END

IF ISDATE(@fechai)=1 AND ISDATE (@fechaf)=1 
	BEGIN           
		SET @SQL = @SQL + +' AND NavFecha BETWEEN '+CHAR(39)+ @fechai +CHAR(39)+' AND '+CHAR(39)+ @fechaf +CHAR(39)		
	END        
	ELSE  IF @fechai NOT IN('','-1') AND ISDATE (@fechai)=1 
	BEGIN              
		SET @SQL = @SQL + +' AND NavFecha >=' +CHAR(39)+ @fechai +CHAR(39)           		
	END                
	ELSE IF @fechaf NOT IN('','-1') AND ISDATE (@fechaf)=1 
	BEGIN              
		SET @SQL = @SQL + +' AND NavFecha <='+CHAR(39)+ @fechaf +CHAR(39)        		
	END 

IF @menu>0
BEGIN
	set @where = @where + @and +' NavPagina IN (' + CAST(@menu as varchar(100)) + ')'
	set @and = ' and '
END


IF @xPagina = '1' or @xUsuario = '1' or @xFecha = '1' or @xHora = '1'  
BEGIN
	DECLARE @x nvarchar (500) 
	SET @x = left(@select, len(@select)-1)
END


IF @where <> ''
BEGIN
	SET @where = ' WHERE ' + @where 
END

	
IF @xPagina <> '1' and @xUsuario <> '1' and @xFecha <> '1' and @xHora <> '1' 
BEGIN
	set @sql = @sql + @where
END
ELSE
BEGIN
	SET @sql = @sql + @where  + ' GROUP BY ' + @x
END

if(@xFecha <> '' OR  @xHora <> '')
BEGIN
	SET @Orden = left(@Orden, len(@Orden)-1)
END

--print(@sqlCount)
--exec(@sqlCount)
DECLARE @TotalRegistros INT 
--set @TotalRegistros = exec(@sqlCount)
SET @sql += 'ORDER BY '+@Orden+' OFFSET('+@rows+' - 1) * '+@page+' ROWS
			 FETCH NEXT '+@page+' ROWS ONLY '

PRINT @sql
EXEC ( @sql)


INSERT INTO #visitas(visitas, orden, totalRegistros)
SELECT  sum(visitas), 1,  2
FROM #visitas

select *  from #visitas 

END
