using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Licitaciones
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
