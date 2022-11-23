
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
[Table("ActividadEconomica")]
  public  class ActividadEconomica
    {
        [Key]
        public int ActEcId { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string ActEcCodigo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string ActECtexto { get; set; }

    }
}
