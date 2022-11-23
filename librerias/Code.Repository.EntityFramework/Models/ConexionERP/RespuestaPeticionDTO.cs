using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.DAO.Models.ConexionERP
{
    public class RespuestaPeticionDTO
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public object data { get; set; }

    }

}
