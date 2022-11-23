
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace Code.Repository.RepositoryBL.Operations
{
    public class CategoriaBL
    {
        public IEnumerable<CategoriaDTO> ConsultarCategorias(string filter)
        {
            filter = filter == "_" ? "" : filter;
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _data = objcnn.categorias.Where(x => x.CatDesc.Contains(filter)).OrderBy(z => z.CatDesc);

            List<CategoriaDTO> Categorias_ = new List<CategoriaDTO>();
            foreach (var x in _data)
            {
                Categorias_.Add(x.MapToCategoriaDTO());
            }


            return Categorias_;


        }
    }
}
