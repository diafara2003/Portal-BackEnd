using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.Model.Mapper;
using Code.Repository.Model.Mapper.Abastecimiento.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Mapper;
using Code.Repository.Model.DTO.ResponseCommon;

namespace Code.Repository.RepositoryBL.Operations.Abastecimiento.Cotizaciones
{
    public class CotizacionBL
    {
        public CSCotizacionCotDTO ConsultarDetalleCotizacion(int idProveedor, int idConstructora, int idLicitacion, int idCotizacion)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            int idLic = objcnn.licitaciones.Where(c => c.LicConstructora == idConstructora).Where(l => l.LicLicitacion == idLicitacion).FirstOrDefault().LicId;
            var data = objcnn.cotizaciones.Where(c => c.CLTerceroId == idProveedor).Where(c => c.CLLicitacionId == idLic).Where(c => c.CLCotizacion == idCotizacion).FirstOrDefault();

            return data.MapToDTO();
        }

        public IEnumerable<CSResumenCotizacionDTO> ConsultarCotizacionesxProveedor(int idConstructora, int idProveedor)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var cot_ = objcnn.cotizaciones.Where(x => x.CLTerceroId == idProveedor);

            var data_ = (from lic in objcnn.licitaciones
                         join emp in objcnn.constructoras on lic.LicConstructora equals emp.ConstId
                         join cat in objcnn.categorias on lic.LicCategoria equals cat.CatId
                         join cot in cot_ on lic.LicId equals cot.CLLicitacionId

                         select new CSResumenCotizacionDTO()
                         {
                             Cotizacion = cot.MapToDTO(),
                             Licitacion = lic.MapToDTO(emp, cat)
                         });
            return data_;
        }

        public CSDashboardCotDTO ConsultarResumenCotizaciones(int idProveedor, int idEmpresa)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Dictionary<string, object> _parametros = new Dictionary<string, object>();

            _parametros.Add("@IdProveedor", idProveedor);
            _parametros.Add("@IdEmpresa", idEmpresa);


            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "[CS].[ConsultarDashboardCotizaciones]",
                parametros = _parametros
            });




            return result.MapToDashboardCotDTO();
        }

        public object ConsultarInvitacionesAgrupadasxCategoria(int idProveedor, int idEmpresa)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var result_ = (
                                    from i in objcnn.licitacionTerceros
                                    join l in objcnn.licitaciones on i.LTLicitacionId equals l.LicId
                                    join cat in objcnn.categorias on l.LicCategoria equals cat.CatId
                                    where i.LTTerceroId == idProveedor
                                    group l by cat.CatDesc into c
                                    select new { Nombre = c.Key, Cantidad = c.Count() });
            return result_;
        }

        public object ConsultarEstadosAgrupados(int idProveedor, int idEmpresa)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var result_ = (
                                    from i in objcnn.licitacionTerceros
                                    join l in objcnn.licitaciones on i.LTLicitacionId equals l.LicId
                                    where i.LTTerceroId == idProveedor
                                    group l by l.LicEstado into c
                                    select new { Nombre = c.Key, Cantidad = c.Count() });
            return result_;
        }

        public ResponseDTO GuardarCotizacion(CSCotizacionCotDTO data)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            data.IdLicitacion = objcnn.licitaciones.Where(c => c.LicLicitacion == data.IdLicitacion).FirstOrDefault().LicId;
            var cot_ = objcnn.cotizaciones.Where(c => c.CLCotizacion == data.Cotizacion).FirstOrDefault();

            if (cot_ == null)
            {
                var _nuevo = data.MapToEntity();
                objcnn.cotizaciones.Add(_nuevo);
                objcnn.SaveChanges();
                data.IdCotizacion = _nuevo.CLId;
            }
            else
            {

                cot_.CLValor = data.Valor;
                cot_.CLFormaPago = data.FormaPago;
                cot_.CLTipoTributo = data.TipoTributo;
                cot_.CLEstado = data.Estado;

                objcnn.cotizaciones.Update(cot_);
                objcnn.SaveChanges();
            }


            return new ResponseDTO() { codigo = 0, mensaje = "Se guardó correctamente", Success = true };
        }

        public ResponseDTO ConfirmarCotizacion(int idConstructora, int idLicitacion, int idCotizacion)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            int licId = objcnn.licitaciones.Where(c => c.LicConstructora == idConstructora).Where(c => c.LicLicitacion == idLicitacion).FirstOrDefault().LicId;
            var cot_ = objcnn.cotizaciones.Where(c => c.CLCotizacion == idCotizacion).Where(l => l.CLLicitacionId == licId).FirstOrDefault();
            cot_.CLEstado = 1;
            objcnn.cotizaciones.Update(cot_);
            objcnn.SaveChanges();



            return new ResponseDTO() { codigo = 0, mensaje = "Se guardó correctamente", Success = true };
        }
    }


}
