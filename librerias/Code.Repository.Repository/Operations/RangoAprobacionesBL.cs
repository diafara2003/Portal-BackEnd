
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Aprobaciones.Rangos;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity.apr;
using Code.Repository.Model.Mapper;
using Code.Repository.Model.Mapper.Aprobaciones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Repository.RepositoryBL.Operations
{
    public class RangoAprobacionesBL
    {

        public ResponseDTO EliminarRango(int id)
        {

            ResponseDTO respuesta = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            objcnn.Entry(objcnn.rangoAprobacion.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            objcnn.SaveChanges();

            respuesta.codigo = 1;
            respuesta.mensaje = String.Empty;
            respuesta.Success = true;

            return respuesta;
        }

        public IEnumerable<RangoTerceroDTO> GetRangos(int empresa)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<RangoTerceroDTO> objConsulta = new List<RangoTerceroDTO>();
            List<RangoAprobacion> rangos = (from a in objcnn.rangoAprobacion
                                            where a.RAIdEmpresa == empresa
                                            select a).ToList();


            rangos.ForEach(c =>
            {
                string name = string.Empty;
                if (c.RAprTipo.ToLower() == "n")
                    //id del nivel de acceso
                    name = objcnn.perfil.Find(c.RAIdTipo).NivNombre;
                else name = objcnn.usuario.Find(c.RAIdTipo).UserCorreo;


                objConsulta.Add(c.MapToDTO(name));

            });

            return objConsulta;
        }


        public Tuple<ResponseDTO, RangoTerceroDTO> AgregarRango(int empresa, RangoTerceroDTO request)
        {

            ResponseDTO respuesta = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            Tuple<ResponseDTO, RangoTerceroDTO> objResponse = null;

            try
            {
                RangoAprobacion rango = request.MapToEntity();
                rango.RAIdEmpresa = empresa;

                if (rango.RAId == 0)
                    objcnn.rangoAprobacion.Add(rango);
                else
                    objcnn.Entry<RangoAprobacion>(rango).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                objcnn.SaveChanges();

                request.id = rango.RAId;

                if (rango.RAId == 0)
                    respuesta.mensaje = "Se guardó correctamente";
                else
                    respuesta.mensaje = "Se modifico correctamente";

                respuesta.Success = true;

            }
            catch (System.Exception e)
            {

                respuesta.codigo = 1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
            }


            objResponse = new Tuple<ResponseDTO, RangoTerceroDTO>(respuesta, request);

            return objResponse;
        }


        public IEnumerable<AProbacionRangoDTO> GetPerfilesAProbacion(int Idempresa, int documento)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<AProbacionRangoDTO> objAprobaciones = new List<AProbacionRangoDTO>();

            var rangos = (from r in objcnn.rangoAprobacion
                          join n in objcnn.perfil on r.RAIdTipo equals n.NivId
                          where r.RAIdEmpresa == Idempresa


                          select new
                          {
                              r.RAId,
                              n.NivNombre
                          }).ToList();

            rangos.ForEach(rango =>
            {

                AProbacionRangoDTO obj = new AProbacionRangoDTO();


                obj.nombrePerfil = rango.NivNombre;
                obj.aprobacion = (from a in objcnn.logAprobaciones
                                  join u in objcnn.usuario on a.LogIdUsuario equals u.UserId
                                  where a.LogIdEmpresa == Idempresa
                                  && a.LogIsAprobacion
                                  && a.LogIdDocumento == documento
                                  select a.MapToDTO(u.UserCorreo)).FirstOrDefault();


                obj.rechazo = (from a in objcnn.logAprobaciones
                               join u in objcnn.usuario on a.LogIdUsuario equals u.UserId
                               where a.LogIdEmpresa == Idempresa
                               && a.LogIsAprobacion == false
                               && a.LogIdDocumento == documento
                               select a.MapToDTO(u.UserCorreo)).FirstOrDefault();

                objAprobaciones.Add(obj);

            });


            return objAprobaciones;

        }



        public ResponseDTO AprobarTercero(AprobarTerceroDTO request, int empresa, int usuario)
        {
            ResponseDTO respuesta = new ResponseDTO();

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();




            string motivosStr = string.Empty;



            if (!request.isAprobado && (request.motivoRechazo != null && request.motivoRechazo.Count() > 0))
            {
                motivosStr = String.Join(",", request.motivoRechazo.Select(c => c.id.ToString()).ToArray());

            }

            Dictionary<string, object> _parametros = new Dictionary<string, object>();
            _parametros.Add("@documento", request.id);
            _parametros.Add("@empresa", empresa);
            _parametros.Add("@usuario", usuario);
            _parametros.Add("@isAprobacion", request.isAprobado);
            _parametros.Add("@comentarios", request.comentarios);
            _parametros.Add("@motivos", motivosStr);




            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "APR.[AprobarTercero]",
                parametros = _parametros
            });


            respuesta.Success = true;
            respuesta.codigo = (int)result.Rows[0]["codigo"];
            respuesta.mensaje = (string)result.Rows[0]["mensaje"];

            return respuesta;
        }


        public IEnumerable<MotivosRechazoDTO> GetMotivosRechazo()
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return objcnn.motivoRechazo.ToList().MapToDTO();
        }

    }
}
