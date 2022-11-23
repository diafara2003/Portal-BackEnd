using Code.Repository.Document.Operations;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Adjuntos;
using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Mapper;
using Code.Repository.RepositoryBL.Helper;
using Code.Repository.RepositoryBL.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Repository.RepositoryBL.Operations
{
    public enum TipoUsuario
    {
        Tercero = 'p',
        constructora = 'c'
    }


    public enum TipoRango
    {
        Nivel = 'n',
        constructora = 'u'
    }
    public class TercerosBL : OperationsEF
    {


        public bool checkDocumento(string NIT)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var _data = objcnn.terInfGeneral.Where(c => c.TigNumeroIdentificacion == NIT
                        ).FirstOrDefault();

            return _data != null ? true : false;

        }

        public bool checkEmail(string email)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var _data = objcnn.terInfGeneral.Where(c => c.TigCorreo == email
                        ).FirstOrDefault();

            return _data != null ? true : false;

        }



        public Tuple<TerceroDTO, bool> InvitacionTercero(InvitacionTerceroDTO data)
        {

            bool IsNewTercero = false;
            try
            {
                TerceroDTO objResultado = null;
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                Terceros _nuevo = new Terceros
                ()
                {
                    TerEmailConfirmado = false,
                    TerEstado = 1,
                    Terid = 0,
                    TerFechaRegistro = DateTime.Now,
                };

                //se valida si el tercero ya esta registrado
                if (!checkDocumento(data.documento))
                {
                    _nuevo.TerEstado = 1;
                    objcnn.terceros.Add(_nuevo);

                    objcnn.SaveChanges();

                    #region creacion del usuario del tercero
                    agregarUsuarioTercero(objcnn, new UserRegistrationDto()
                    {
                        correo = data.correo,
                        documento = data.documento,
                        Nombres = data.nombre,
                        Apellidos = data.apellido,
                        codigoVerificacion = data.tipoPersona.ToUpper().StartsWith("J")
                        ? data.codigoVerificacion is null || data.codigoVerificacion > 0 ? int.Parse(new CodigoVerificacion().CalcularDigitoVerificacion(data.documento)) : data.codigoVerificacion
                        : null

                    }, _nuevo.Terid, TipoUsuario.Tercero, isUserPpal: true, estado: estadoUsuario.Activo);
                    #endregion



                    #region Se identifica la ciudad del tercero

                    var ciudad = 0;
                    if (!string.IsNullOrEmpty(data.ciudad))
                    {
                        var ciudades = objcnn.ciudad.Where(c => c.CiuCodigo.Equals(data.ciudad));

                        if (ciudades.Count() > 0)
                            ciudad = ciudades.FirstOrDefault().CiudID;
                    }

                    #endregion

                    #region Se identifica el tipo de personsa

                    string tipoPersona = string.Empty;

                    if (!string.IsNullOrEmpty(data.tipoPersona))
                    {
                        if (data.tipoPersona.ToUpper().StartsWith("N")) tipoPersona = "N";
                        else if (data.tipoPersona.ToUpper().StartsWith("J")) tipoPersona = "J";
                        else tipoPersona = string.Empty;
                    }

                    #endregion

                    #region Informacion general del tercero
                    objcnn.terInfGeneral.Add(new TerInformacionGeneral()
                    {
                        TigTerceroId = _nuevo.Terid,
                        TigCorreo = data.correo,
                        TigNombre = data.nombre,
                        TerApellido = string.IsNullOrEmpty(data.apellido) ? string.Empty : data.apellido,
                        TigTipoPersona = tipoPersona,
                        TigTipoDocumento = data.tipoDocumento,
                        TigNumeroIdentificacion = data.documento,
                        TerDigitoVerificacion = data.tipoPersona.ToUpper().StartsWith("J")
                        ? data.codigoVerificacion is null || data.codigoVerificacion > 0 ? int.Parse(new CodigoVerificacion().CalcularDigitoVerificacion(data.documento)) : data.codigoVerificacion
                        : null,
                        //campos por defecto
                        TigCiudad = ciudad,
                        TigActEconomicaPri = 0,
                        TigTelefono = string.IsNullOrEmpty(data.telefono) ? string.Empty : data.telefono,
                        TigPaginaWeb = "",
                        TigCertificadoISO = false,
                        TigDireccion = "",
                        TigTipoEmpresa = "p",

                        TigId = 0

                    });
                    #endregion


                    objcnn.SaveChanges();
                    IsNewTercero = true;


                }

                objResultado = GetTerceroDocumento(data.documento);
                objcnn.Dispose();

                return new Tuple<TerceroDTO, bool>(objResultado, IsNewTercero);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public TerceroDTO GetTerceroDocumento(string documento)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from t in objcnn.terceros
                    join inf in objcnn.terInfGeneral on t.Terid equals inf.TigTerceroId
                    where inf.TigNumeroIdentificacion == documento
                    select t.MapToDTO(inf)
                             ).FirstOrDefault();

        }



        public TerceroDTO GetxCorreo(string correo)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from t in objcnn.terceros
                    join inf in objcnn.terInfGeneral on t.Terid equals inf.TigTerceroId
                    where inf.TigCorreo == correo
                    select t.MapToDTO(inf)
                                      ).FirstOrDefault();

        }


        public List<EstadoTercerosDTO> GetEstadoTercero(int id)
        {

            List<EstadoTercerosDTO> estadosCount = new List<EstadoTercerosDTO>();


            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var estados = Enum.GetValues(typeof(EstadoTercero));


            foreach (var item in estados)
            {

                EstadoTercero value = (EstadoTercero)item;
                int count = 0;

                count = objcnn.terceroconstructora.Count(c => c.Estado == (int)value && c.IdTercero == id);

                estadosCount.Add(new EstadoTercerosDTO()
                {
                    count = count,
                    nombre = item.ToString()
                });

            }


            return estadosCount;


        }

        void agregarUsuarioTercero(ApplicationDatabaseContext objcnn, UserRegistrationDto data, int terid, TipoUsuario tipo, bool isUserPpal = false, estadoUsuario estado = estadoUsuario.Inactivo)
        {
            objcnn.usuario.Add(new Usuario()
            {

                UserClave = Utilities.GenerarclaveRandom(),
                UserEstado = (int)estado,
                UserIdPpal = terid,
                UserCorreo = data.correo,
                UserPpal = isUserPpal,
                UserNivel = 4,
                UserCargo = "AdministradorPortal",
                UserDoc = data.documento,
                UserFechaRegistro = DateTime.Now,
                UserNombre = data.Nombres

            });

            objcnn.SaveChanges();
        }


        public TerceroDTO Add(UserRegistrationDto data)
        {
            try
            {
                TerceroDTO objResponse = new TerceroDTO();
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                Terceros _nuevo = new Terceros()
                {
                    Terid = 0,
                    TerEstado = (int)EstadoTercero.DocumentacionPendiente,
                    TerEmailConfirmado = false
                };

                objcnn.terceros.Add(_nuevo);

                objcnn.SaveChanges();

                agregarUsuarioTercero(objcnn, data, _nuevo.Terid, TipoUsuario.Tercero, estado: estadoUsuario.Activo);

                objcnn.terInfGeneral.Add(new TerInformacionGeneral()
                {
                    TigTerceroId = _nuevo.Terid,
                    TigCorreo = data.correo,
                    TigNombre = data.Nombres,
                    TerApellido = data.Apellidos,
                    TigTipoPersona = data.TipoPersona,
                    TigTipoDocumento = data.tipoDocumento,
                    TigNumeroIdentificacion = data.documento,
                    TerDigitoVerificacion = data.codigoVerificacion,

                    //campos por defecto
                    TigCiudad = 0,
                    TigActEconomicaPri = 0,
                    TigTelefono = "",
                    TigPaginaWeb = "",
                    TigCertificadoISO = false,
                    TigDireccion = "",
                    TigTipoEmpresa = "p"

                });

                objcnn.SaveChanges();


                objResponse = new TerceroDTO()
                {
                    id = _nuevo.Terid,
                    documento = data.documento,
                    correo = data.correo,
                    correoConfirmado = _nuevo.TerEmailConfirmado,
                    estado = (EstadoTercero)_nuevo.TerEstado
                };

                objcnn.Dispose();

                return objResponse;


            }
            catch (System.Exception)
            {

                throw;
            }


        }


        public void CambiarEstadoTercero(int idConstructora, int terecero, EstadoTercero estado)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var terCons = objcnn.terceroconstructora
            .Where(c => c.IdConstructora == idConstructora && c.IdTercero == terecero)
            .Single();
            terCons.Estado = (int)estado;

            objcnn.SaveChanges();

        }


        public void CambiarEstadoTercero(int idConstructora, List<CambiarEstadoTerCons> tereceros)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            tereceros.ForEach(t =>
            {
                var terCons = objcnn.terceroconstructora
                .Where(c => c.IdConstructora == idConstructora && c.IdTercero == t.id)
                .Single();
                terCons.Estado = t.estado;
            });


            /*enviar correo a los usuarios habilitados desde la opcion "datos notificaciones"*/

            objcnn.SaveChanges();

        }

        public void NuevoTerceroERP(int constructora, ResponseCrearProveedorDTO response)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            if (response != null)
            {
                List<int> tiposId = new List<int>();
                IEnumerable<AdjuntoTerceroDTO> lstAdjuntos = new AdjuntoBL().GetAdjuntoTercero(response.idTercero);

                var _split = response.tipos.Split(',');

                if (_split.Length > 0) _split.ToList().ForEach(c => tiposId.Add(int.Parse(c)));


                var _resultado = (from a in lstAdjuntos
                                  join t in tiposId on a.tipoAdjunto.id equals t
                                  select a.adjunto.id
                                  ).ToList();

                foreach (var t in _resultado)
                {
                    var _adjuntoTercero = objcnn.adjuntoTercero.FirstOrDefault(c => c.AjdTerIdAdjunto == t && c.AjdTerTerId == response.idTercero);
                    if (_adjuntoTercero != null)
                        objcnn.adjuntosAprobados.Add(new AdjuntosAprobados()
                        {
                            Id = 0,
                            IdAdjunto = t,
                            IdConstructora = constructora,
                            IdTercero = response.idTercero,
                            IdAdjuntoTercero = _adjuntoTercero.AdjTerId
                        });
                }



                var terCons = objcnn.terceroconstructora.Where(c => c.IdConstructora == constructora && c.IdTercero == response.idTercero).FirstOrDefault();
                terCons.Estado = (int)EstadoTercero.Completada;


                objcnn.SaveChanges();
            }
        }

        public IEnumerable<TerInformacionGeneral> ConsultarTercerosxEspecialidad(int idEspecialidad, int idConstructora)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var _resultado = (from tc in objcnn.terceroconstructora
                              join a in objcnn.terEspecialidad on tc.IdTercero equals a.TerId
                              join t in objcnn.especialidadTercero on a.EspId equals t.EspId
                              join tg in objcnn.terInfGeneral on a.TerId equals tg.TigTerceroId
                              where a.EspId == idEspecialidad && tc.IdConstructora == idConstructora
                              select tg).ToList();

            return _resultado;

        }



        public void AgregarContactosERP(List<DatosContactoERPDTO> contactos, TerceroDTO tercero)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            contactos.ForEach(c =>
            {
                var ciucod = objcnn.ciudad.Where(x => x.CiuCodigo == c.Ciudad).FirstOrDefault() ?? new Ciudades() { };

                objcnn.terDatosContacto.Add(new TerDatosContacto()
                {
                    TdcCargo = c.Cargo,
                    TdcCelular = c.Celular,
                    TdcCorreo = c.Correo,
                    TdcDireccion = c.Direccion,
                    TdcNombre = c.Nombre,
                    TdcTelefono = c.Telefono,
                    TdcTipoContactoId = c.tipo,
                    TdcId = 0,
                    TdcNumDocumento = c.NumeroDocumento,
                    TdcTerceroId = tercero.id,
                    TdcCiudad = ciucod.CiudID
                });
            });

            objcnn.SaveChanges();

        }
        public InvitacionTerceroRes InvitaAsociaEnviaMailTercero(InvitarTerceroFromERP request, Constructora _constructora, string webRootPath)
        {
            InvitacionTerceroRes _newInvitacion = new InvitacionTerceroRes();
            _newInvitacion.TerceroInvitacion = InvitacionTercero(request.informacion);

            if (request.datosContacto != null && request.datosContacto.Count > 0 && _newInvitacion.TerceroInvitacion.Item2)
                AgregarContactosERP(request.datosContacto, _newInvitacion.TerceroInvitacion.Item1);

            _newInvitacion.Mail = new List<string>();
            _newInvitacion.Mail.Add(request.informacion.correo);
            //se valida si el proveedor ya existia para enviar otro template

            _newInvitacion.Asunto = string.Empty;
            var ter = new UsuarioBL().GetUserMain(_newInvitacion.TerceroInvitacion.Item1.id);
            _newInvitacion.Asunto = $"Adproveedor - Invitación {_constructora.ConstNombre}";
            _newInvitacion.Template = new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
            {
                correo = request.informacion.correo,
                nombreEmpresa = _constructora.ConstNombre,
                clave = new UsuarioBL().GetClaveUser(ter.id),
                logoEMpresa = _constructora.ConstUrlLogo
            }, isnew: true);

            //IEmailSender.SendEmail(mail, asunto, template);

            new ConstucturasBL().AsociarTercero(_constructora.ConstId, _newInvitacion.TerceroInvitacion.Item1.id, (EstadoTercero)request.estado);
            return _newInvitacion;
        }
        public InvitacionTerceroRes creaUserEstadoIncompleto(InvitarTerceroFromERP data, int IdTer, Constructora _constructora, string webRootPath)
        {
            InvitacionTerceroRes _newInvitacion = new InvitacionTerceroRes();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            agregarUsuarioTercero(objcnn, new UserRegistrationDto()
            {
                correo = data.informacion.correo,
                documento = data.informacion.documento,
                Nombres = data.informacion.nombre,
                Apellidos = data.informacion.apellido,
                codigoVerificacion = data.informacion.tipoPersona.ToUpper().StartsWith("J")
                      ? data.informacion.codigoVerificacion is null || data.informacion.codigoVerificacion > 0 ? int.Parse(new CodigoVerificacion().CalcularDigitoVerificacion(data.informacion.documento)) : data.informacion.codigoVerificacion
                      : null

            }, IdTer, TipoUsuario.Tercero, isUserPpal: false, estado: estadoUsuario.Incompleto);
            _newInvitacion.Mail = new List<string>();
            _newInvitacion.Mail.Add(data.informacion.correo);
            _newInvitacion.Asunto = $"Adproveedor - Invitación {_constructora.ConstNombre}";
            _newInvitacion.Template = new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
            {
                correo = data.informacion.correo,
                nombreEmpresa = _constructora.ConstNombre,
                clave = new UsuarioBL().GetClaveUser(IdTer),
                logoEMpresa = _constructora.ConstUrlLogo
            }, isnew: true);

            return _newInvitacion;
        }
    }
}
