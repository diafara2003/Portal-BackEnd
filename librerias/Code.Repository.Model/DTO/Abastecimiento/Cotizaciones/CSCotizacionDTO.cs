using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public class CSCotizacionDTO
    {
        public int IdCotizacion { get; set; }
        public int Numero { get; set; }
        public CSProveedorDTO Proveedor { get; set; }
        public string Fecha { get; set; }
        public CSEstadoDTO Estado { get; set; }
        public CSFormaPagoDTO FormaPago { get; set; }
        public decimal Valor { get; set; }
        public bool AplicaAIU { get; set; }
        public decimal IVA { get; set; }
        public decimal Administracion { get; set; }
        public decimal Imprevistos { get; set; }
        public decimal Utilidad { get; set; }
    }
}
