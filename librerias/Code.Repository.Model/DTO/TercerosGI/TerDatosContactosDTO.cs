using Code.Repository.Model.DTO.Ciudades;


namespace Code.Repository.Model.DTO.TercerosGI
{
    public class TerDatosContactosDTO
    {
        public int Id { get; set; }
        public int TerceroId { get; set; }
        public int TipoContactoId { get; set; }
        public string Nombre { get; set; }
        public string NumeroDocumento { get; set; }
        public string Cargo { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public CiudadesDTO Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }       
        public bool isNew { get; set; }
    }

    public class TerDatosContactos_Audit_DTO
    {
        public int TdcId { get; set; }
        public int TdcTerceroId { get; set; }
        public int TdcTipoContactoId { get; set; }
        public string TdcNombre { get; set; }
        public string TdcNumDocumento { get; set; }
        public string TdcCargo { get; set; }
        public string TdcCorreo { get; set; }
        public string TdcDireccion { get; set; }
        public string TdcCiudad { get; set; }
        public string TdcTelefono { get; set; }
        public string TdcCelular { get; set; }
    }
}
