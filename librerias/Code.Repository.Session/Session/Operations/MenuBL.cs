

using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.Entity;
using Code.Repository.Session.Model.menu;
using System.Collections.Generic;
using Code.Repository.Session.Mapper;

namespace Code.Repository.Session.Operations
{
    public class MenuBL
    {
        public IEnumerable<MenuDTO> GetMenu(int usuario, string codigo = "")
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Dictionary<string, object> _parametros = new Dictionary<string, object>();

            _parametros.Add("@mencodigo", codigo);
            _parametros.Add("@usuario", usuario);


            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "menu.[ObtenerMenu]",
                parametros = _parametros
            });

            return result.MapToDTO();

        }


        public void InsertNavegacion(int menu, int usuario, string ip)
        {

            if (ip == "::1") ip = "localhost";

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            objcnn.navegacion.Add(new Navegacion()
            {
                NavFecha = System.DateTime.Now,
                NavId = 0,
                NavPagina = menu,
                NavUsu = usuario,
                NavIP = ip
            });

            objcnn.SaveChanges();

        }
    }
}
