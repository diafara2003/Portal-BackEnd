using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Cotizaciones;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Abastecimiento.Cotizaciones
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CSCotizacionActividadController : ControllerBase
    {
        private readonly IUserRepository _Iprov;
        private const int idConstructora_ = 2;
        private const int idEmpresa_ = 1;

        public CSCotizacionActividadController(IUserRepository IProv)
        {
            _Iprov = IProv;
        }

        #region Portal

        [HttpGet]
        public IEnumerable<CSActividadCotDTO> Get(int idConstructora = idConstructora_, int idLicitacion = -1, int idCotizacion = -1)
        {
            return new CotizacionActividadBL().ConsultarActividadesCotizacion(idConstructora, idLicitacion, idCotizacion);
        }

        [HttpGet]
        [Route("ValidarImportacion")]
        public IEnumerable<CSActividadCotDTO> ValidarImportacionActividades(IEnumerable<CSActividadCotDTO> data)
        {
            return new CotizacionActividadBL().ValidarImportacionActividades(data);
        }

        #endregion

        #region ERP 

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> ConsultarActividades(int idConstructora, int idLicitacion = -1, int idCotizacion = -1)
        {

            var token = await new ConexionERP(idConstructora).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/Cotizacion/Actividad/Listado?idLicitacion=" + idLicitacion.ToString() + "&idCotizacion=" + idCotizacion.ToString();

            var response_ = await new ConexionERP(idConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            return Ok(response_);
        }


        [HttpPost]
        [Route("ValidarImportacion")]
        public async Task<IActionResult> ValidarImportacion(List<CSActividadCotDTO> data)
        {

            var token = await new ConexionERP(idConstructora_).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora_).ObtenerConstructora();
            string url = "/ADPRO/API/Cotizacion/Actividad/ValidarImportacion";

            var response_ = await new ConexionERP(idConstructora_).Peticion(Const_.ConstRuta_API + url, HttpMethod.Post, data, token.ToString());
            return Ok(response_);
        }

        #endregion



    }
}
