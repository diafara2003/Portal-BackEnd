using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento
{
    public class CSProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public string Correo { get; set; }
    }
}
