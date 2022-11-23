using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Monedas
{
    public class MonedaDTO
    {
        public int IdMoneda { get; set; }
        public string Descripcion { get; set; }
        public string Abreviacion { get; set; }
        public bool Funcional { get; set; }
        public decimal TasaCambio { get; set; }

    }
}
