using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Adjuntos;
using Code.Repository.Model.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.Model.Mapper;
using Code.Repository.Model.DTO.Especialidades;
using Code.Repository.Model.MappAuditoria;
using static Code.Repository.EntityFramework.Context.ApplicationDatabaseContext;
using Newtonsoft.Json;
using Code.Repository.Model.DTOAuditoria;

namespace Code.Repository.Document.Operations
{
    public enum Tipodocumento
    {

        tercero,
        licitacion
    }

    public class AdjuntoBL
    {

        public IEnumerable<AdjuntoTerceroDTO> GetAdjuntoTerceroXTipoAdjunto(int id, string tipos)
        {
            IEnumerable<AdjuntoTerceroDTO> lstAdjuntos = GetAdjuntoTercero(id);

            List<int> tiposId = tipos
                .Split(',')
                .Select(int.Parse)
                .Distinct()
                .ToList();

            var _resultado = (from a in lstAdjuntos
                              join t in tiposId on a.tipoAdjunto.id equals t
                              select a).ToList();

            return _resultado;
        }


        public IEnumerable<AdjuntoTerceroDTO> GetAdjuntoTercero(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            List<AdjuntoTerceroDTO> adjuntosTercero = new List<AdjuntoTerceroDTO>();

            (from data in objcnn.tipoAdjuntoTercero

             select data.MapToDTO())
             .ToList()
             .ForEach(c =>
             {

                 adjuntosTercero.Add(new AdjuntoTerceroDTO()
                 {
                     tipoAdjunto = c,
                     adjunto = (from a in objcnn.adjuntos
                                join at in objcnn.adjuntoTercero on a.AjdId equals at.AjdTerIdAdjunto
                                where at.AjdTerTipo == c.id && at.AjdTerTerId == id
                                select a.MapToDTO())
                                .FirstOrDefault() ?? new AdjuntosDTO() { }
                 });
             });






            return adjuntosTercero;

        }


        public IEnumerable<TipoAdjuntoTerceroDTO> GetTipoadjuntotercero()
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from data in objcnn.tipoAdjuntoTercero select data.MapToDTO());

        }

        /// <summary>
        /// Metodo encargado de asociar los adjuntos entranter al tercero 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="tercero"></param>
        /// <param name="idTipo"></param>
        protected void AsociarDocumentosTercero(Adjuntos file, int tercero, int idTipo, int usuario)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _adjunto = objcnn.adjuntoTercero.Where(c => c.AjdTerTerId == tercero && c.AjdTerTipo == idTipo);

            var tipoDoc = objcnn.tipoAdjuntoTercero.Find(idTipo).TipAdjTexto;


            if (_adjunto.Count() > 0)
            {
                //Se obtiene el adjunto existente para su eliminacion de la tabla adjunto y reemplazo por el nuevo adjunto
                //var _adjuntoExistente = objcnn.adjuntos.Where(c => c.AjdId == _adjunto.FirstOrDefault().AjdTerIdAdjunto).FirstOrDefault();
                //objcnn.Entry(_adjuntoExistente).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                //objcnn.SaveChanges();

                //Se modifica el id del adjunto en la tabla adjunto tercero
                _adjunto.FirstOrDefault().AjdTerIdAdjunto = file.AjdId;


                var dataExistente = (from d in objcnn.adjuntoTercero
                                     join ta in objcnn.tipoAdjuntoTercero on d.AjdTerTipo equals ta.TipAdjId
                                     join a in objcnn.adjuntos on d.AjdTerIdAdjunto equals a.AjdId
                                     where d.AjdTerTerId == tercero
                                     select new TerdocumentosAuditoriaDTO
                                     {
                                         nombreDocumento = a.AjdNombre,
                                         nombreTipoDocumento = ta.TipAdjTexto
                                     }
                                     ).FirstOrDefault();

                Tuple<string, string> _datos = diferenciasAudit(file.MapToAuditoria(tipoDoc), dataExistente);


                objcnn.Entry(_adjunto.FirstOrDefault()).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.DocumentosInformacionGeneral, false, false, Opcion: "Documentos tercero");

            }
            else
            {
                Tuple<string, string> _datos = diferenciasAudit(file.MapToAuditoria(tipoDoc), new TerdocumentosAuditoriaDTO { });


                objcnn.adjuntoTercero.Add(new AdjuntoTercero()
                {
                    AjdTerTerId = tercero,
                    AjdTerIdAdjunto = file.AjdId,
                    AjdTerTipo = idTipo
                });

                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.DocumentosInformacionGeneral, false, true, Opcion: "Documentos tercero");

            }

        }

        public IList<AdjuntosDTO> GuardarArchivos(IFormFileCollection files, Tipodocumento tipo, int id, int id2, string rootPath, int IdUser)
        {
            List<AdjuntosDTO> objLst = new List<AdjuntosDTO>();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            string fechaCreacion = DateTime.Now.ToString("HH_mm_s");
            TerInformacionGeneral objInfoTercero = objcnn.terInfGeneral.Where(c => c.TigTerceroId == id).FirstOrDefault();
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


                Adjuntos adjunto = new Adjuntos();
                adjunto.Adjruta = RutaFullCompleta;
                adjunto.AjdExtension = extension;
                adjunto.AjdNombre = NombreArchivo;
                adjunto.AjdTipo = tipo.ToString();
                adjunto.AjdFechaCreacion = DateTime.Now;
                adjunto.AjdIdUsuario = IdUser;
                objResponse.Add(adjunto);

            }


            objResponse.ForEach(c => objcnn.adjuntos.Add(c));


            objcnn.SaveChanges();


            switch (tipo)
            {
                case Tipodocumento.tercero:
                    AsociarDocumentosTercero(objResponse.FirstOrDefault(), id, id2, IdUser);
                    break;
                case Tipodocumento.licitacion:
                    break;
                default:
                    break;
            }

            objResponse.ForEach(c => objLst.Add(c.MapToDTO()));

            return objLst;

        }


        public Adjuntos GetFile(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return objcnn.adjuntos.Find(id);
        }


        public IEnumerable<EspecialidadDTO> GetRequeridosGrupo(string categorias)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            List<int> TagIds = categorias.Split(',').Select(int.Parse).ToList();

            var _especialidades = objcnn.especialidadTercero.ToList();

            return (from e in _especialidades
                    join c in TagIds on e.EspIdCategoria equals c
                    select e.MapToDTO());

        }




        public Tuple<string, string> diferenciasAudit(Object newData, Object oldData)
        {
            Dictionary<string, string> valuesNew = new Dictionary<string, string>();
            Dictionary<string, string> valuesOld = new Dictionary<string, string>();
            Tuple<string, string> tupleAllValues = null;

            foreach (var valoresData in newData.GetType().GetProperties())
            {
                var _valueNew = newData.GetType().GetProperty(valoresData.Name).GetValue(newData, null);
                var _valueOld = oldData.GetType().GetProperty(valoresData.Name).GetValue(oldData, null);

                //Valida null 
                _valueNew = _valueNew == null ? string.Empty : _valueNew.ToString().Trim();
                _valueOld = _valueOld == null ? string.Empty : _valueOld.ToString().Trim();

                if (!_valueNew.Equals(_valueOld))
                {
                    valuesNew.Add(valoresData.Name, _valueNew.ToString());
                    valuesOld.Add(valoresData.Name, _valueOld.ToString());

                }

            }
            tupleAllValues = new Tuple<string, string>(JsonConvert.SerializeObject(valuesNew), JsonConvert.SerializeObject(valuesOld));
            return tupleAllValues;
        }


    }
}
