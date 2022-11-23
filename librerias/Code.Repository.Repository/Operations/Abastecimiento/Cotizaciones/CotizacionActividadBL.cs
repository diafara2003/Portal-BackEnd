using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using Code.Repository.Model.Mapper;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.Model.Mapper.Abastecimiento.Cotizaciones;
using Code.Repository.Model.Entity.CS.Cotizaciones;

namespace Code.Repository.RepositoryBL.Operations.Abastecimiento.Cotizaciones
{
    public class CotizacionActividadBL
    {
        public IEnumerable<CSActividadCotDTO> ConsultarActividadesCotizacion(int idConstructora, int idLicitacion, int idCotizacion)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var data_ = (from _actividad in objcnn.licitacionActividad
                         join _cActividad in objcnn.cotizacionActividad
                              on _actividad.LAId equals _cActividad.CAActividadId
                                                  into actividades
                         from c in actividades.DefaultIfEmpty()
                         select _actividad.MapActividadCotToDTO(c == null ? new CotizacionActividad() : c)
                         );
            return data_;
        }


        public IEnumerable<CSActividadCotDTO> ValidarImportacionActividades(IEnumerable<CSActividadCotDTO> data)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var data_ = (from _actividad in objcnn.licitacionActividad
                         join _cActividad in objcnn.cotizacionActividad
                              on _actividad.LAId equals _cActividad.CAActividadId
                                                  into actividades
                         from c in actividades.DefaultIfEmpty()
                         select _actividad.MapActividadCotToDTO(c == null ? new CotizacionActividad() : c)
                         );
            return data_;
        }


    }
}
