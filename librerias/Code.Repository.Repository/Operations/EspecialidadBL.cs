using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Especialidades;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Mapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Code.Repository.RepositoryBL.Operations.Auditoria;
using System;
using static Code.Repository.EntityFramework.Context.ApplicationDatabaseContext;

namespace Code.Repository.RepositoryBL.Operations
{
    public class EspecialidadBL
    {
        public IEnumerable<EspecialidadAcDTO> GetEspecialidadesFaltantes(int idTercero, string filter)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<EspecialidadAcDTO> objResult = new List<EspecialidadAcDTO>();

            var terEspecialidad = objcnn.terEspecialidad.Where(c => c.TerId == idTercero);



            var Especialidad = (from e in objcnn.especialidadTercero
                                join c in objcnn.categoriasTercero on e.EspIdCategoria equals c.CatId
                                join g in objcnn.gruposTercero on c.CatIdGrupo equals g.GruId
                                select new
                                {
                                    e,
                                    c.CatTexto,
                                    g.GruTexto
                                });


            if (!string.IsNullOrEmpty(filter))
                Especialidad = Especialidad.Where(c => c.e.EspTexto.ToLower().Contains(filter.ToLower()));


            Especialidad.ToList().ForEach(c => objResult.Add(c.e.MapToDTO(c.CatTexto, c.GruTexto)));


            return objResult.Take(ActividadEconomicaBL.maxRegisterAutoComplete);

        }

