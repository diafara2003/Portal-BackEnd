using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Auditoria;
using Code.Repository.Model.Entity.Auditoria.dbo;
using Code.Repository.Model.Mapper.Auditoria;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations.Auditoria
{
    public class AuditoriaBL : OperationsEF
    {
        public enum TipoAuditoria
        {
            InformacionGeneal = 1
        }

        /// <summary>
        /// Metodo encargado de obtener la auditoria por ultimos campos modificados
        /// </summary>
        /// <param name="_Documento"></param>
        /// <param name="_idTipAuditora"></param>
        /// <param name="_IsDelete"></param>
        /// <param name="_IsNew"></param>
        /// <returns></returns>
        public IEnumerable<AuditDTO> getAuditoria(int _Documento, int _idTipAuditora, bool _IsDelete, bool _IsNew)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IList<AuditDTO> listaCamposAudit = new List<AuditDTO>();
            TipoAuditoriaGeneral _glosario = new TipoAuditoriaGeneral();
            _glosario = GetTipoAuditoria(_idTipAuditora);

            if (_glosario.GlosarioTipoAuditoria != null)
            {
                var listGlosario = JsonConvert.DeserializeObject<List<Glosario>>(_glosario.GlosarioTipoAuditoria);

                var dataAudit = (from aud in objcnn.AuditoriaGeneral
                                 join usu in objcnn.usuario on aud.IdUsuario equals usu.UserId
                                 where aud.Documento == _Documento && aud.TipoAuditoria == _idTipAuditora
                                       && aud.IsDelete == _IsDelete && aud.IsNew == _IsNew
                                 orderby aud.Fecha descending, aud.Hora descending
                                 select aud.MapToDTO(usu)).ToList();


                foreach (var itemGlosario in listGlosario)
                {
                    AuditDTO campo = new AuditDTO();
                    var contVerMas = 0;
                    foreach (var itemAuditoria in dataAudit)
                    {
                        if (itemAuditoria.OldData.Contains(itemGlosario.NombreSQL) == true)
                        {
                            if (contVerMas == 0)
                            {
                                campo.Id = itemAuditoria.Id;
                                campo.Documento = itemAuditoria.Documento;
                                campo.Fecha = itemAuditoria.Fecha;
                                campo.Opcion = itemAuditoria.Opcion;
                                campo.Tipo = itemAuditoria.Tipo;
                                campo.Hora = itemAuditoria.Hora;
                                campo.nameUsuario = itemAuditoria.nameUsuario;

                                campo.Valores = new Valores
                                {
                                    Old = JObject.Parse(itemAuditoria.OldData)[itemGlosario.NombreSQL].ToString(),
                                    New = JObject.Parse(itemAuditoria.NewData)[itemGlosario.NombreSQL].ToString()
                                };

                                campo.Glosario = new Glosario
                                {
                                    NombreHTML = itemGlosario.NombreHTML,
                                    NombreSQL = itemGlosario.NombreSQL
                                };
                                campo.IsDelete = itemAuditoria.IsDelete;
                                campo.IsNew = itemAuditoria.IsNew;
                                // listaCamposAudit.Add(campo);
                            }
                            contVerMas++;
                            //break;
                        }
                    }
                    if (campo.Glosario != null)
                    {
                        campo.CountVerMas = contVerMas;
                        listaCamposAudit.Add(campo);
                    }

                }
            }
            return listaCamposAudit;
        }
        /// <summary>
        /// Metodo encargado de consultar la auditoria por campos a detalle
        /// </summary>
        /// <param name="_idTercero"></param>
        /// <param name="_glosario"></param>
        /// <returns></returns>
        public IEnumerable<AuditDTO> DetalleCampoAuditoria(int _Documento, Glosario _glosario, int _idTipoAuditoria, bool _IsDelete, bool _IsNew)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            IEnumerable<AuditoriaGeneralDTO> dataAudit = new List<AuditoriaGeneralDTO>();
            IList<AuditDTO> listaCamposAudit = new List<AuditDTO>();


            dataAudit = (from aud in objcnn.AuditoriaGeneral
                         join usu in objcnn.usuario on aud.IdUsuario equals usu.UserId
                         where aud.Documento == _Documento
                               && aud.OldData.Contains(_glosario.NombreSQL)
                               && aud.TipoAuditoria == _idTipoAuditoria
                               && aud.IsDelete == _IsDelete && aud.IsNew == _IsNew
                         orderby aud.Fecha descending, aud.Hora descending
                         select aud.MapToDTO(usu)).ToList();

            foreach (var itemAuditoria in dataAudit)
            {
                AuditDTO campo = new AuditDTO();
                if (itemAuditoria.OldData.Contains(_glosario.NombreSQL) == true)
                {
                    campo.Id = itemAuditoria.Id;
                    campo.Documento = itemAuditoria.Documento;
                    campo.Fecha = itemAuditoria.Fecha;
                    campo.Opcion = itemAuditoria.Opcion;
                    campo.Tipo = itemAuditoria.Tipo;
                    campo.Hora = itemAuditoria.Hora;
                    campo.nameUsuario = itemAuditoria.nameUsuario;

                    campo.Valores = new Valores
                    {
                        Old = JObject.Parse(itemAuditoria.OldData)[_glosario.NombreSQL].ToString(),
                        New = JObject.Parse(itemAuditoria.NewData)[_glosario.NombreSQL].ToString()
                    };

                    campo.Glosario = new Glosario
                    {
                        NombreHTML = _glosario.NombreHTML,
                        NombreSQL = _glosario.NombreSQL
                    };
                    campo.IsDelete = itemAuditoria.IsDelete;
                    campo.IsNew = itemAuditoria.IsNew;
                    listaCamposAudit.Add(campo);
                }
            }

            return listaCamposAudit;
        }

        /// <summary>
        /// Metodo encargado de obtener el tipo de auditoria con su respectivo glosario
        /// </summary>
        /// <param name="_tipAuditoria"></param>
        /// <returns></returns>
        public TipoAuditoriaGeneral GetTipoAuditoria(int _tipAuditoria)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            TipoAuditoriaGeneral responseTip = new TipoAuditoriaGeneral();
            responseTip = objcnn.TipoAuditoriaGeneral.Where(ta => ta.IdTipoAuditoria == _tipAuditoria).FirstOrDefault();
            return responseTip ?? new TipoAuditoriaGeneral() { };
        }

        /// <summary>
        /// Metodo encargado de validar las diferencias entre los datos anteriores y nuevos crear el JSON de new y old data
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="oldData"></param>
        /// <returns></returns>
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
