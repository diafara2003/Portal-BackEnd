using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Code.Repository.Model.Entity.dbo
{
    [Table("TerSISO", Schema = "dbo")]
    public class TerSISO
    {

        [Key]
        public int Id { get; set; }
        public int IdTercero { get; set; }
        public bool ProgramaSaludOcupacional { get; set; }
        public bool ProgramaFactoresRiesgo { get; set; }
        public bool TieneComiteSO { get; set; }
        public bool ProgramaSeguridadEhigiene { get; set; }
        public bool ProgramaAmbiental { get; set; }
    }
}
