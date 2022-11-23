using Code.Repository.DAO.Context;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Informe;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.inf;
using Code.Repository.Model.Mapper.Informe;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations
{
    public class InformeBL
    {

        string concatenarUrl(Informe inf, List<ParametrosInforme> parametros, int rows, int page, int oc, int estado)
        {
            string _url = string.Empty;
            for (int i = 0; i < parametros.Count; i++)
            {
                ParametrosInforme _parametro = parametros[i];
                _url += $"{(i == 0 ? "" : "&")}{_parametro.ParamTexto}={_parametro.ParamValorDefault}";
            }
            //Validar funcionamiento dinamico
            _url += $"&noOc={oc}";
            _url += $"&estado={estado}";
            // ======================================


            if (inf.InfPaginacion)
            {
                _url += $"&Rows={rows}&Page={page}";
            }

            return _url;
        }


        public async Task<InformeDTO> GetDataInforme(int constructora, int id, int rows, int page, int oc, int estado)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            InformeDTO objLst = new InformeDTO();

            var _inf = objcnn.informe.Find(id);

            objLst.encabezado = (from d in objcnn.columnasInforme
                                 where d.ColIdInforme == id
                                 select d.MapToDTO());


            var _parametros = objcnn.parametrosInforme.Where(c => c.ParamIdInforme == id).ToList();

            var _api = new ConexionERP(constructora);
            var token = await _api.ObtenerToken();
            var Const_ = _api.ObtenerConstructora();
            string _url = $"{Const_.ConstRuta_API}/{_inf.infAPI}?{concatenarUrl(_inf, _parametros, rows, page, oc, estado)}";

            try
            {



                objLst.detalles = (await _api.Peticion(_url, HttpMethod.Get, null, token.ToString())); ;


            }
            catch (System.Exception e)
            {

                throw;
            }


            return objLst;
        }


        public IEnumerable<PaginaDTO> GetPaginas(string filter = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IEnumerable<Menu> expresion = new List<Menu>();
            IEnumerable<PaginaDTO> expresion2 = new List<PaginaDTO>();

            expresion = (from _menus in objcnn.menu
                                            where _menus.Descripcion.Contains(filter)
                                            select _menus);

            return expresion.MapToDTO();
        }
        public IEnumerable<AuditoriaLoginDTO> GetInformeAuditoriaLogin(int proveedor, string fechai = "", string fechaf = "", int usuario = 0, int rows = 0, int page = 0)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Dictionary<string, object> _parametros = new Dictionary<string, object>();

            _parametros.Add("@proveedor", proveedor);
            _parametros.Add("@fechai", fechai);
            _parametros.Add("@fechaf", fechaf);            
            _parametros.Add("@usuario", usuario);
            _parametros.Add("@rows", rows);
            _parametros.Add("@page", page);


            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "menu.[AuditoriaLogin]",
                parametros = _parametros
            });

            return result.MapToDTO();

        }

        public IEnumerable<PaginasVisitadasDTO> GetInfPaginasVisitadas(string fechaInicial = "", string fechaFinal = "", string usuario = "-1", int idMenu =0, int rows = 0, int page = 0,
                                                                        int xUsuario = 0, int xFecha = 0, int xHora = 0, int xPagina = 0, int proveedor = 0)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Dictionary<string, object> _parametros = new Dictionary<string, object>();

            _parametros.Add("@proveedor", proveedor);

            _parametros.Add("@fechai", fechaInicial);
            _parametros.Add("@fechaf", fechaFinal);
            _parametros.Add("@usuario", usuario);
            _parametros.Add("@menu", idMenu);

            _parametros.Add("@xUsuario", xUsuario);
            _parametros.Add("@xFecha", xFecha);
            _parametros.Add("@xHora", xHora);
            _parametros.Add("@xPagina", xPagina);

            _parametros.Add("@rows", rows);
            _parametros.Add("@page", page);


            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "[menu].[AuditPaginasVisitadas]",
                parametros = _parametros
            });

            return result.MapToDTOPaginasVisitas();

        }
    }
}
