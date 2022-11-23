using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.Entity;


namespace Code.Repository.Model.Mapper
{
    public static class MapCategoria
    {
        public static CategoriaDTO MapToCategoriaDTO(this Categoria register)
        {
            return new CategoriaDTO()
            {
                IdCategoria = register.CatId,
                Descripcion = register.CatDesc,
                Estado = register.CatEstado
            };
        }


    }
}
