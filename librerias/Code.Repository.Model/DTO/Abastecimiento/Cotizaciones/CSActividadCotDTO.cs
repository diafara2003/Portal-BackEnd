using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public class CSActividadCotDTO : CSActividadDTO
    {
        public int? IdCotizacion { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Total { get; set; }
        public bool Valido { get; set; }
        public int? Moneda { get; set; }
        public decimal? TasaCambio { get; set; }
        public string MonedaAbrev { get; set; }
    }
}
