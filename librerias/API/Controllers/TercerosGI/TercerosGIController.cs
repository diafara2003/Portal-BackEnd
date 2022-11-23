
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers.TercerosGI
{
    [Route("api/[controller]")]
    [ApiController]
    public class TercerosGIController : ControllerBase
    {
        private readonly IUserRepository _IUser;

        public TercerosGIController(IUserRepository IUser)
        {
            _IUser = IUser;
        }



        #region INFORMACION GENERAL

        /// <summary>
        /// Metodo encargado de consutar los terceros por constructora
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listado")]
        public IActionResult ConsultarListadoTercero(int id = 0, int estado = 0, int rows = 50, int page = 1)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new TercerosGestionInformacionBL().ConsultarListadoTercero(currentUser.idEmpresa, id, estado));
        }


        [HttpGet]
        [Route("buscar")]
        public IActionResult ConsultarListadoTercero(string filter = "")
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new TercerosGestionInformacionBL().BuscarTerceros(currentUser.idEmpresa, filter));
        }

        /// <summary>
        /// Metodo encargado de conentar con DAO para la obtencion de la informacion general segun empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [EnableCors("CorsPolicy")]
        [Route("InformacionGeneral")]
        public IActionResult ConsultaInformacionGeneral(int id)
        {

            if (id <= 0)
            {
                var currentUser = _IUser.GetUser(HttpContext);
                id = currentUser.idEmpresa;
            }

            return Ok(new TercerosGestionInformacionBL().ConsultarInfGeneral(id));
        }
        /// <summary>
        /// Metodo encargado de actulizar la informacion general de la empresa
        /// </summary>
        /// <param name="infGeneral"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GuardaInfGeneral")]
        public IActionResult GuardaInfGeneral(TerInformacionGeneralDTO infGeneral)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new TercerosGestionInformacionBL().ActualizaInfGeneral(infGeneral, currentUser.idEmpresa, currentUser.id));
        }



        #endregion

        #region DATOS CONTACTO


        /// <summary>
        /// Metodo encargado de consultar los datos de los contantos
        /// </summary>
        /// <param name="TipoContacto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DatosContacto")]
        public IActionResult ConsultaDatosContacto(int TipoContacto, int id)
        {
            if (id <= 0)
                id = _IUser.GetUser(HttpContext).idEmpresa;

            return Ok(new TercerosGestionInformacionBL().ConsultarDatosContacto(id, TipoContacto));
        }
        /// <summary>
        /// Metodo encargado de guardar los datos de los contactos
        /// </summary>
        /// <param name="datosContacto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GuardaDatosContacto")]
        public IActionResult GuardaDatosContacto(TerDatosContactosDTO datosContacto)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new TercerosGestionInformacionBL().GuardaDatosContacto(datosContacto, currentUser.idEmpresa, currentUser.id));
        }
        /// <summary>
        /// Metodo encargado de eliminar los datos de un contacto especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteDatoContacto")]
        public IActionResult ElimiarDatoContacto(int idContacto)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new TercerosGestionInformacionBL().EliminarDatoContacto(idContacto, currentUser.idEmpresa, currentUser.id));

        }

        #endregion


        #region INSCRITOS EN CAMARA Y COMERCIO

        /// <summary>
        /// consulta las personas inscritas en camara y comercio asociado al proveedor de session
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CamaraComercio")]
        public IActionResult GetCamaraComercio(int id)
        {

            if (id <= 0)
                id = _IUser.GetUser(HttpContext).idEmpresa;


            return Ok(new TercerosGestionInformacionBL().GetCamaraComercio(id));
        }

        /// <summary>
        /// guarda una persona en camara y comercio asociada el proveedor de sesion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CamaraComercio")]
        public IActionResult GetCamaraComercio(TerCamaraComercioDTO request)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new TercerosGestionInformacionBL().GuardarCamaraComercio(request, currentUser.idEmpresa, currentUser.id));
        }



        /// <summary>
        /// elimina una persona de camara y comercio asociado al proveedor de sesion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("CamaraComercio/{id:int}")]
        public IActionResult DeleteCamaraComercio(int id)
        {

            var currentUser = _IUser.GetUser(HttpContext);
            new TercerosGestionInformacionBL().EliminarCamaraComercio(id, currentUser.idEmpresa, currentUser.id);

            return Ok(new ResponseDTO()
            {
                codigo = 1,
                mensaje = "",
                Success = true
            });
        }

        #endregion



        #region INFORMACION TRIBUTARIA

        [HttpGet]
        [Route("bancos")]
        public IActionResult GetBancos()
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new TercerosGestionInformacionBL().GetBancos());
        }

        [Route("tipocuentas")]
        [HttpGet]
        public IActionResult GetTipocuenta()
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new TercerosGestionInformacionBL().GetTipoCuentaBancaria());
        }

        [HttpGet]
        [Route("informaciontributaria")]
        public IActionResult GetInfoTributaria(int id)
        {
            if (id <= 0)
            {
                var currentUser = _IUser.GetUser(HttpContext);
                id = currentUser.idEmpresa;
            }

            return Ok(new TercerosGestionInformacionBL().GetInfoTributaria(id));
        }

        [HttpPost]
        [Route("informaciontributaria")]
        public IActionResult PostInfoTributaria(TerInfoTributaria request)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            request.IdTercero = currentUser.idEmpresa;

            return Ok(new TercerosGestionInformacionBL().SaveInfoTributaria(request, currentUser.idEmpresa, currentUser.id));
        }

        [HttpGet]
        [Route("informacionSISO")]
        public IActionResult GetInfoSISO(int id = 0)
        {
            if (id <= 0)
            {
                var currentUser = _IUser.GetUser(HttpContext);
                id = currentUser.idEmpresa;
            }

            return Ok(new TercerosGestionInformacionBL().GetInfoSISO(id));
        }




        [HttpPost]
        [Route("informacionSISO")]
        public IActionResult PostInfoSISO(TerSISO request)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            request.IdTercero = currentUser.idEmpresa;

            return Ok(new TercerosGestionInformacionBL().SaveInfoSISO(request, currentUser.idEmpresa, currentUser.id));
        }

        [HttpGet]
        [Route("informacionbancaria")]
        public IActionResult GetcuentaBancaria(int id = 0)
        {
            if (id <= 0)
            {
                var currentUser = _IUser.GetUser(HttpContext);
                id = currentUser.idEmpresa;
            }

            return Ok(new TercerosGestionInformacionBL().GetcuentaBancaria(id));
        }
        [HttpPost]
        [Route("informacionbancaria")]
        public IActionResult PostcuentaBancaria(TerCuentaBancariaDTO request)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new TercerosGestionInformacionBL().SaveCuentaBancaria(request, currentUser.idEmpresa, currentUser.id));
        }

        #endregion

    }
}
