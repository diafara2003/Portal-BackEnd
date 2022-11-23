using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.ProductosProveedor;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static API.Helper.XmlHelper;

namespace API.Controllers.ProductosProveedor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosProveedorController : ControllerBase
    {
       
        private readonly IUserRepository _IUser;
        public ProductosProveedorController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        // ERP
        [HttpGet]
        [Route("ProductosCliente")]
        public async Task<IActionResult> ConsultarProductosProveedorCliente(int idConst, int Sincroniza)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            UserSessionDTO sesion = _IUser.GetUser(HttpContext);

            string url = "/ADPRO/API/ProveedorZonas/ConsultaProductosXTerceroZona?TerNit=" + sesion.nit+"&ConstNit="+Const_.ConstNIT;

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            List<ProductosProveedorDTO> data = new();
            if (response_ == "[]")
            {
                ResponseDTO respuesta = new()
                {
                    codigo = -3,
                    mensaje = "No hay datos configurados por la constructora"
                };
                return Ok(respuesta);
            }
            else
            {
                var x = JArray.Parse(response_);

                var ListProductoProveedor= ((JArray)x).Select(x => new ProductosProveedorDTO
                {

                    TerceroERP=(int)x["IdTercero"],
                    IdProducto = (int)x["IdProducto"],
                    IdZona = (int)x["IdZona"],
                    IdRegister = (int)x["IdRegister"],
                    Descripcion = (string)x["Descripcion"],
                    Um = (string)x["Um"],
                    Dto = (decimal)x["Dto"],
                    Iva = (decimal)x["Iva"],
                    ValorSinIva = (decimal)x["ValorSinIva"],
                    FechaVigencia = (DateTime)x["FechaVigencia"],
                    Obs=(string)x["Obs"],
                    Referencia = (string)x["Referencia"],
                    ZonaNombre = (string)x["ZonaNombre"],
                    EntregaMax = (int)x["EntregaMax"],
                    IdBim = (string)x["IdBim"],
                    FechaCotizacion=(DateTime)x["FechaCotizacion"],
                    CantidadMinima=(int)x["CantidadMin"]

                }).ToList();

                if (Sincroniza == 1)
                {
                    string xmlMessage = SerializarXML<ProductosProveedorDTO>.Serialize(ListProductoProveedor);
                    return Ok(new ProductosProveedorBL().SincronizarProductosProveedor(sesion.id, idConst , xmlMessage));

                }
                else
                {
                    return Ok(ListProductoProveedor);
                }

            }
        }


        [HttpGet]
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductosProveedor(int idConst)
        {
            UserSessionDTO sesion = _IUser.GetUser(HttpContext);
            Console.WriteLine(sesion);

            return Ok(new ProductosProveedorBL().ConsultarListadoxConstructora(idConst, sesion.id));
        }

        [HttpPost]
        [Route("ActualizarProducto")]
        public IActionResult ActualizaProducto(ProductosProveedorDTO data)
        {
            return Ok(new ProductosProveedorBL().ActualizarProducto(data));
        }

        [HttpPost]
        [Route("Migrar")]
        public IActionResult Migrar(List<MigracionProductosProveedorDTO> data )
        {
            UserSessionDTO sesion = _IUser.GetUser(HttpContext);
            string xmlMessage = SerializarXML<MigracionProductosProveedorDTO>.Serialize(data);
            return Ok(new ProductosProveedorBL().MigrarProductosProveedor(sesion.id, xmlMessage));

        }


        [HttpGet]
        [Route("EnviarSolicitud")]
        public async Task<IActionResult> EnviarSolicitudProductoProv(int idConst, int idUsuario)
        {
            string xmlMessage = SerializarXML<ProductosProveedorDTO>.Serialize(new ProductosProveedorBL().ConsultarListadoxConstructora(idConst, idUsuario).ToList());
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            var objeto = new { xml = xmlMessage, id = idUsuario };
            string url = "/ADPRO/API/ProveedorZonas/RecibirSolicitudPPZona";
            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Post, objeto, token.ToString());
            return Ok(response_);
            
        }

        //ERP
        [HttpGet]
        [Route("ConsultarSolicitudes")]
        public async Task<IActionResult> ConsultarSolicitudes(int idConst, int idUsuario)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/ProveedorZonas/ConsultarSolicitudPPZona?tererp=-1&terid=" + idUsuario ;
            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var x = JArray.Parse(response_);
            List<SolicitudProductosProveedorDTO> data = new();
            var ListSolicitud = ((JArray)x).Select(x => new SolicitudProductosProveedorDTO
            {
                Id =(int)x["PPZId"],
                IdTercero=(int)x["PPZTercero"],
                Fecha=(DateTime)x["PPZFecha"],
                Estado=(int)x["PPZEstado"],
                UsuAprob=(int)x["PPZUsuAprob"],
                Sincronizado=(bool)x["PPZSincronizado"]
                

            }).ToList();
            return Ok(ListSolicitud);
        }

        [HttpGet]
        [Route("ConsultarDetallesSolicitud")]
        public async Task<IActionResult> ConsultarDetalleSolicitud(int idConst, int id)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/ProveedorZonas/ConsultarSolicitudDetallePPZona?id=" + id;
            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var x = JArray.Parse(response_);
            List<SolicitudProductosProveedorDetDTO> data = new();
            var ListSolicitud = ((JArray)x).Select(x => new SolicitudProductosProveedorDetDTO
            {
               Id= (int)x["PPZId"],
               IdPortal=(int)x["PPZDetIdPortal"],
               IdProdProveedor=(int)x["PPZDetProv"],
               IdProducto=(int)x["PPZDetProducto"],
               DetId=(int)x["PPZDetId"],
               DetIdBim=(string)x["PPZDetBim"],
               DetIdZona=(int)x["PPZDetIdZona"],
               DetDescripcion=(string)x["PPZDetDescripcion"],
               DetDto=(decimal)x["PPZDetDto"],
               DetEntregaMax=(int)x["PPZDetEntregaMax"],
               DetEstado=(int)x["PPZDetEstado"],
               DetFechaCotizacion=(DateTime)x["PPZDetFechaCotizacion"],
               DetFechaVigencia=(DateTime)x["PPZDetFechaVigencia"],
               DetIva=(decimal)x["PPZDetDetIva"],
               DetNombreZona=(string)x["PPZDetNombreZona"],
               DetReferencia=(string)x["PPZDetDetReferencia"],
               DetUm=(string)x["PPZDetUm"],
               DetUsuario=(int)x["PPZDetUsuario"],
               DetValorSinIva=(decimal)x["PPZDetValorSinIva"]
            }).ToList();
            return Ok(ListSolicitud);
        }

    }
}




