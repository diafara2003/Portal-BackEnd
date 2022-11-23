using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Licitaciones
{
    public class LicitacionTerceroDTO
    {
        public int Id { get; set; }
        public int IdLicitacion { get; set; }
        public string Nit { get; set; }
        public int IdTercero { get; set; }
        public int Estado { get; set; }
        public int Asegurado { get; set; }
    }
}
