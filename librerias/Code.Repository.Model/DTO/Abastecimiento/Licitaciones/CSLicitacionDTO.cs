using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Licitaciones
{
    public class CSLicitacionDTO
    {
        public int IdConstructora { get; set; }
        public string NombreEmpresa { get; set; }
        public int IdLicitacion { get; set; }
        public int Numero { get; set; }
        public string FechaCierre { get; set; }
        public string Asunto { get; set; }
        public int Categoria { get; set; }
        public string NombreCategoria { get; set; }
        public int Estado { get; set; }
        public string Proyecto { get; set; }
        public string Ciudad { get; set; }

    }
}
