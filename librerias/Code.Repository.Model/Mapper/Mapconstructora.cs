

using Code.Repository.Model.DTO.Constructora;
using Code.Repository.Model.Entity;

namespace Code.Repository.Model.Mapper
{
    public static class Mapconstructora
    {
        public static ConstructoraDTO MapToDTO(this Constructora _contructora)
        {

            return new ConstructoraDTO()
            {
                id = _contructora.ConstId,
                NIT = _contructora.ConstNIT,
                nombre = _contructora.ConstNombre,
                urlLogo = _contructora.ConstUrlLogo,
                baseURL = _contructora.ConstRuta_API


            };
        }
    }
}
