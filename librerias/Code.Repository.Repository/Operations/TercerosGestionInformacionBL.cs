using Code.Repository.Document.Operations;
using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Adjuntos;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.Model.MappAuditoria;
using Code.Repository.Model.Mapper;
using Code.Repository.RepositoryBL.Operations.Auditoria;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using static Code.Repository.EntityFramework.Context.ApplicationDatabaseContext;

namespace Code.Repository.RepositoryBL.Operations
{
    public class TercerosGestionInformacionBL : OperationsEF
    {

        /// <summary>
        /// Metodo encargado de consulta la informacion general segun empresa
        /// </summary>
        /// <param name="idTer"></param>
        /// <returns></returns>
        public TerInformacionGeneralDTO ConsultarInfGeneral(int idTer)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var informacion = (from info in objcnn.terInfGeneral
                               join tercero in objcnn.terceros on info.TigTerceroId equals tercero.Terid
                               where info.TigTerceroId == idTer
                               //&& info.TigTipoEmpresa == "p"
                               select info.MapToDTO("")).FirstOrDefault();

            if (informacion != null)
                //Fecha registro
                informacion.FechaRegistro = objcnn.terceros
                    .Where(c => c.Terid == idTer)
                    .FirstOrDefault().TerFechaRegistro;

            if (informacion != null)
                //actividad economica
                informacion.actividadEconomica = objcnn.actividadEconomica
                    .Where(c => c.ActEcId == informacion.actividadEconomica.id)
                    .FirstOrDefault()
                    .MaptoDTO();


            if (informacion != null)
                //ciudad
                informacion.Ciudad = objcnn.ciudad.Where(c => c.CiudID == informacion.Ciudad.id)
                .FirstOrDefault()
                .MapToDTO();


            return informacion;


        }

        public IEnumerable<ListadoEstadoTerccerosDTO> BuscarTerceros(int idEmpresa, string filtro = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IEnumerable<ListadoEstadoTerccerosDTO> objterceros = new List<ListadoEstadoTerccerosDTO>();

            if (string.IsNullOrEmpty(filtro) || filtro == "_")
                return (from tc in objcnn.terceroconstructora
                        join info in objcnn.terInfGeneral on tc.IdTercero equals info.TigTerceroId
                        join e in objcnn.terEstado on tc.Estado equals e.Id
                        where tc.IdConstructora == idEmpresa && info.TigTipoEmpresa == "p"

                        select info.MapToDTO(e)).Take(15);
            else

                return (from tc in objcnn.terceroconstructora
                        join info in objcnn.terInfGeneral on tc.IdTercero equals info.TigTerceroId
                        join e in objcnn.terEstado on tc.Estado equals e.Id
                        where tc.IdConstructora == idEmpresa && info.TigTipoEmpresa == "p"
                         && (info.TigNombre.Contains(filtro)
                         || info.TigNumeroIdentificacion.Contains(filtro))
                        select info.MapToDTO(e)).Take(15);
        }


        public IEnumerable<ListadoEstadoTerccerosDTO> ConsultarListadoTercero(int empresa, int idTer = 0, int estado = 0, int rows = 50, int page = 1)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IEnumerable<ListadoEstadoTerccerosDTO> objterceros = new List<ListadoEstadoTerccerosDTO>();

            if (idTer > 0)

                objterceros = (from tc in objcnn.terceroconstructora
                               join info in objcnn.terInfGeneral on tc.IdTercero equals info.TigTerceroId
                               join e in objcnn.terEstado on tc.Estado equals e.Id
                               where tc.IdConstructora == empresa && tc.Estado == estado && info.TigTipoEmpresa == "p"
                                && tc.IdTercero == idTer
                               orderby info.TigNombre.Trim()
                               select info.MapToDTO(e));

            else
                objterceros = (from tc in objcnn.terceroconstructora
                               join info in objcnn.terInfGeneral on tc.IdTercero equals info.TigTerceroId
                               join e in objcnn.terEstado on tc.Estado equals e.Id
                               where tc.IdConstructora == empresa && tc.Estado == estado && info.TigTipoEmpresa == "p"
                               orderby info.TigNombre.Trim()
                               select info.MapToDTO(e));

