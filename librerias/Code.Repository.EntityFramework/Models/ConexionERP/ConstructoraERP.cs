using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.DAO.Models.ConexionERP
{
    public class ConstructoraERP: Constructora
    {
        public string ConstUsuario_API { get; set; }
        public string ConstClave_API { get; set; }
    }
}
