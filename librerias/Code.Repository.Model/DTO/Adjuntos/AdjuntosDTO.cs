
namespace Code.Repository.Model.DTO.Adjuntos
{
  public  class AdjuntosDTO
    {

        public AdjuntosDTO()
        {
            this.id = 0;
            this.nombre = "";
            this.extension = "";
        }
        public int id { get; set; }
        public string nombre { get; set; }        
        public string extension { get; set; }
    }
}
