
namespace Code.Repository.Document.Model
{
    public static class MapDocumentosRequeridos
    {

        public static DocumentosRequeridosERPDTO MapToDTO(this ResponseERPDocReqDTO data, string nombreRequerido)
        {
            return new DocumentosRequeridosERPDTO
            {
                especialidad = data.especialidad,
                tipoAdjunto = data.tipoAdjunto,
                nombreEspecialidad = nombreRequerido

            };
        }
    }
}
