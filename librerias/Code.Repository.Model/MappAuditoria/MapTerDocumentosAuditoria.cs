using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.MappAuditoria
{
    public static class MapTerDocumentosAuditoria
    {
        public static TerdocumentosAuditoriaDTO MapToAuditoria(this Adjuntos data, string nombreTipoDoc)
        {
            return new TerdocumentosAuditoriaDTO()
            {
                nombreDocumento = data.AjdNombre,
                nombreTipoDocumento = nombreTipoDoc

            };
        }


    }
}
