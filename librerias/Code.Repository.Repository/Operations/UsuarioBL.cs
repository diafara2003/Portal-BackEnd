using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity;
using Code.Repository.Model.MappAuditoria;
using Code.Repository.Model.Mapper;
using Code.Repository.RepositoryBL.Helper;
using Code.Repository.RepositoryBL.Operations.Auditoria;
using Code.Repository.RepositoryBL.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Code.Repository.EntityFramework.Context.ApplicationDatabaseContext;

namespace Code.Repository.RepositoryBL.Operations
{
    public class UsuarioBL : OperationsEF
    {

        public Usuario checkUser(string usuario)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            Usuario _data = (from u in objcnn.usuario where u.UserCorreo == usuario select u).FirstOrDefault();

            return _data;

        }


        public string cambiarClaveUsuario(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _user = objcnn.usuario.Find(id);
            string clave = Utilities.GenerarclaveRandom();

            _user.UserClave = clave;

            objcnn.Entry(_user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            objcnn.SaveChanges();


            return clave;
        }


        /// <summary>
        /// usuarios sin ninguin perfil en el sistema
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public IEnumerable<UsuariosSinPerfilDTO> GetUsersSinPerfil(int IdEmpresa)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (
                    from _usuarios in objcnn.usuario

                    where _usuarios.UserIdPpal == IdEmpresa
                        && _usuarios.UserPpal == false
                    //  && _usuarios.UserNivel == null
                    //&& m.UsuId == null
                    select _usuarios.MapToDTOPerfil()).Distinct();


        }


        public UsuarioDTO GetUserMain(int IdEmpresa)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Usuario expresion = new Usuario();

            expresion = (
                from _usuarios in objcnn.usuario
                where _usuarios.UserIdPpal == IdEmpresa
                    && _usuarios.UserPpal == true

                select _usuarios).FirstOrDefault();


