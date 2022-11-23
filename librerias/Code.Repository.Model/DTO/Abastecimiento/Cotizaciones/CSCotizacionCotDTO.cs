using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public class CSCotizacionCotDTO
    {
        public int IdCotizacion { get; set; }
        public int Cotizacion { get; set; }
        public int IdTercero { get; set; }
        public int IdLicitacion { get; set; }
        public decimal Valor { get; set; }
        public string FormaPago { get; set; }
        public string TipoTributo { get; set; }
        public int Estado { get; set; }
    }
}
