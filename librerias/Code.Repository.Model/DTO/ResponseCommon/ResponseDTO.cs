

namespace Code.Repository.Model.DTO.ResponseCommon
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
            this.codigo = 0;
            this.Success = true;
            this.mensaje = string.Empty;
        }

        public int codigo { get; set; }
        public string mensaje { get; set; }
        public bool Success { get; set; }
    }
}