            return expresion.MapToDTO();
        }

        public string GetClaveUser(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _user = objcnn.usuario.Find(id);

            return _user == null ? string.Empty : _user.UserClave;
        }

        public IEnumerable<UsuarioDTO> GetUsers(int IdEmpresa, string filter = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IEnumerable<Usuario> expresion = new List<Usuario>();


            expresion = (
                from _usuarios in objcnn.usuario
                where _usuarios.UserIdPpal == IdEmpresa
                    && _usuarios.UserPpal == false
                    && (string.IsNullOrEmpty(filter) ? _usuarios.UserNombre == _usuarios.UserCorreo || _usuarios.UserNombre == _usuarios.UserCorreo || _usuarios.UserDoc == _usuarios.UserDoc :
                    _usuarios.UserCorreo.Contains(filter) || _usuarios.UserNombre.Contains(filter) || _usuarios.UserDoc.Contains(filter))
                select _usuarios);


            return expresion.MapToDTO(); ;
        }


        public IEnumerable<UsuarioDTO> GetUsersAll(int IdEmpresa, string filter = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IEnumerable<Usuario> expresion = new List<Usuario>();

            expresion = (
                from _usuarios in objcnn.usuario
                where _usuarios.UserIdPpal == IdEmpresa

                    && (string.IsNullOrEmpty(filter) ? _usuarios.UserCorreo == _usuarios.UserCorreo :
            _usuarios.UserCorreo.Contains(filter))
                select _usuarios);


            return expresion.MapToDTO(); ;
        }

        public Tuple<ResponseDTO, UsuarioDTO> ActualizarDatos(UsuarioDTO request, int tercero, int usuario)
        {

            ResponseDTO response = new ResponseDTO();

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Usuario _user = objcnn.usuario.Find(usuario);

            Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(request.MapToAuditoria(), _user.MapToAuditoria());


            _user.UserCorreo = request.correo;
            _user.UserNombre = request.nombre;
            _user.UserCargo = request.cargo;
            _user.UserDoc = request.documento;
            _user.UserCelular = request.celular;

         
            Tuple<ResponseDTO, UsuarioDTO> resultado = new Tuple<ResponseDTO, UsuarioDTO>(response, request);

            objcnn.Entry(_user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;


            objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.AdminUsuarios, false, false, Opcion: "Mi perfil");

            return resultado;

        }
        public Tuple<ResponseDTO, UsuarioDTO> SaveUsuario(UsuarioDTO request, int tercero, int usuario)
        {
            ResponseDTO response = new ResponseDTO();
            Usuario _user = this.checkUser(request.correo);
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            if (request.id > 0)
            {

                var _usercurrently = objcnn.usuario.Find(request.id);

                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(request.MapToAuditoria(), _usercurrently.MapToAuditoria());


                if (_user != null)
                {
                    if (_user.UserId == request.id)
                    {
                        _usercurrently.UserCorreo = request.correo;
                        _usercurrently.UserNombre = request.nombre;
                        _usercurrently.UserCargo = request.cargo;
                        _usercurrently.UserDoc = request.documento;
                        _usercurrently.UserCelular = request.celular;

                        //todo:cambiar esto
                        _usercurrently.UserNivel = 4;

                        objcnn.Entry(_usercurrently).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.AdminUsuarios, false, false, Opcion: "Administracion de usuarios");

                    }
                    else
                    {
                        response.codigo = 0;
                        response.Success = false;
                        response.mensaje = "El correo ya existe en el sistema";
                    }
                }
                else
                {
                    _usercurrently.UserCorreo = request.correo;
                    _usercurrently.UserNombre = request.nombre;
                    _usercurrently.UserCargo = request.cargo;
                    _usercurrently.UserDoc = request.documento;
                    _usercurrently.UserCelular = request.celular;
                    //todo:cambiar esto
                    _usercurrently.UserNivel = 4;

                    objcnn.Entry(_usercurrently).State = Microsoft.EntityFrameworkCore.EntityState.Modified;


                    objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.AdminUsuarios, false, false, Opcion: "Administracion de usuarios");

                }

            }
            else
            {
                if (new UsuarioValidation().ExisteUsuario(request))
                {
                    response.codigo = 0;
                    response.Success = false;
                    response.mensaje = "El usuario ya existe en el sistema";
                }
                else
                {
                    Usuario _userRequest = request.MapToEntity();
                    _userRequest.UserClave = Utilities.GenerarclaveRandom();
                    _userRequest.UserFechaRegistro = DateTime.Now;
                    _userRequest.UserNivel = 4;
                    _userRequest.UserIdPpal = tercero;
                    _userRequest.UserEstado = 1;


                    Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(request.MapToAuditoria(), new UsuarioDTOAuditoria() { });

                    objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.AdminUsuarios, false, true, Opcion: "Administracion de usuarios");
                    request.id = _userRequest.UserId;
                }
            }
            objcnn.SaveChanges();
            Tuple<ResponseDTO, UsuarioDTO> resultado = new Tuple<ResponseDTO, UsuarioDTO>(response, request);

            return resultado;
        }


        public UsuarioDetalleDTO GetUserDetail(int empresa, int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            UsuarioDetalleDTO objUser = new UsuarioDetalleDTO();
            var user = objcnn.usuario.Find(id);
            objUser.informacion = user.MapToDTO();

            objUser.perfiles = (from n in objcnn.perfil
                                where n.NivEmpresa == empresa && user.UserNivel == n.NivId
                                select n.MapToDTO()).FirstOrDefault();

            return objUser;


        }

        public UsuarioDTO GetUser(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return objcnn.usuario.Find(id).MapToDTO();
        }

        public ResponseDTO CambiarEstadoUsuario(int usuario, estadoUsuario isActivo, int tercero, int usuarioSesion)
        {
            ResponseDTO objResultado = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _user = objcnn.usuario.Find(usuario);

            _user.UserEstado = (int)isActivo;


            Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(
                new UsuarioDTOAuditoria() { UserEstado = (int)isActivo },
                new UsuarioDTOAuditoria() { UserEstado = _user.UserEstado });


            objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuarioSesion, tercero, (int)TipoAuditoria.AdminUsuarios, false, true, Opcion: "Administracion de usuarios");

            return objResultado;
        }
        public ResponseDTO GuardarLogo(IFormFileCollection files, int idTercero, string rootPath)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            ResponseDTO response = new ResponseDTO();
            string fechaCreacion = DateTime.Now.ToString("HH_mm_s");
            TerInformacionGeneral objInfoTercero = objcnn.terInfGeneral.Where(c => c.TigTerceroId == idTercero).FirstOrDefault();
            Terceros objTercero = objcnn.terceros.Where(c => c.Terid == idTercero).FirstOrDefault();
            string NombreCarpeta = $"/Archivos/{objInfoTercero.TigNombre.Trim().TrimStart().TrimEnd().Replace(" ", "")}";
            string RutaCompleta = rootPath + NombreCarpeta;

            List<Adjuntos> objResponse = new List<Adjuntos>();

            if (!Directory.Exists(RutaCompleta))
            {
                //En caso de no existir se crea esa carpeta
                Directory.CreateDirectory(RutaCompleta);
            }

            foreach (var formFile in files)
            {

                string NombreArchivo = formFile.FileName;
                string RutaFullCompleta = Path.Combine(RutaCompleta, $"{fechaCreacion}_{NombreArchivo}");

                using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                var extension = Path.GetExtension(RutaFullCompleta);

                objTercero.TerRutaLogo = RutaFullCompleta;
                objTercero.TerNombreLogo = NombreArchivo;
                objcnn.Entry(objTercero).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            objcnn.SaveChanges();
            response.Success = true;
            response.codigo = 1;
            response.mensaje = RutaCompleta;

            return response;

        }
        /// <summary>
        /// Metodo encargado de cambiar la contraseña manuealmente
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="dataPass"></param>
        /// <returns></returns>
        public ResponseDTO cambiarClaveManual(int idUser, CambioClaveDTO dataPass)
        {
            ResponseDTO _response = new ResponseDTO { codigo = -1, mensaje = "", Success = false };
            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                var _user = objcnn.usuario.Find(idUser);
                if (_user != null)
                {
                    if (_user.UserClave != dataPass.PassOld)
                    {
                        _response.mensaje = "La contraseña anterior es incorrecta";
                        return _response;
                    }
                    else
                    {
                        if (dataPass.PassNew.Length < 6 || dataPass.PassNewR.Length < 6)
                        {
                            _response.mensaje = "La contraseña debe incluir minimo 6 caracteres";
                            return _response;
                        }
                        else
                        {
                            _user.UserClave = dataPass.PassNew;
                            objcnn.Entry(_user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            objcnn.SaveChanges();
                            _response.Success = true;
                            _response.codigo = 1;
                            _response.mensaje = "La contraseña se ha actualizado correctamente";
                            return _response;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return _response;
                throw;
            }
            return _response;
        }


    }
}