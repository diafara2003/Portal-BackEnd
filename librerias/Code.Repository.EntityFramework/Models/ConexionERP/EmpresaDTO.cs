using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.DAO.Models.ConexionERP
{
    /*
       [{"IdOrigen":1,"IdEmpresa":1,"Nombre":"DEMO SAS","Estado":true,"Imagenes":"../../Logos/LogoSincoSoftPortada.png"}]
       */
    public class EmpresaDTO
    {
        public int IdOrigen { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string Imagenes { get; set; }
    }
}
