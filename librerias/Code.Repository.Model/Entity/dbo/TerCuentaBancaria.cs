using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace Code.Repository.Model.Entity.dbo
{
    [Table("TerCuentaBancaria")]
    public class TerCuentaBancaria
    {
        [Key]
        public int Id { get; set; }
        public int IdTercero { get; set; }
        public string IdBanco { get; set; }
        public int TipoCuenta { get; set; }
        public string Numero { get; set; }
        public int IdCiudad { get; set; }
        public string CorreoNotificaPago { get; set; }
    }
}
