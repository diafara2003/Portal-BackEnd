
namespace Code.Repository.Model.DTO.TercerosGI
{
    public class TerCuentaBancariaDTO
    {
        public int id { get; set; }
        public string banco { get; set; }
        public string BancoTexto { get; set; }
        public int tipoCuenta { get; set; }
        public string tipoCuentaTexto { get; set; }
        public string numero { get; set; }
        public int ciudad { get; set; }
        public string ciudadTexto { get; set; }
        public string correoPagos { get; set; }
    }
}
