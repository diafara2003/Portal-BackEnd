SET XACT_ABORT ON;
IF OBJECT_ID('dbo.EspecialidadesAll ',N'P') IS NOT NULL BEGIN
	DROP PROCEDURE dbo.EspecialidadesAll 
END 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.EspecialidadesAll 

AS
BEGIN


create table #TblEspecialidadesTercero
(grupo  int default(0), categoria int default(0), especialidad int default(0),texto varchar(500) default(''))


--grupos
insert into #TblEspecialidadesTercero
		(grupo,texto)
select	GruId,GruTexto
from	GruposTercero

--categorias
insert into #TblEspecialidadesTercero
		(categoria,texto,grupo)
select	CatId,CatTexto,CatIdGrupo
from	CategoriasTercero


--especialidades
insert into #TblEspecialidadesTercero
		(especialidad,texto,categoria,grupo)
select	EspId,EspTexto,EspIdCategoria,EspIdGrupo
from	EspecialidadesTercero



select grupo,categoria,especialidad,texto from #TblEspecialidadesTercero
END
GO
