using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.dbo
{
    public class NovedadesDet
    {
        [Key]
        public int Id { get; set; }
        public int NovDetIdNov { get; set; }
        public int NovDetIdTipoAdjujunto { get; set; }
        public string NovDetTipo { get; set; }
    }
}
