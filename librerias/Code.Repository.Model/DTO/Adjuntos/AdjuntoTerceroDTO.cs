
namespace Code.Repository.Model.DTO.Adjuntos
{
    public class AdjuntoTerceroDTO
    {

        public AdjuntosDTO adjunto { get; set; }
        public TipoAdjuntoTerceroDTO tipoAdjunto { get; set; }
    }


    public class TipoAdjuntoTerceroDTO {

        public int id { get; set; }
        public string nombre { get; set; }
    }
}
