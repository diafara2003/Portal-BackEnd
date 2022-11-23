using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Licitaciones
{
    public class CSAseguradoraDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public int Licitacion { get; set; }
        public string Identificacion { get; set; }
        public string RazonSolcial { get; set; }
        public decimal ValorEstimado { get; set; }
        public int Asegurado { get; set; }
        public int IdLicitacion { get; set; }
        public int IdConstructora { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}
