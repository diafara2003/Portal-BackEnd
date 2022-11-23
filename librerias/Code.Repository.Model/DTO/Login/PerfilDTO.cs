

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code.Repository.Model.DTO.Login
{
    public class PerfilDTO
    {
        public int id { get; set; }

        [Required(ErrorMessage ="El nombre es obligatrio")]
        public string nombre { get; set; }
        public bool estado { get; set; }
        public string tipo { get; set; }

    }

    public class PerfilConsultaDTO: PerfilDTO
    {
        public int countUsuarios { get; set; }
    }


    public class AgregarPerfilDTO {

        public PerfilDTO perfil { get; set; }
        public IEnumerable<int> usuarios { get; set; }
    }
}
