using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.adp_cs;
using Code.Repository.Model.Entity.CS.Cotizaciones;
using Code.Repository.Model.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations.Abastecimiento.Licitaciones
{
    public class LicitacionTerceroBL : OperationsEF
    {
        private object constructoras;

        public IEnumerable<CSAseguradoraDTO> consultaAsegurados(int op)
        {
            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                var data = (from licT in objcnn.licitacionTerceros
                            join lic in objcnn.licitaciones on licT.LTLicitacionId equals lic.LicId
                            join con in objcnn.constructoras on lic.LicConstructora equals con.ConstId
                            join ter in objcnn.terInfGeneral on licT.LTTerceroId equals ter.TigTerceroId
                            where licT.LTAsegurado == (op == -1 ? licT.LTAsegurado : op)
                            orderby licT.LTAsegurado ascending, lic.LicLicitacion ascending

                            select licT.MapCSAseguradoraDTO(lic, con, ter)).ToList();
                return data;


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseDTO actualizaEstado(CSAseguradoraDTO data)
        {
            ResponseDTO respuesta = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            try
            {
                var _obj = objcnn.licitacionTerceros.Where(x => x.LTId == data.Id).FirstOrDefault();
                if (_obj != null)
                {
                    _obj.LTAsegurado = data.Asegurado;
                }
                objcnn.licitacionTerceros.Update(_obj);
                objcnn.SaveChanges();
            }
            catch (Exception e)
            {

                respuesta.codigo = 1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }

        public ResponseDTO HabilitarCotizar(List<CSInvitadoDTO> data, int idConstructora)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {

                for (int i = 0; i < data.Count; i++)
                {
                    ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                    int terId_ = objcnn.terInfGeneral.Where(x => x.TigNumeroIdentificacion == data[i].NIT).FirstOrDefault().TigTerceroId;
                    int idLic_ = objcnn.licitaciones.Where(x => x.LicLicitacion == data[i].IdLicitacion).Where(x => x.LicConstructora == idConstructora).FirstOrDefault().LicId;
                    var _data = objcnn.licitacionTerceros.Where(x => x.LTTerceroId == terId_).Where(x => x.LTLicitacionId == idLic_).FirstOrDefault();
                    if (_data != null)
                    {
                        _data.LTEstado = 0;
                        objcnn.licitacionTerceros.Update(_data);
                        objcnn.SaveChanges();

                    }

                }
            }

            catch (Exception e)
            {
                respuesta.codigo = 1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }

    }
}
