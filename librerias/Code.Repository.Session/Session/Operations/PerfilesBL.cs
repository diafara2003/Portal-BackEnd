
using System.Linq;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Login;
using System.Collections.Generic;
using Code.Repository.Model.Mapper;
using System;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Session.ValidationsBL;
using Code.Repository.Model.Entity;
using Code.Repository.Model.DTO.Usuarios;

namespace Code.Repository.Session.Operations
{
    public class PerfilesBL
    {

        public IEnumerable<PerfilConsultaDTO> GetPerfilconsulta(int empresa, int usuario = 0, int perfil = 0, string filter = "")
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            IEnumerable<PerfilConsultaDTO> perfiles = new List<PerfilConsultaDTO>();


            if (usuario > 0)
            {
                perfiles =
                    (from n in objcnn.perfil
                     where n.NivEmpresa == empresa 
                     select n).ToList().MapToDTOConsulta();
            }


            else if (perfil > 0)
            {
                perfiles =
                    (from n in objcnn.perfil
                     where n.NivId == perfil && n.NivEmpresa == empresa                    
                     select n).ToList().MapToDTOConsulta();
            }
            else
            {
                perfiles =
                    (from n in objcnn.perfil
                     where n.NivEmpresa == empresa                    
                      && (string.IsNullOrEmpty(filter) ? n.NivNombre == n.NivNombre : n.NivNombre.Contains(filter))
                     select n).ToList().MapToDTOConsulta();
            }


            //objcnn = new ApplicationDatabaseContext();

            foreach (var item in perfiles)
            {
                int count = objcnn.usuario.Count(c => c.UserNivel == item.id && c.UserIdPpal == empresa);
                item.countUsuarios = count;
            }

            return perfiles;
        }


        public Tuple<PerfilDTO, IEnumerable<int>> GetPerfil(int empresa, int perfil)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            Tuple<PerfilDTO, IEnumerable<int>> obj = null;

            PerfilDTO infoPerfil = (from p in objcnn.perfil
                                    where p.NivEmpresa == empresa && p.NivId == perfil                                   

                                    select p.MapToDTO()
                                    ).Single();


            IEnumerable<int> usuarios = objcnn.usuario
                .Where(c => c.UserNivel == perfil && c.UserIdPpal == empresa)
                .Select(c => c.UserId);

            obj = new Tuple<PerfilDTO, IEnumerable<int>>(infoPerfil, usuarios);

            return obj;
        }

        public Tuple<ResponseDTO, PerfilDTO> AgregarPerfil(AgregarPerfilDTO request, int empresa)
        {
            Tuple<ResponseDTO, PerfilDTO> objResult = null;
            ResponseDTO validaciones = new PerfilVal().AgregarPerfil(request, empresa);

            if (validaciones.Success)
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                NivelesA nivel = request.perfil.MapToEntity();
                nivel.NivEmpresa = empresa;               

                if (nivel.NivId == 0)
                    objcnn.perfil.Add(nivel);
                else objcnn.Entry<NivelesA>(nivel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;


                objcnn.usuario.Where(c => c.UserNivel == nivel.NivId).ToList().ForEach(n =>
                {
                    n.UserNivel = null;
                    objcnn.Entry(n).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                });

                objcnn.SaveChanges();

                request.usuarios.ToList().ForEach(u =>
                {
                    var _user = objcnn.usuario.Find(u);

                    if (_user != null)
                    {
                        _user.UserNivel = nivel.NivId;
                        objcnn.Entry(_user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                });

                objcnn.SaveChanges();

                objResult = new Tuple<ResponseDTO, PerfilDTO>(new ResponseDTO()
                {
                    mensaje = "Se guardo correctamente"

                }, request.perfil);
            }
            else
            {
                objResult = new Tuple<ResponseDTO, PerfilDTO>(validaciones, request.perfil);
            }

            return objResult;
        }


        public void CambiarEstado(PerfilDTO request)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var _perfil = objcnn.perfil.Find(request.id);

            _perfil.NivEstado = request.estado;

            objcnn.Entry(_perfil).State = Microsoft.EntityFrameworkCore.EntityState.Modified;


            objcnn.usuario.Where(c => c.UserNivel == request.id).ToList().ForEach(u =>
            {

                u.UserEstado = request.estado  ? (int)estadoUsuario.Activo : (int)estadoUsuario.Inactivo;
            });

            objcnn.SaveChanges();



        }
    }
}
