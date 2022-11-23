using Code.Repository.Model.DTO.Auditoria;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.Auditoria.dbo;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper.Auditoria
{
    public static class MapAuditoriaGeneral
    {
        public static AuditoriaGeneralDTO MapToDTO(this AuditoriaGeneral data, Usuario usuario)
        {
            DateTime _hh;
            bool _result = DateTime.TryParseExact(data.Hora.ToString(), "HHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None, out _hh);
            return new AuditoriaGeneralDTO()
            {
                Id = data.Id,
                Fecha = DateTime.ParseExact(data.Fecha.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd"),
                NewData = data.NewData,
                OldData = data.OldData,
                Opcion = data.Opcion,
                Hora = _result ? _hh.ToString("HH:mm:ss") : string.Empty,
                Tipo = data.TipoAuditoria,
                Documento = data.Documento,
                nameUsuario = usuario.UserNombre,
                IsNew = data.IsNew,
                IsDelete = data.IsDelete
            };
        }
    }
}
