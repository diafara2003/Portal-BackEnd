using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public class CSDatosCotDTO
    {
        public decimal Administracion { get; set; }
        public string FechaCreacion { get; set; }
        public string FormaPago { get; set; }
        public decimal IVA { get; set; }
        public int IdConstructora { get; set; }
        public int IdCotizacion { get; set; }
        public int IdLicitacion { get; set; }
        public decimal Imprevistos { get; set; }
        public int Proveedor { get; set; }
        public int TiempoEntrega { get; set; }
        public int TipoTributo { get; set; }
        public decimal Utilidad { get; set; }
        public int Vigencia { get; set; }

    }
}
