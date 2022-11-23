using Code.Repository.DAO.Context;
using Code.Repository.Document.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.Entity;
using static Code.Repository.EntityFramework.Context.ApplicationDatabaseContext;

namespace Code.Repository.Document.Operations
{


    public class DocumentosERP
    {

        public async Task<IEnumerable<DocumentosRequeridosERPDTO>> GetDocumentosRequeridosconstructora(int constructora, int tercero)
        {

            List<DocumentosRequeridosERPDTO> objRequeridos = new List<DocumentosRequeridosERPDTO>();
            var _api = new ConexionERP(constructora);
            var token = await _api.ObtenerToken();
            var Const_ = _api.ObtenerConstructora();
            string _url = $"{Const_.ConstRuta_API}/adpro/api/PortalProveedorERP/documentos/requeridos";

            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                string response = await _api.Peticion(_url, HttpMethod.Get, null, token.ToString());


                if(response==null)
                    return new List<DocumentosRequeridosERPDTO>() {
                new DocumentosRequeridosERPDTO(){
                nombreEspecialidad="Error al consumir api del ERP",
                }
                };

                List<EspecialidadesTercero> lstEspecialidadTercero = (from e in objcnn.especialidadTercero
                                                                      join et in objcnn.terEspecialidad on e.EspId equals et.EspId
                                                                      where et.TerId == tercero
                                                                      select e).ToList();


                var objEspecialidades = new List<EspecialidadesTercero>();

                JArray.Parse(response).ToList().ForEach(x =>
                {
                    var _myObject = x.ToObject<ResponseERPDocReqDTO>();
                    var _docRequerido = new DocumentosRequeridosERPDTO();

                    //se valida la categoria para obtener todas las especialidades y marcarlas como obligatorio
                    if (TipoDocumentoRequerido.Categorias == (TipoDocumentoRequerido)_myObject.tipo)
                    {

                        lstEspecialidadTercero
                        .Where(c => c.EspIdCategoria == _myObject.especialidad)
                        .ToList()
                        .ForEach(e => objRequeridos.Add(new DocumentosRequeridosERPDTO()
                        {
                            especialidad = e.EspId,
                            tipoAdjunto = _myObject.tipoAdjunto,
                            nombreEspecialidad = e.EspTexto
                        })
                        );
                    }
                    else
                    {
                        objRequeridos.Add(_myObject.MapToDTO(" Obligatorio"));
                    }

                });

                return objRequeridos.OrderBy(c => c.nombreEspecialidad);

            }
            catch (System.Exception e)
            {

                return new List<DocumentosRequeridosERPDTO>() {
                new DocumentosRequeridosERPDTO(){
                nombreEspecialidad=e.Message,
                }
                };
            }

        }
    }
}
