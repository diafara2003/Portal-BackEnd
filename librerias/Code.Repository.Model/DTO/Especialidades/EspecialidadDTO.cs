using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Especialidades
{
    public class EspecialidadDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int grupo { get; set; }
    }

    public class EspecialidadAcDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string grupoTexto { get; set; }
        public string categoriaTexto { get; set; }
    }


    public class AsociarEspecialidadDTO
    {
        public int id { get; set; }
    }


    public class EspecialidadesGruposDTO
    {

        public int grupo { get; set; }
        public int categoria { get; set; }
        public int especialidad { get; set; }
        public string texto { get; set; }
    }

}