        public IEnumerable<EspecialidadDTO> GetEspecialidades()
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from e in objcnn.especialidadTercero
                    orderby e.EspTexto ascending
                    select e.MapToDTO());
        }


        public IEnumerable<EspecialidadesGruposDTO> GetAllEspecialdiades()
        {
            List<EspecialidadesGruposDTO> objLst = new List<EspecialidadesGruposDTO>();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();



            Dictionary<string, object> _parametros = new Dictionary<string, object>();


            var result = objcnn.ExecuteStoreQuery(new EntityFramework.Models.ProcedureDTO()
            {
                commandText = "[dbo].[EspecialidadesAll]",
            });

            objLst = (from d in result.AsEnumerable()

                      select new EspecialidadesGruposDTO
                      {
                          categoria = (int)d["categoria"],
                          especialidad = (int)d["especialidad"],
                          grupo = (int)d["grupo"],
                          texto = (string)d["texto"]
                      }).ToList();

            //1.grupos
            //objcnn.gruposTercero.ToList().ForEach(g =>
            //{
            //    objLst.Add(new EspecialidadesGruposDTO()
            //    {
            //        grupo = g.GruId,
            //        texto = g.GruTexto
            //    });


            //    //2. categorias
            //    var _categproaXGrupo = objcnn.categoriasTercero.Where(c => c.CatIdGrupo == g.GruId).ToList();

            //    _categproaXGrupo.ForEach(cg =>
            //    {
            //        objLst.Add(new EspecialidadesGruposDTO()
            //        {
            //            grupo = cg.CatIdGrupo,
            //            categoria = cg.CatId,
            //            texto = cg.CatTexto
            //        });



            //        //3. especialidades
            //        var _categproaXEspecialidad = objcnn.especialidadTercero.Where(e => e.EspIdCategoria == cg.CatId).ToList();

            //        _categproaXEspecialidad.ForEach(ec =>
            //        {
            //            objLst.Add(new EspecialidadesGruposDTO()
            //            {
            //                grupo = cg.CatIdGrupo,
            //                categoria = ec.EspIdCategoria,
            //                especialidad = ec.EspId,
            //                texto = ec.EspTexto
            //            });
            //        });

            //    });

            //});

            return objLst;
        }


        public IEnumerable<EspecialidadDTO> GetGrupos(string filter = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            if (string.IsNullOrEmpty(filter))
                return (from e in objcnn.gruposTercero
                        orderby e.GruTexto ascending
                        select e.MapToDTO());
            else
                return (from e in objcnn.gruposTercero
                        orderby e.GruTexto ascending
                        where e.GruTexto.ToLower().Contains(filter.ToLower())
                        select e.MapToDTO()).Take(ActividadEconomicaBL.maxRegisterAutoComplete);
        }

        public IEnumerable<EspecialidadDTO> GetCategorias(int grupo, string filter = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<CategoriasTercero> source = new List<CategoriasTercero>();

            if (grupo > 0)
                source = objcnn.categoriasTercero.Where(c => c.CatIdGrupo == grupo).ToList();
            else
                source = objcnn.categoriasTercero.ToList();

            if (string.IsNullOrEmpty(filter))

                return (from e in source
                        orderby e.CatTexto ascending
                        select e.MapToDTO());
            else
                return (from e in source
                        where e.CatTexto.ToLower().Contains(filter.ToLower())
                        orderby e.CatTexto ascending
                        select e.MapToDTO()).Take(ActividadEconomicaBL.maxRegisterAutoComplete);
        }


        public IEnumerable<EspecialidadDTO> GetEspecialidadxCategorias(int categoria = 0, string filter = "")
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            List<EspecialidadesTercero> objEspecialidades = objcnn.especialidadTercero.ToList();


            if (categoria > 0)
                objEspecialidades = objEspecialidades.Where(c => c.EspIdCategoria == categoria).ToList();


            if (!string.IsNullOrEmpty(filter))
                objEspecialidades = objEspecialidades.Where(c => c.EspTexto.ToLower().Contains(filter)).Take(ActividadEconomicaBL.maxRegisterAutoComplete).ToList();

            return (from e in objEspecialidades
                    select e.MapToDTO());

        }


        public IEnumerable<EspecialidadDTO> GetEspecialidades(string filter)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            List<EspecialidadesTercero> Especialidad = null;

            if (!string.IsNullOrEmpty(filter))
                Especialidad = objcnn.especialidadTercero.Where(c => c.EspTexto.Contains(filter)).Take(ActividadEconomicaBL.maxRegisterAutoComplete).ToList();
            else Especialidad = objcnn.especialidadTercero.ToList();

            return (from e in Especialidad
                    select e.MapToDTO()).ToList();

        }


        public IEnumerable<EspecialidadAcDTO> GetEspecialidades(int idTercero)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            return (from e in objcnn.especialidadTercero
                    join c in objcnn.categoriasTercero on e.EspIdCategoria equals c.CatId
                    join g in objcnn.gruposTercero on c.CatIdGrupo equals g.GruId
                    join et in objcnn.terEspecialidad on e.EspId equals et.EspId
                    where et.TerId == idTercero
                    orderby c.CatTexto ascending, e.EspTexto ascending
                    select e.MapToDTO(c.CatTexto, g.GruTexto));


        }
        public void AsociarEspecialidad(List<AsociarEspecialidadDTO> especialidades, int tercero)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            objcnn.terEspecialidad.Where(c => c.TerId == tercero).ToList().ForEach(c =>
            {
                objcnn.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            });

            especialidades.ForEach(e =>
            {
                TerEspecialidad te = new TerEspecialidad();

                te.EspId = e.id;
                te.TerId = tercero;

                objcnn.terEspecialidad.Add(te);
            });

            objcnn.SaveChanges();
        }

        public void AsociarEspecialidad(AsociarEspecialidadDTO especialidades, int tercero,int usuario)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _Especialidad = objcnn.especialidadTercero.Find(especialidades.id);
            var _Cateroria = objcnn.categoriasTercero.Find(_Especialidad.EspIdCategoria);
            var _Grupo = objcnn.gruposTercero.Find(_Cateroria.CatIdGrupo);

            string espcComplete = $"{_Grupo.GruTexto} / {_Cateroria.CatTexto} / {_Especialidad.EspTexto}";

            Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit( new { TerEspecialidad = espcComplete }, new { TerEspecialidad = "" });

            TerEspecialidad te = new TerEspecialidad();

            te.id = 0;
            te.EspId = especialidades.id;
            te.TerId = tercero;

            objcnn.terEspecialidad.Add(te);

            objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.Especialidades, false, true, Opcion: "Especialidades");

        }


        public void EliminarEspecialidad(int id, int tercero,int usuario)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _Especialidad = objcnn.especialidadTercero.Find(id);
            var _Cateroria = objcnn.categoriasTercero.Find(_Especialidad.EspIdCategoria);
            var _Grupo = objcnn.gruposTercero.Find(_Cateroria.CatIdGrupo);

            string espcComplete = $"{_Grupo.GruTexto} / {_Cateroria.CatTexto} / {_Especialidad.EspTexto}";

            TerEspecialidad te = objcnn.terEspecialidad.Where(c => c.EspId == id && c.TerId == tercero).FirstOrDefault();

            Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(new { TerEspecialidad = "" }, new { TerEspecialidad = espcComplete });


            if (te != null)
            {
                objcnn.Entry(te).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, usuario, tercero, (int)TipoAuditoria.Especialidades, true, false, Opcion: "Especialidades");

            }
        }
    }
}
