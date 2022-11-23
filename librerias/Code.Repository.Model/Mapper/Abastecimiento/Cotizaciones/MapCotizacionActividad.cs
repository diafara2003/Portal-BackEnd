using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.Model.Entity.CS.Cotizaciones;
using Code.Repository.Model.Entity.CS.Licitaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper.Abastecimiento.Cotizaciones
{
    public static class MapCotizacionActividad
    {
        public static CSActividadCotDTO MapActividadCotToDTO(this LicitacionActividad Licitacion, CotizacionActividad Cotizacion)
        {
            return new CSActividadCotDTO()
            {
                IdCotizacion = Cotizacion.CACotizacionId,
                IdLicitacion = Licitacion.LALicitacionId,
                IdActividad = Licitacion.LAId,
                Codigo = Licitacion.LACodigo,
                Descripcion = Licitacion.LADescripcion,
                Alcance = Licitacion.LAAlcance,
                Cantidad = Licitacion.LaCant,
                UM = Licitacion.LAUM,
                Valor = Cotizacion.CAValor
            };
        }
    }
}
