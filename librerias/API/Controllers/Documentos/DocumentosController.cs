

using Code.Repository.Document.Operations;
using Code.Repository.Model.DTO.Adjuntos;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Documentos
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentosController : ControllerBase
    {

        private readonly IUserRepository _IUser;
        private readonly IWebHostEnvironment _env;

        public DocumentosController(IUserRepository IUser, IWebHostEnvironment env)
        {
            _IUser = IUser;
            _env = env;
        }


        [HttpGet("constructora/logo")]
        public IActionResult GetLogoConstructora(int constructora)
        {

            string logo = new ConstucturasBL().ObtenerConstructora(constructora).ConstUrlLogo;

            if (string.IsNullOrEmpty(logo))
            {

                var image = System.IO.File.OpenRead($"{_env.ContentRootPath}\\images\\sin-logopng.png");

                return File(image, "image/jpeg");
            }
            else {

                var image = "";//System.IO.File.OpenRead($"{logo}");   
                return File(image, "image/jpeg");
            }
        }


        /// <summary>
        /// obtiene todos los adjuntos por tercero
        /// </summary>
        /// <returns></returns>
        [HttpGet("tercero")]
        public IEnumerable<AdjuntoTerceroDTO> GetAdjuntos(int id)
        {
            if (id <= 0)
                id = _IUser.GetUser(HttpContext).idEmpresa;

            return new AdjuntoBL().GetAdjuntoTercero(id);

        }

        [HttpGet("tercero/tipodocumento")]
        public IEnumerable<AdjuntoTerceroDTO> GetAdjuntosTercero(int proveedor, string tipodocumento)
        {
            if (proveedor <= 0)
                proveedor = _IUser.GetUser(HttpContext).idEmpresa;

            return new AdjuntoBL().GetAdjuntoTerceroXTipoAdjunto(proveedor, tipodocumento);

        }





        /// <summary>
        /// encargado de suir archivos
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPost]
        [Route("UploadFiles/")]
        public IActionResult SubirArchivo()
        {
            try
            {
                string Tipo = HttpContext.Request.Form["Tipo"].ToString();
                //string OrigenID = HttpContext.Request.Form["OrigenID"].ToString();
                string OrigenID2 = HttpContext.Request.Form["OrigenID2"].ToString();

                string OrigenID = _IUser.GetUser(HttpContext).idEmpresa.ToString();

                Tipodocumento tipo = (Tipodocumento)Enum.Parse(typeof(Tipodocumento), Tipo, true);

                return Ok(new AdjuntoBL().GuardarArchivos(Request.Form.Files, tipo, int.Parse(OrigenID), int.Parse(OrigenID2), _env.ContentRootPath, _IUser.GetUser(HttpContext).id));
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }

        }

        [HttpGet]
        [Route("Descargar")]
        public IActionResult Descargar(int id)
        {
            var _arhivos = new AdjuntoBL().GetFile(id);


            if (_arhivos.Adjruta == null || string.IsNullOrEmpty(_arhivos.Adjruta) || string.IsNullOrWhiteSpace(_arhivos.Adjruta))
                return BadRequest("la Ruta del archivo es obligatorio.");
            if (!System.IO.File.Exists(_arhivos.Adjruta))
                return BadRequest("El archivo no existe en el servidor.");


            string nameFile = HttpUtility.UrlEncode(_arhivos.AjdNombre, System.Text.Encoding.UTF8);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(_arhivos.Adjruta, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(_arhivos.Adjruta);
            return File(bytes, contentType, Path.GetFileName(nameFile));


        }


    }
}
