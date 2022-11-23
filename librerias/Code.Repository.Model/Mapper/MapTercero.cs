using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Code.Repository.Model.Mapper
{
    public static class MapTercero
    {

        public static TerceroDTO MapToDTO(this Terceros data, TerInformacionGeneral info)
        {
            return new TerceroDTO()
            {
                id = data.Terid,
                correo = info.TigCorreo,
                documento = info.TigNumeroIdentificacion,
                estado = (EstadoTercero)data.TerEstado,
                correoConfirmado = data.TerEmailConfirmado
            };

        }



    }
}
