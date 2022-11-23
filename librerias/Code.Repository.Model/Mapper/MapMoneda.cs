using Code.Repository.Model.DTO.Monedas;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper
{
    public static class MapMoneda
    {
        public static MonedaDTO MapToDTO(this Monedas data)
        {
            return new MonedaDTO()
            {
                IdMoneda = data.MonId,
                Descripcion = data.MonDescripcion,
                Abreviacion = data.MonAbreviacion,
                Funcional = data.MonFuncional,
                TasaCambio = data.MonTasaCambio
            };
        }

        public static Monedas MapToEntity(this MonedaDTO data)
        {
            return new Monedas()
            {
                MonId = data.IdMoneda,
                MonDescripcion = data.Descripcion,
                MonAbreviacion = data.Abreviacion,
                MonFuncional = data.Funcional,
                MonTasaCambio = data.TasaCambio
            };
        }

    }
}