            return objterceros;

        }

        /// <summary>
        /// Metodo encargado de guardar la informacion general de la empresa
        /// </summary>
        /// <param name="data"></param>
        /// <param name="IdTer"></param>
        /// <returns></returns>
        public ResponseDTO ActualizaInfGeneral(TerInformacionGeneralDTO data, int IdTer, int IdUser)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var id = objcnn.terInfGeneral.Where(c => c.TigTerceroId == IdTer).FirstOrDefault();
            var _ciu = objcnn.ciudad.Find(data.Ciudad.id);
            var _act = objcnn.actividadEconomica.Find(data.actividadEconomica.id);
            var _NuevaInfAudit = data.MapToDTO(_ciu, _act);

            var _NuevaInfGeneral = data.MapToEntity();


            _NuevaInfGeneral.TigId = id.TigId;
            _NuevaInfGeneral.TigTerceroId = IdTer;
            _NuevaInfGeneral.TigTipoEmpresa = "p";
            ResponseDTO _response = new ResponseDTO();
            try
            {
                var _ciudad = objcnn.ciudad.Where(c => c.CiudID == id.TigCiudad).FirstOrDefault();
                var _actEcono = objcnn.actividadEconomica.Where(c => c.ActEcId == id.TigActEconomicaPri).FirstOrDefault();
                var _dataTable = (id.MapToDTO(_ciudad, _actEcono));

                objcnn = new ApplicationDatabaseContext();

                if (_dataTable != null)
                {
                    _NuevaInfGeneral.TigId = _dataTable.TigId;
                    _NuevaInfAudit.TigId = _dataTable.TigId;

                    Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_NuevaInfAudit, _dataTable);
                    string NewData = _datos.Item1;
                    string OldData = _datos.Item2;

                    objcnn.Entry(_NuevaInfGeneral).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    objcnn.SaveChangesAuditoria(NewData, OldData, IdUser, IdTer, (int)TipoAuditoria.InformacionGeneral, false, false, "Informacion General");
                }
                //else
                //{
                //    objcnn.terInfGeneral.Add(_NuevaInfGeneral);
                //    objcnn.SaveChanges();
                //}
                _response.codigo = 1;
                _response.mensaje = "Información guardada exitosamente.";
                _response.Success = true;
            }
            catch (Exception Ex)
            {
                _response.codigo = 0;
                _response.mensaje = Ex.Message;
                _response.Success = false;

            }
            return _response;
        }
        /// <summary>
        /// Metodo encargado de consultar la datos de los contactos
        /// </summary>
        /// <param name="idTer"></param>
        /// <param name="TipoDatosContacto"></param>
        /// <returns></returns>
        public List<TerDatosContactosDTO> ConsultarDatosContacto(int idTer, int TipoDatosContacto)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _response = objcnn.terDatosContacto
                .Where(t => t.TdcTerceroId == idTer
                        && t.TdcTipoContactoId == TipoDatosContacto)
                .ToList();

            if (_response != null)
            {

                List<TerDatosContactosDTO> data = new List<TerDatosContactosDTO>();

                _response.ForEach(c =>
                {
                    Ciudades _ciudad = new Ciudades();

                    if (c.TdcCiudad > 0)

                        _ciudad = objcnn.ciudad.Find(c.TdcCiudad);
                    data.Add(c.MapToDTO(_ciudad));
                });

                return data;
            }
            else
                return new List<TerDatosContactosDTO>();
        }
        /// <summary>
        /// Metodo encargado de guardar los datos de contacto
        /// </summary>
        /// <param name="data"></param>
        /// <param name="IdTer"></param>
        /// <returns></returns>
        public ResponseDTO GuardaDatosContacto(TerDatosContactosDTO data, int IdTer, int IdUser)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            TerDatosContacto _NuevoDatoContacto = data.MapToEntity();

            var _id = objcnn.terDatosContacto.Where(c => c.TdcTerceroId == IdTer && c.TdcTipoContactoId == data.TipoContactoId).FirstOrDefault();
            var _ciu = objcnn.ciudad.Find(data.Ciudad.id);
            var _NuevaDatosContactoAudit = data.MapToDTO(_ciu);

            _NuevoDatoContacto.TdcTerceroId = IdTer;
            ResponseDTO _response = new ResponseDTO();
            try
            {
                //var _dataTable = (id.MapToDTO_Audit(_ciudad));

                var _dataTable = objcnn.terDatosContacto.Where(t => t.TdcTerceroId == IdTer &&
                                                                    t.TdcTipoContactoId == data.TipoContactoId &&
                                                                    t.TdcId == data.Id).FirstOrDefault();

                objcnn = new ApplicationDatabaseContext();
                if (!data.isNew)
                {
                    var _ciudad = objcnn.ciudad.Where(c => c.CiudID == _id.TdcCiudad).FirstOrDefault();
                    var _tipoContacto = objcnn.TipoContactoTercero.Where(t => t.Id == data.TipoContactoId).FirstOrDefault();
                    var _dataAnterior = (_id.MapToDTO_Audit(_ciudad));


                    _NuevoDatoContacto.TdcTerceroId = _id.TdcTerceroId;
                    _NuevoDatoContacto.TdcId = _dataTable.TdcId;

                    _NuevaDatosContactoAudit.TdcId = _dataTable.TdcId;
                    _NuevaDatosContactoAudit.TdcTerceroId = _id.TdcTerceroId;

                    Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_NuevaDatosContactoAudit, _dataAnterior);
                    string NewData = _datos.Item1;
                    string OldData = _datos.Item2;

                    objcnn.Entry(_NuevoDatoContacto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    objcnn.SaveChangesAuditoria(NewData, OldData, IdUser, IdTer, (_tipoContacto.Id + 1), false, false, _tipoContacto.Texto);
                }
                else
                {
                    var _tipoContacto = objcnn.TipoContactoTercero.Where(t => t.Id == data.TipoContactoId).FirstOrDefault();
                    var _OldData = new TerDatosContacto
                    {
                        TdcCargo = "",
                        TdcCelular = "",
                        TdcCiudad = -1,
                        TdcCorreo = "",
                        TdcDireccion = "",
                        TdcId = -1,
                        TdcNombre = "",
                        TdcNumDocumento = "",
                        TdcTelefono = "",
                        TdcTerceroId = -1,
                        TdcTipoContactoId = -1
                    };

                    Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_NuevaDatosContactoAudit, _OldData);
                    string NewData = _datos.Item1;
                    string OldData = _datos.Item2;

                    objcnn.terDatosContacto.Add(_NuevoDatoContacto);
                    objcnn.SaveChangesAuditoria(NewData, OldData, IdUser, IdTer, (_NuevoDatoContacto.TdcTipoContactoId + 1), false, true, _tipoContacto.Texto);
                }
                _response.codigo = 1;
                _response.mensaje = "Información guardada exitosamente.";
                _response.Success = true;
            }
            catch (Exception Ex)
            {
                _response.codigo = 0;
                _response.mensaje = Ex.Message;
                _response.Success = false;

            }
            return _response;
        }
        /// <summary>
        /// Metodo encargado de eliminar los datos de un contacto especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDTO EliminarDatoContacto(int id, int _IdTer, int IdUser)
        {

            ResponseDTO respuesta = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            try
            {
                var _newData = new TerDatosContacto
                {
                    TdcCargo = "",
                    TdcCelular = "",
                    TdcCiudad = -1,
                    TdcCorreo = "",
                    TdcDireccion = "",
                    TdcId = -1,
                    TdcNombre = "",
                    TdcNumDocumento = "",
                    TdcTelefono = "",
                    TdcTerceroId = -1,
                    TdcTipoContactoId = -1
                };
                var dataContacto = objcnn.terDatosContacto.Where(t => t.TdcId == id).FirstOrDefault();
                var _tipoContacto = objcnn.TipoContactoTercero.Where(t => t.Id == dataContacto.TdcTipoContactoId).FirstOrDefault();
                var _ciu = objcnn.ciudad.Find(dataContacto.TdcCiudad);

                var _data = dataContacto.MapToDTO_Audit(_ciu);
                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_newData, _data);
                string NewData = _datos.Item1;
                string OldData = _datos.Item2;

                objcnn.Entry(objcnn.terDatosContacto.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                objcnn.SaveChangesAuditoria(NewData, OldData, IdUser, _IdTer, (dataContacto.TdcTipoContactoId + 1), true, false, _tipoContacto.Texto);

                respuesta.codigo = 1;
                respuesta.mensaje = "Datos eliminados exitosamente";
                respuesta.Success = true;

            }
            catch (Exception ex)
            {
                respuesta.codigo = 0;
                respuesta.mensaje = ex.Message;
                respuesta.Success = false;
                throw;
            }
            return respuesta;
        }

        public IEnumerable<TerCamaraComercioDTO> GetCamaraComercio(int idtercero)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            return (from data in objcnn.terCamaraComercio
                    where data.TerCamTerId == idtercero
                    select data.MapToDTO());

        }


        public TerCamaraComercioDTO GuardarCamaraComercio(TerCamaraComercioDTO data, int idtercero, int IdUser)
        {

            TerCamaraComercio info = data.MapToEntity();
            info.TerCamTerId = idtercero;
            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                var _dataTable = objcnn.terCamaraComercio.Where(t => t.TerCamTerId == idtercero &&
                                                                 t.TerCamId == data.id).FirstOrDefault();

                var _OldData = new TerCamaraComercio { TerCamId = -1, TerCamTerId = -1, TerCamCargo = "", TerCamDocumento = "", TerCamTipoDoc = "", TerCapNombre = "" };
                objcnn = new ApplicationDatabaseContext();

                if (_dataTable != null)
                {
                    info.TerCamId = _dataTable.TerCamId;
                    objcnn.Entry(info).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    objcnn.SaveChanges();
                    data.id = info.TerCamId;
                }
                else
                {
                    Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(info, _OldData);
                    string NewData = _datos.Item1;
                    string OldData = _datos.Item2;
                    objcnn.terCamaraComercio.Add(info);
                    objcnn.SaveChangesAuditoria(NewData, OldData, IdUser, idtercero, (int)TipoAuditoria.CamaraComercio, false, true, "Camara y comercio");
                    // objcnn.SaveChanges();

                    data.id = info.TerCamId;
                }

            }
            catch (Exception Ex)
            {

                data = null;
            }
            return data;
        }


        public void EliminarCamaraComercio(int id, int _IdTer, int IdUser)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var info = objcnn.terCamaraComercio.Where(t => t.TerCamId == id).FirstOrDefault();
            var _newData = new TerCamaraComercio { TerCamId = -1, TerCamTerId = -1, TerCamCargo = "", TerCamDocumento = "", TerCamTipoDoc = "", TerCapNombre = "" };


            if (info != null)
            {

                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_newData, info);
                string NewData = _datos.Item1;
                string OldData = _datos.Item2;
                objcnn.Entry(info).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                objcnn.SaveChangesAuditoria(NewData, OldData, IdUser, _IdTer, (int)TipoAuditoria.CamaraComercio, true, false, "Camara y comercio");
                //objcnn.SaveChanges();
            }


        }


        TerceroInformacionPortalDTO GetInfoTercero(int tercero, ApplicationDatabaseContext objcnn, bool adjuntos = true)
        {

            TerceroInformacionPortalDTO _tercero = new TerceroInformacionPortalDTO();

            _tercero.informacionGeneral = (from info in objcnn.terInfGeneral where info.TigTerceroId == tercero select info.MapToDTO(""))
                .FirstOrDefault() ?? new TerInformacionGeneralDTO() { };

            var _ciudad = objcnn.ciudad.Find(_tercero.informacionGeneral.Ciudad.id);

            if (_ciudad != null)
                _tercero.informacionGeneral.Ciudad = new Model.DTO.Ciudades.CiudadesDTO()
                {
                    id = _ciudad.CiudID,
                    nombre = _ciudad.CiuNombre,
                    codigo = _ciudad.CiuCodigo
                };


            _tercero.datosContacto = (from c in objcnn.terDatosContacto
                                      join ciu in objcnn.ciudad on c.TdcCiudad equals ciu.CiudID
                                      where c.TdcTerceroId == tercero
                                      select c.MapToDTO(ciu)).ToList() ?? new List<TerDatosContactosDTO>();

            _tercero.inscritosCamaraComercio = (from c in objcnn.terCamaraComercio where c.TerCamTerId == tercero select c.MapToDTO()).ToList() ?? new List<TerCamaraComercioDTO>();

            _tercero.especialidades = (from te in objcnn.terEspecialidad
                                       join e in objcnn.especialidadTercero on te.EspId equals e.EspId
                                       where te.TerId == tercero
                                       select e.MapToDTO()).ToList() ?? new List<Model.DTO.Especialidades.EspecialidadDTO>();

            if (adjuntos)
            {
                List<AdjuntoTerceroDTO> adjuntosTercero = new List<AdjuntoTerceroDTO>();

                (from data in objcnn.adjuntoTercero
                 where data.AjdTerTerId == tercero
                 select data)
                 .ToList()
                 .ForEach(c =>
                 {
                     var _a = (from a in objcnn.adjuntos
                               where a.AjdId == c.AjdTerIdAdjunto
                               select a.MapToDTO())
                                .FirstOrDefault() ?? new AdjuntosDTO() { };

                     if (_a.id > 0)
                     {
                         adjuntosTercero.Add(new AdjuntoTerceroDTO()
                         {
                             tipoAdjunto = objcnn.tipoAdjuntoTercero.Find(c.AjdTerTipo).MapToDTO(),
                             adjunto = _a
                         });
                     }
                 }
                 );

                _tercero.adjuntos = adjuntosTercero;
            }
            return _tercero;
        }


        public TerceroInformacionPortalDTO GetDatosTerceroXId(int idConstructora, int proveedor)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            int tercero = (from t in objcnn.terceros
                           join tc in objcnn.terceroconstructora on t.Terid equals tc.IdTercero
                           where tc.IdConstructora == idConstructora
                           && t.Terid == proveedor
                           select t.Terid).FirstOrDefault();

            return GetInfoTercero(tercero, objcnn);


        }


        public TerceroInfoDetalladaDTO GetDatosTercero(int proveedor)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            TerceroInfoDetalladaDTO objResponse = new TerceroInfoDetalladaDTO();

            var info = GetInfoTercero(proveedor, objcnn, adjuntos: false);

            objResponse.inscritosCamaraComercio = info.inscritosCamaraComercio;
            objResponse.informacionGeneral = info.informacionGeneral;
            objResponse.especialidades = info.especialidades;
            objResponse.datosContacto = info.datosContacto;
            objResponse.adjuntos = (from at in objcnn.adjuntoTercero
                                    join a in objcnn.adjuntos on at.AjdTerIdAdjunto equals a.AjdId
                                    where at.AjdTerTerId == proveedor
                                    select new AdjuntosConstructoraDTO()
                                    {
                                        IdAdjunto = a.AjdId,
                                        IdAdjuntoTercero = at.AjdTerTerId,
                                        TipoAdjunto = at.AjdTerTipo
                                    }).ToList();



            objcnn.Dispose();
            return objResponse;


        }
        public IEnumerable<TerceroInformacionPortalDTO> GetAllDatosTercero(int idConstructora)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<TerceroInformacionPortalDTO> objResponse = new List<TerceroInformacionPortalDTO>();

            IEnumerable<int> terceros = (from t in objcnn.terceros
                                         join tc in objcnn.terceroconstructora on t.Terid equals tc.IdTercero
                                         where tc.IdConstructora == idConstructora
                                        //&& list.Contains(tc.Estado)
                                        && tc.Estado != (int)EstadoTercero.Completada
                                         select t.Terid).ToList();



            foreach (var item in terceros)
            {
                objResponse.Add(GetInfoTercero(item, objcnn));
            }

            objcnn.Dispose();

            if (objResponse == null) return new List<TerceroInformacionPortalDTO>();
            return objResponse;


        }


        public IEnumerable<TerceroInformacionPortalDTO> GetDatosTercero(int idConstructora, int estado = 0)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<TerceroInformacionPortalDTO> objResponse = new List<TerceroInformacionPortalDTO>();

            IEnumerable<int> terceros = (from t in objcnn.terceros
                                         join tc in objcnn.terceroconstructora on t.Terid equals tc.IdTercero
                                         where tc.IdConstructora == idConstructora
                                         && tc.Estado == estado
                                         select t.Terid).ToList();



            foreach (var item in terceros)
            {
                objResponse.Add(GetInfoTercero(item, objcnn));
            }

            objcnn.Dispose();
            return objResponse;


        }

        public IEnumerable<TerInformacionGeneralDTO> GetDatosTercero(string filterId, int idConstructora)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<TerInformacionGeneralDTO> objResponse = new List<TerInformacionGeneralDTO>();

            List<int> TagIds = filterId.Split(',').Select(int.Parse).ToList();

            objResponse = (from t in objcnn.terceros
                           join tc in objcnn.terceroconstructora on t.Terid equals tc.IdTercero
                           join i in objcnn.terInfGeneral on t.Terid equals i.TigTerceroId
                           join c in objcnn.ciudad on i.TigCiudad equals c.CiudID
                           where tc.IdConstructora == idConstructora
                           select i.MapToDTO(c.CiuNombre)).ToList();


            objcnn.Dispose();

            return (from t in objResponse
                    join f in TagIds on t.id equals f
                    select t);


        }

        public TerInformacionGeneral getDataTercero(string numeroIdentificacion)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var _data = (from inf in objcnn.terInfGeneral where inf.TigNumeroIdentificacion == numeroIdentificacion select inf).FirstOrDefault();

            return _data;

        }
        public bool existeCorreoTercero(int idTer, string _correo)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _data = (from us in objcnn.usuario where us.UserIdPpal == idTer && us.UserCorreo.Contains(_correo) select us).ToList().Count() > 0;

            return _data;

        }



        public TerInfoTributaria GetInfoTributaria(int tercero)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return objcnn.terInfotributaria.FirstOrDefault(c => c.IdTercero == tercero) ?? new TerInfoTributaria()
            {
                Id = 0,
                IdTercero = tercero,
                AutoRetenedorICA = false,
                Autorretenedor = false,
                Declarante = false,
                GranContribuyente = false,
                ResponsableIVA = false
            }; ;
        }

        public ResponseDTO SaveInfoTributaria(TerInfoTributaria info)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            ResponseDTO objResponse = new ResponseDTO();

            if (info.Id == 0)
            {

                objcnn.terInfotributaria.Add(info);

                objResponse.mensaje = "Se guando correctamente.";
                objResponse.codigo = info.Id;
            }
            else
            {

                objcnn.Entry(info).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                objResponse.mensaje = "Se modifico correctamente.";
                objResponse.codigo = info.Id;
            }


            objcnn.SaveChanges();


            return objResponse;
        }

        public IEnumerable<TipoCuentaBancaria> GetTipoCuentaBancaria()
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return objcnn.tipoCuentaBancaria;
        }

        public IEnumerable<Bancos> GetBancos()
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return objcnn.bancos.Where(c => c.Estado);
        }

        public TerCuentaBancariaDTO GetcuentaBancaria(int tercero)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from tcb in objcnn.terCuentaBancaria
                    join b in objcnn.bancos on tcb.IdBanco equals b.Id
                    join c in objcnn.ciudad on tcb.IdCiudad equals c.CiudID
                    join tc in objcnn.tipoCuentaBancaria on tcb.TipoCuenta equals tc.Id
                    where tcb.IdTercero == tercero
                    select tcb.MapToDTO(b, c, tc)
                    ).FirstOrDefault() ?? new TerCuentaBancariaDTO
                    {
                        banco = "",
                        BancoTexto = "",
                        ciudad = 0,
                        ciudadTexto = "",
                        correoPagos = "",
                        id = 0,
                        numero = "",
                        tipoCuenta = 0,
                        tipoCuentaTexto = ""
                    };
        }

        public ResponseDTO SaveCuentaBancaria(TerCuentaBancariaDTO data, int tercero, int usuario)
        {
            ResponseDTO objResponse = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            if (!string.IsNullOrEmpty(data.banco))
                data.BancoTexto = objcnn.bancos.Find(data.banco).Texto;

            if (data.tipoCuenta > 0)
                data.tipoCuentaTexto = objcnn.tipoCuentaBancaria.Find(data.tipoCuenta).Codigo;

            if (data.ciudad > 0)
                data.ciudadTexto = objcnn.ciudad.Find(data.ciudad).CiuNombre;


            var TerCuentaBancaria = data.MapToEntity(tercero);

            bool isNew = false;

            if (data.id == 0) isNew = true;

            Tuple<string, string> _datos = null;

            if (isNew)
            {
                objcnn.terCuentaBancaria.Add(TerCuentaBancaria);
                objResponse.mensaje = "Se guando correctamente.";

                _datos = new AuditoriaBL().diferenciasAudit(data.MapToAuditoria(), new TerInfoBancariaDTOAuditoria() { });

            }
            else
            {
                var datacurrent = getAuditoriaInfoBancaria(TerCuentaBancaria.Id);

                objcnn.Entry(TerCuentaBancaria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                objResponse.mensaje = "Se modifico correctamente.";


                _datos = new AuditoriaBL().diferenciasAudit(data.MapToAuditoria(), datacurrent);

            }

            objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.InformacionBancaria, false, isNew, Opcion: "Informacion bancaria");


            objResponse.codigo = TerCuentaBancaria.Id;

            return objResponse;

        }

        TerInfoBancariaDTOAuditoria getAuditoriaInfoBancaria(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from cb in objcnn.terCuentaBancaria
                    join c in objcnn.ciudad on cb.IdCiudad equals c.CiudID
                    join tc in objcnn.tipoCuentaBancaria on cb.TipoCuenta equals tc.Id
                    join b in objcnn.bancos on cb.IdBanco equals b.Id
                    where cb.Id == id
                    select cb.MapToAuditoria(b, tc, c)
                                     ).FirstOrDefault();

        }

        public TerSISO GetInfoSISO(int tercero)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();



            return objcnn.terSISO.FirstOrDefault(c => c.IdTercero == tercero) ?? new TerSISO()
            {
                Id = 0,
                IdTercero = tercero,
                ProgramaAmbiental = false,
                ProgramaFactoresRiesgo = false,
                ProgramaSaludOcupacional = false,
                ProgramaSeguridadEhigiene = false,
                TieneComiteSO = false
            };
        }

        public ResponseDTO SaveInfoSISO(TerSISO info, int tercero, int usuario)
        {
            bool isNew = false;

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            ResponseDTO objResponse = new ResponseDTO();

            Tuple<string, string> _datos = null;

            if (info.Id == 0) isNew = true;

            var siso = info;

            if (isNew)
            {

                _datos = new AuditoriaBL().diferenciasAudit(info.MapToAuditoria(), new TerSISODTOAuditoria() { });

                objcnn.terSISO.Add(siso);
                objResponse.mensaje = "Se guando correctamente.";
                objResponse.codigo = info.Id;
            }
            else
            {

                var current = objcnn.terSISO.Find(info.Id);
                _datos = new AuditoriaBL().diferenciasAudit(info.MapToAuditoria(), current.MapToAuditoria());
                objcnn.Entry(siso).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                objResponse.mensaje = "Se modifico correctamente.";
                objResponse.codigo = info.Id;
            }

            objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.SISO, false, isNew, Opcion: "Informacion bancaria");


            return objResponse;

        }
    }
}
