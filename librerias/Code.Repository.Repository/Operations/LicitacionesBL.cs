using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Code.Repository.RepositoryBL.Operations
{
    public class LicitacionesBL : OperationsEF
    {
        public ResponseDTO GuardarResumen(LicitacionDTO data)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                var _data = objcnn.licitaciones.Where(x => x.LicLicitacion.Equals(data.IdLicitacion) && x.LicConstructora.Equals(data.IdConstructora)).FirstOrDefault();

                if (_data != null)
                {
                    _data = data.MapToLicitacion();
                    objcnn.licitaciones.Update(_data);
                    objcnn.SaveChanges();
                }
                else
                {
                    CSLicitacion _nuevo = data.MapToLicitacion();
                    objcnn.licitaciones.Add(_nuevo);
                    objcnn.SaveChanges();



                }

                respuesta.codigo = 0;
                respuesta.mensaje = "Se guardó correctamente";
                respuesta.Success = true;


            }
            catch (Exception e)
            {
                respuesta.codigo = 1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
            }
            return respuesta;

        }
        public ResponseDTO GuardarInvitacionTercero(List<LicitacionTerceroDTO> data, int idConst)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {

                for (int i = 0; i < data.Count; i++)
                {
                    ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                    int terId_ = objcnn.terInfGeneral.Where(x => x.TigNumeroIdentificacion == data[i].Nit).FirstOrDefault().TigTerceroId;
                    int idLic_ = objcnn.licitaciones.Where(x => x.LicLicitacion == data[i].IdLicitacion).Where(x => x.LicConstructora == idConst).FirstOrDefault().LicId;
                    var _data = objcnn.licitacionTerceros.Where(x => x.LTTerceroId == terId_).Where(x => x.LTLicitacionId == idLic_).FirstOrDefault();
                    if (_data == null)
                    {
                        var _nuevo = new Model.Entity.adp_cs.LicitacionTercero();
                        _nuevo.LTLicitacionId = idLic_;
                        _nuevo.LTTerceroId = terId_;
                        objcnn.licitacionTerceros.Add(_nuevo);
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

        public ResponseDTO CambiarEstadoCotizacion(int idConst_, int idLic_, int idProv_)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {

                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                idLic_ = objcnn.licitaciones.Where(x => x.LicLicitacion == idLic_).Where(x => x.LicConstructora == idConst_).FirstOrDefault().LicId;
                var _data = objcnn.licitacionTerceros.Where(x => x.LTTerceroId == idProv_).Where(x => x.LTLicitacionId == idLic_).FirstOrDefault();
                _data.LTEstado = 1;
                objcnn.SaveChanges();
                respuesta.codigo = 0;
                respuesta.mensaje = "Se cambió el estado correctamente";
                respuesta.Success = true;
            }

            catch (Exception e)
            {
                respuesta.codigo = 1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
            }
            return respuesta;

        }

        public IEnumerable<ListadoLicitacionDTO> ConsultarListadoxProveedor(int idConstructora, int idProveedor, bool vencer)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Dictionary<string, object> _parametros = new Dictionary<string, object>();

            _parametros.Add("IdProveedor", idProveedor);
            _parametros.Add("IdConstructora", idConstructora);
            _parametros.Add("ProximasVencer", vencer);



            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "CS.ConsultarListadoLicitaciones",
                parametros = _parametros
            });
            var data = (from d in result.AsEnumerable()
                        select new ListadoLicitacionDTO()
                        {
                            Asunto = (string)d["LicAsunto"],
                            CantActividades = (int)d["LicCantActividades"],
                            Ciudad = (string)d["LicCiudad"],
                            Cotizada = (int)d["Cotizada"],
                            EstadoCotizacion = (int)d["EstadoCot"],
                            EstadoLicitacion = (int)d["LicEstado"],
                            Fecha =  (string)d["LicFecha"],
                            FechaCierre = (string)d["LicFechaCierre"],
                            IdConstructora = (int)d["ConstId"],
                            IdCotizacion = (int)d["IdCotizacion"],
                            IdLicitacion = (int)d["LicLicitacion"],
                            NombreEspecialidad = (string)d["LicEspecialidad"],
                            NombreConstructora = (string)d["ConstNombre"],
                            NombreEstadoCot = (string)d["NEstadoCot"],
                            NombreEstadoLic = (string)d["NEstadoLic"],
                            Numero = (int)d["LicNumero"],
                            Proyecto = (string)d["LicProyecto"],
                            Valor = (decimal)d["LicVrEstimado"]

                        }).ToList();

            return data;
        }

        public object ConsultarLicitacionesxMes(int idConstructora, int idProveedor)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _data = objcnn.licitaciones.Where(x => x.LicConstructora == idConstructora).ToList();

            var x = (from p in _data
                     group p by new
                     {
                         MesDesc = p.LicFechaCierre.ToString("MMMM"),
                         MesN = Convert.ToInt32((p.LicFechaCierre).ToString("MM"))

                     } into g
                     select new { Mes = g.Key.MesDesc, MesN = g.Key.MesN, Cantidad = g.Count() }).ToList();

            return x.OrderBy(x => x.MesN);

        }

        public IEnumerable<LicitacionDTO> ConsultarListadoContructoraxProveedor(int idProveedor, int idConstructora)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _data = objcnn.licitaciones.Where(x => x.LicConstructora == idConstructora).ToList();
            var data = new List<LicitacionDTO>();
            foreach (var x in _data)
            {
                data.Add(x.MapToLicitacionDTO());
            }
            return data;
        }

        public IEnumerable<LicitacionDTO> ConsultarListadoOrdenFechaxProveedor(int idProveedorss)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _data = objcnn.licitaciones.ToList().OrderByDescending(x => x.LicFechaCierre).Take(10);
            var data = new List<LicitacionDTO>();
            foreach (var x in _data)
            {
                data.Add(x.MapToLicitacionDTO());
            }
            return data;
        }


        public object ConsultarCategoriasValor()
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _data = objcnn.licitaciones.GroupBy(item => item.LicCategoria)
                                .Select(group => group.Sum(item => item.LicVrEstimado)).ToList();

            return _data;
        }


    }

}
