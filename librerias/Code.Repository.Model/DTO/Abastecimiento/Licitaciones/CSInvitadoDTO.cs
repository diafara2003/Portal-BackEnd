using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Licitaciones
{
    public class CSInvitadoDTO
    {
        public int IdInvitado { get; set; }
        public int IdLicitacion { get; set; }
        public string NIT { get; set; }
        public bool Acepta { get; set; }
        public int Asegurado { get; set; }
    }
}
