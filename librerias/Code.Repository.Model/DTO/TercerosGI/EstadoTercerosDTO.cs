
namespace Code.Repository.Model.DTO.TercerosGI
{
    public class EstadoTercerosDTO
    {
        public string nombre { get; set; }
        public int count { get; set; }
    }

    public class CambiarEstadoTerCons {

        public int id { get; set; }
        public int estado { get; set; }
        public bool enviarCorreo { get; set; }

    }

    public class ResponseCrearProveedorDTO
    {
        public int idTercero { get; set; }
        public string tipos { get; set; }
    }

}
