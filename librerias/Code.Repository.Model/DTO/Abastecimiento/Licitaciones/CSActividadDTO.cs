using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Licitaciones
{
    public class CSActividadDTO
    {
        public int IdLicitacion { get; set; }
        public int IdActividad { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string UM { get; set; }
        public string Alcance { get; set; }
        public decimal Cantidad { get; set; }
    }
}
