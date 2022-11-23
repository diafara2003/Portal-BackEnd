using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Auditoria
{
    public class AuditoriaGeneralDTO
    {
        public int Id { get; set; }
        public int Documento { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }        
        public string Fecha { get; set; }
        public string Opcion { get; set; }
        public int Tipo { get; set; }
        public string Hora { get; set; }
        public string nameUsuario {get;set;}
        public bool IsDelete { get; set; }
        public bool IsNew { get; set; }

    }

    public class AuditDTO
    {
        public Glosario Glosario { get; set; }
        public Valores Valores { get; set; }
        public int Id { get; set; }
        public int Documento { get; set; }
        public string Fecha { get; set; }
        public string Opcion { get; set; }
        public int Tipo { get; set; }
        public string Hora { get; set; }
        public string nameUsuario { get; set; }
        public int CountVerMas { get; set; }
        public bool IsDelete{ get; set; }
        public bool IsNew { get; set; }

    }

    public class Glosario
    {
        public string NombreSQL { get; set; }
        public string NombreHTML { get; set; }
    }
    public class Valores
    {
        public string Old { get; set; }
        public string New { get; set; }
    }
}
