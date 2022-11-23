using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.RepositoryBL.Operations.Licitaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Licitaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        public int prov_ = 20;

        [HttpGet]
        [Route("Listado")]
        public IEnumerable<NotificacionDTO> ConsultarNotificaciones(int idEmpresa)
        {
            return new NotificacionesBL().ConsultarNotificaciones(prov_, idEmpresa);
        }

        [HttpGet]

        public NotificacionDTO ConsultarNotificacion(int idNotificacion)
        {
            return new NotificacionesBL().ConsultarNotificacion(idNotificacion);
        }

        [HttpPut]
        public ResponseDTO GuardarNotificacion(NotificacionDTO data)
        {
            return new NotificacionesBL().GuardarNotificacion(data);
        }
    }
}
