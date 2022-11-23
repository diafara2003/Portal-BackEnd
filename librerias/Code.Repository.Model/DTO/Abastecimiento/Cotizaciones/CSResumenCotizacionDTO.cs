using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public  class CSResumenCotizacionDTO
    {

        public CSCotizacionCotDTO Cotizacion { get; set; }
        public CSLicitacionDTO Licitacion { get; set; }
    }
}
