using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Novedades
{
    public class NovedadUsuarioDTO
    {
        public int IdNovedadUsuario { get; set; }
        public int IdNovedad { get; set; }
        public int IdUsuario { get; set; }
        public bool Visto { get; set; }
    }
}
