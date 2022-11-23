using Code.Repository.DAO.Context;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Novedades;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.Model.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations
{

    public enum EstadoNovedad
    {
        Activo = 1,
        Inactivo = 0
    }

    public enum TipoNotificacion
    {
        Proveedor = 1,
        Licitacion = 2
    }
    public class NovedadBL
    {

        public List<NovedadesDTO> GetNovedadTercero(int idTercero, int constructora,int idUsuario = -1)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _constructora = objcnn.constructoras.Find(constructora);

            List<NovedadesDTO> _novedad = (from n in objcnn.novedades

                                           where n.NovIdTercero == idTercero && n.NovIdConstructora == constructora
                                           orderby n.NovFecha descending
                                           select n.MapToDTO(_constructora.ConstNombre)
                                                 ).ToList();

            var nu = (from u in objcnn.novedades
                      join n in objcnn.novedadesUsuarios on u.NovId equals n.NUNovedadId
                      where n.NUUsuarioId == idUsuario
                      select u.MapToDTO(_constructora.ConstNombre)).ToList();

            nu.ForEach(n =>
            {
                _novedad.Add(n);
            });
            _novedad.ForEach(c =>
            {
                c.detalle = (from n in objcnn.novedadesDet
                             join d in objcnn.tipoAdjuntoTercero on n.NovDetIdTipoAdjujunto equals d.TipAdjId
                             where n.NovDetIdNov == c.numero && n.NovDetTipo == "documento"
                             select n.MapToDTO(d)).ToList();


                (from n in objcnn.novedadesDet
                 join d in objcnn.motivoRechazo on n.NovDetIdTipoAdjujunto equals d.id
                 where n.NovDetIdNov == c.numero && n.NovDetTipo == "formulario"
                 select n.MapToDTO(new TipoAdjuntoTercero()
                 {
                     TipAdjId = d.id,
                     TipAdjTexto = d.MotTexto
                 }))
                             .ToList()
                             .ForEach(f => c.detalle.Add(f));


            });

            return _novedad;
        }

        public async Task<ResponseDTO> CambiarEstado(CambiarEstadoNovedadDTO request)
        {
            ResponseDTO respuesta = new ResponseDTO();

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _novedad = objcnn.novedades.Find(request.codigo);

            _novedad.NovEstado = (int)EstadoNovedad.Inactivo;
            _novedad.NovVisto = 1;

            objcnn.Entry(_novedad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            objcnn.SaveChanges();

            var token = await new ConexionERP(_novedad.NovIdConstructora).ObtenerToken();
            var Const_ = new ConexionERP(_novedad.NovIdConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/PortalProveedorERP";

            var response_ = await new ConexionERP(_novedad.NovIdConstructora).Peticion(Const_.ConstRuta_API + url,
                HttpMethod.Post,
                 new
                 {
                     iddocumento = _novedad.NovLogAprid
                 }, token.ToString());


            return respuesta;
        }

        public NovedadesDTO GetNovedad(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            NovedadesDTO _novedad = (from n in objcnn.novedades
                                     join c in objcnn.constructoras on n.NovIdConstructora equals c.ConstId
                                     select n.MapToDTO(c.ConstNombre)
                                     ).FirstOrDefault();


            _novedad.detalle = (from n in objcnn.novedadesDet
                                join d in objcnn.tipoAdjuntoTercero on n.NovDetIdTipoAdjujunto equals d.TipAdjId
                                where n.NovDetIdNov == id
                                select n.MapToDTO(d)).ToList();

            return _novedad;
        }

        public ResponseDTO RegistrarNovedad(NovedadesDTO data, int consutrctora)
        {

            ResponseDTO respuesta = new ResponseDTO();

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            if (data.estadoTercero != (int)EstadoTercero.Aprobado)
            {

                var _novedad = new Model.Entity.dbo.Novedades()
                {
                    NovEstado = 1,
                    NovFecha = DateTime.Now,
                    NovId = 0,
                    NovIdConstructora = consutrctora,
                    NovIdTercero = data.tercero,
                    NovObservaciones = String.IsNullOrEmpty(data.comentario) ? "" : data.comentario,
                    NovLogAprid = data.numero,
                    NovTipo = 1

                };

                objcnn.novedades.Add(_novedad);


                objcnn.SaveChanges();

                respuesta.codigo = _novedad.NovId;

                if (data.detalle != null && data.detalle.Count() > 0)
                {
                    data.detalle.ForEach(c =>
                    {
                        objcnn.novedadesDet.Add(new Model.Entity.dbo.NovedadesDet()
                        {
                            Id = 0,
                            NovDetIdNov = _novedad.NovId,
                            NovDetIdTipoAdjujunto = c.tipoDocumento,
                            NovDetTipo = c.tipo
                        });
                    });

                    objcnn.SaveChanges();
                }




            }
            var terCons = objcnn.terceroconstructora.Where(c => c.IdConstructora == consutrctora && c.IdTercero == data.tercero).FirstOrDefault();

            if (terCons != null)
            {
                terCons.Estado = data.estadoTercero;

                objcnn.SaveChanges();
            }

            return respuesta;
        }


        public IEnumerable<NovedadesConstructoraDTO> ConsultaNovedadesTerceros(int IdTercero)
        {
            IEnumerable<NovedadesConstructoraDTO> NovedadesConst = new List<NovedadesConstructoraDTO>();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            NovedadesConst = (from nov in objcnn.novedades
                              join con in objcnn.constructoras on nov.NovIdConstructora equals con.ConstId
                              where nov.NovIdTercero == IdTercero && nov.NovEstado == (int)EstadoNovedad.Activo
                              group con by new
                              {
                                  con.ConstUrlLogo,
                                  con.ConstNombre,
                                  con.ConstId
                              } into newNov
                              select new NovedadesConstructoraDTO()
                              {
                                  contNotificaciones = newNov.Count(),
                                  nombreConst = newNov.Key.ConstNombre,
                                  logoConst = newNov.Key.ConstUrlLogo,
                                  ConstructoraId = newNov.Key.ConstId
                              }).ToList();




            return NovedadesConst.OrderBy(query => query.contNotificaciones);
        }
        /// <summary>
        /// Metodo encargado de consultar las constructoras con contador de novedades
        /// </summary>
        /// <param name="IdTercero"></param>
        /// <returns></returns>
        public IEnumerable<NovedadesConstructoraDTO> ConsultaConstructorasNovedadTer(int IdTercero)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var novedadCount = objcnn.novedades.Where(_nov => _nov.NovIdTercero == IdTercero && _nov.NovEstado == (int)EstadoNovedad.Activo)
                                .GroupBy(nov => nov.NovIdConstructora).Select(_novedad => new { _keyConst = _novedad.Key, count = _novedad.Count() });

            var constructoras = (from _tercero in objcnn.terceroconstructora
                                 join _contructora in objcnn.constructoras on _tercero.IdConstructora equals _contructora.ConstId
                                 where _tercero.IdTercero == IdTercero
                                 select new NovedadesConstructoraDTO()
                                 {
                                     contNotificaciones = (novedadCount.FirstOrDefault(_novCont => _novCont._keyConst == _contructora.ConstId)
                                                                ?? new { _keyConst = 0, count = 0 }).count,
                                     nombreConst = _contructora.ConstNombre,
                                     logoConst = _contructora.ConstUrlLogo,
                                     ConstructoraId = _contructora.ConstId
                                 }).ToList();

            return constructoras.OrderBy(query => query.contNotificaciones);
        }

        public ResponseDTO CambiarVistoNovedad(int IdConstructora)
        {
            ResponseDTO respuesta = new ResponseDTO();

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _novedades = objcnn.novedades.Where(n => n.NovIdConstructora.Equals(IdConstructora)).ToList();

            _novedades.ForEach(x => x.NovVisto = 1);

            objcnn.SaveChanges();


            return respuesta;

        }

        public ResponseDTO GuardarRegistroNovedad(NovedadRegistroDTO data, int constructora)
        {
            ResponseDTO respuesta = new ResponseDTO();

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var _novedad = new Model.Entity.dbo.Novedades()
            {
                NovEstado = 1,
                NovFecha = DateTime.Now,
                NovId = 0,
                NovIdConstructora = constructora,
                NovIdTercero = -1,
                NovObservaciones = String.IsNullOrEmpty(data.Mensaje) ? "" : data.Mensaje,
                NovLogAprid = data.IdDocumento,
                NovTipo = (int)TipoNotificacion.Licitacion,
                NovAsunto = data.Asunto
            };


            // 

            objcnn.novedades.Add(_novedad);
            objcnn.SaveChanges();




            respuesta.codigo = _novedad.NovId;
            return respuesta;


        }

        public ResponseDTO GuardarNovedadUsuarios(int idNovedad, List<int> usuarios)
        {
            var respuesta = new ResponseDTO();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            try
            {
                foreach (var item in usuarios)
                {
                    var novedadusuario = new Model.Entity.dbo.NovedadesUsuarios()
                    {
                        NUId = 0,
                        NUNovedadId = idNovedad,
                        NUUsuarioId = item,
                        NUVisto = false
                    };
                    objcnn.novedadesUsuarios.Add(novedadusuario);

                }
                objcnn.SaveChanges();
                respuesta.Success = true;
                respuesta.mensaje = "Se guardó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.mensaje = ex.Message;
            }
            return respuesta;
        }

        public List<int> ObtenerUsuariosxCorreo(List<string> correos)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var usuarios = (from usuario in objcnn.usuario where correos.Contains(usuario.UserCorreo) select usuario.UserId);

            return usuarios.ToList();
        }

    }
}
