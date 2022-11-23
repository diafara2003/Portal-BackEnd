SET XACT_ABORT ON;
IF OBJECT_ID('[menu].[ObtenerMenu]',N'P') IS NOT NULL BEGIN
	DROP PROCEDURE [menu].[ObtenerMenu]
END 
GO
-- ================================================
-- Template generated from Template Explorer using:

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [menu].[ObtenerMenu]
	@mencodigo varchar(20)='',@usuario int
AS
BEGIN
	
	-- OBTIENE LOS MENUS POR ACCESO DE USUARIO
	CREATE TABLE #AccesosMenu(MenuId INT)

	INSERT	INTO #AccesosMenu
	SELECT	IdMenu
	FROM	dbo.UsuariosNivel INNER JOIN 
			dbo.Acceso ON idNivel = UsuNivel
	WHERE	UsuId = @usuario

	
	-- SI NO TIENE ACCESO A MENUS BUSCA LOS MENUS ASIGNADOS A OTROS USUARIOS POR TIPO ( Constructora o proveedor)
	IF (SELECT COUNT(1) FROM #AccesosMenu) = 0 BEGIN 
	
		DECLARE @Tipo VARCHAR(2) =  (SELECT UserTipo FROM dbo.Usuario WHERE UserId = @usuario)

		INSERT	INTO #AccesosMenu
		SELECT  Acceso.IdMenu
		FROM	Usuario INNER JOIN 
				UsuariosNivel ON UsuId = UserId INNER JOIN 
				Acceso ON idNivel = UsuNivel
		WHERE	UserTipo = @Tipo 
		GROUP BY Acceso.IdMenu
		
	END 

	CREATE TABLE #menu(
		IdMenu int,Mencodigo varchar(100),Descripcion varchar(500),
		Ubicacion varchar(500),ActMenu bit,PagAyuda  varchar(500),
		SVG varchar(2000),requiereProyecto bit default(0),
		TieneHijos int default(0)
	)

	if(@mencodigo='')begin
		INSERT INTO #menu
				(IdMenu,Mencodigo,Descripcion,Ubicacion,ActMenu,PagAyuda,SVG,requiereProyecto)
		SELECT	IdMenu,Mencodigo,Descripcion,Ubicacion,ActMenu,PagAyuda,SVG,MenRequiereProyecto
		FROM	#AccesosMenu INNER JOIN 
				MENU.Menu ON IdMenu  = MenuId 
		WHERE	CHARINDEX('.',Mencodigo)=0 AND ActMenu=1  
	end
	ELSE BEGIN
		INSERT INTO #menu
				(IdMenu,Mencodigo,Descripcion,Ubicacion,ActMenu,PagAyuda,SVG,requiereProyecto)
		SELECT	IdMenu,Mencodigo,Descripcion,Ubicacion,ActMenu,PagAyuda,SVG,MenRequiereProyecto
		FROM	#AccesosMenu INNER JOIN 
				MENU.Menu ON IdMenu  = MenuId 
		WHERE	Mencodigo like @mencodigo+'.%'
	END
	 
	/*se valida si el menu tiene hijos*/
	UPDATE	#menu
	SET		TieneHijos=1
	FROM	#menu
			INNER JOIN (
				SELECT	MENCODIGO
				FROM	MENU.MENU
				WHERE	ActMenu=1 AND CHARINDEX('.',Mencodigo)>0
				GROUP BY MENCODIGO			
						) AS X ON X.Mencodigo LIKE #menu.Mencodigo+'.%'


	SELECT	IdMenu,Mencodigo,Descripcion,Ubicacion,
			ActMenu,PagAyuda,SVG,TieneHijos,requiereProyecto
	FROM	#menu
	GROUP	BY IdMenu,Mencodigo,Descripcion,Ubicacion,ActMenu,PagAyuda,SVG,TieneHijos,requiereProyecto
	order by cast('/'+Mencodigo+'/' as hierarchyid)

END
GO
