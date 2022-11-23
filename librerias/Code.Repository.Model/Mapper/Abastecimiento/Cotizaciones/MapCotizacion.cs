using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.Model.Entity.adp_cs;

namespace Code.Repository.Model.Mapper.Abastecimiento.Cotizaciones
{
    public static class MapCotizacion
    {
        public static CSCotizacionCotDTO MapToDTO(this CSCotizaciones Cotizacion)
        {
            return new CSCotizacionCotDTO()
            {
                IdCotizacion = Cotizacion.CLId,
                Cotizacion = Cotizacion.CLCotizacion,
                IdLicitacion = Cotizacion.CLLicitacionId,
                IdTercero = Cotizacion.CLLicitacionId,
                Valor = Cotizacion.CLValor,
                FormaPago = Cotizacion.CLFormaPago,
                TipoTributo = Cotizacion.CLTipoTributo,
                Estado = Cotizacion.CLEstado
            };
        }

        public static CSCotizaciones MapToEntity(this CSCotizacionCotDTO Cotizacion)
        {
            return new CSCotizaciones()
            {
                CLId = Cotizacion.IdCotizacion,
                CLCotizacion = Cotizacion.Cotizacion,
                CLTerceroId = Cotizacion.IdTercero,
                CLLicitacionId = Cotizacion.IdLicitacion,
                CLValor = Cotizacion.Valor,
                CLFormaPago = Cotizacion.FormaPago,
                CLTipoTributo = Cotizacion.TipoTributo,
                CLEstado = Cotizacion.Estado
            };
        }
    }
}
