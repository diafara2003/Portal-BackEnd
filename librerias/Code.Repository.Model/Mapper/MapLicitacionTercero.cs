using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.Model.DTO.Constructora;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.adp_cs;
using Code.Repository.Model.Entity.CS.Licitaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper
{
    public static class MapLicitacionTercero
    {

        public static LicitacionTerceroDTO MapToLicitacionTerceroDTO(this LicitacionTercero register)
        {
            return new LicitacionTerceroDTO()
            {
                Id = register.LTId,
                IdLicitacion = register.LTLicitacionId,
                IdTercero = register.LTTerceroId,
                Estado = register.LTEstado,
                Asegurado = register.LTAsegurado,
            };
        }

        public static LicitacionTercero MapToLicitacionDTO(this LicitacionTerceroDTO register)
        {
            return new LicitacionTercero()
            {
                LTId = register.Id,
                LTLicitacionId = register.IdLicitacion,
                LTTerceroId = register.IdTercero,
                LTEstado = register.Estado,
                LTAsegurado=register.Asegurado
            };
        }


        public static CSAseguradoraDTO MapCSAseguradoraDTO(this LicitacionTercero licT , CSLicitacion lic , Constructora con, TerInformacionGeneral ter)
        {
            return new CSAseguradoraDTO()
            {
                Id = licT.LTId,
                Empresa = con.ConstNombre,
                Licitacion = lic.LicNumero,
                Identificacion = ter.TigNumeroIdentificacion,
                RazonSolcial = ter.TigNombre,
                ValorEstimado = lic.LicVrEstimado,
                Asegurado = licT.LTAsegurado,
                IdLicitacion = lic.LicLicitacion,
                IdConstructora = con.ConstId,
                Direccion = ter.TigDireccion,
                Telefono = ter.TigTelefono,
                Correo = ter.TigCorreo,

            };
        }
    }
}
