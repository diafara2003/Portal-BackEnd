using Code.Repository.Model.DTO.Especialidades;
using Code.Repository.Model.Entity;

namespace Code.Repository.Model.Mapper
{
    public static class MappEspecialidades
    {

        public static EspecialidadDTO MapToDTO(this EspecialidadesTercero data)
        {

            return new EspecialidadDTO()
            {
                id = data.EspId,
                nombre = data.EspTexto,
                grupo = data.EspIdCategoria

            };
        }

        public static EspecialidadAcDTO MapToDTO(this EspecialidadesTercero data, string catDesc,string grupoDesc)
        {

            return new EspecialidadAcDTO()
            {
                id = data.EspId,
                nombre = data.EspTexto,
                categoriaTexto = catDesc,
                grupoTexto=grupoDesc


            };
        }

        public static EspecialidadDTO MapToDTO(this GruposTercero data)
        {

            return new EspecialidadDTO()
            {
                id = data.GruId,
                nombre = data.GruTexto

            };
        }


        public static EspecialidadDTO MapToDTO(this CategoriasTercero data)
        {

            return new EspecialidadDTO()
            {
                id = data.CatId,
                nombre = data.CatTexto

            };
        }
    }
}
