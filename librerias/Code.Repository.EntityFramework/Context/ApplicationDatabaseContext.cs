using Code.Repository.EntityFramework.Models;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.adp_cs;
using Code.Repository.Model.Entity.apr;
using Code.Repository.Model.Entity.Auditoria.dbo;
using Code.Repository.Model.Entity.CS.Cotizaciones;
using Code.Repository.Model.Entity.CS.Licitaciones;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.Model.Entity.inf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;


namespace Code.Repository.EntityFramework.Context
{
    public class ApplicationDatabaseContext : DbContext
    {       
        public enum TipoAuditoria
        {
            InformacionGeneral = 1,
            Gerente = 2,
            CamaraComercio = 8,
            InformacionBancaria = 9,
            InformacionTributaria = 10,
            SISO = 11,
            Novedades = 12,
            DocumentosInformacionGeneral = 13,
            DatosNotificacionesProveedor = 14,
            DatosNotificacionesLicitaciones = 15,
            Especialidades = 16,
            AdminUsuarios = 17

        }

        public enum TipoDocumentoRequerido
        {

            Obligatorio = 0,
            Grupos = 1,
            Categorias = 2,
            Especialidad = 3

        }

        public void SaveChangesAuditoria(string _NewData, string _OldData, int _IdUser, int _IdDocumento, int _tipoAuditoria,
                                                          bool IsDelete, bool IsNew, string Opcion = "")
        {
            AuditoriaGeneral _Auditoria = new AuditoriaGeneral
            {
                Documento = _IdDocumento,
                IdUsuario = _IdUser,
                Opcion = Opcion,
                Fecha = int.Parse(DateTime.Now.ToString("yyyyMMdd")),
                NewData = _NewData,
                OldData = _OldData,
                TipoAuditoria = (int)_tipoAuditoria,
                Hora = int.Parse(DateTime.Now.ToString("HHmmss")),
                IsDelete = IsDelete,
                IsNew = IsNew

            };
            try
            {
                base.Entry(_Auditoria).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region Terceros

        public DbSet<TipoCuentaBancaria> tipoCuentaBancaria { get; set; }
        public DbSet<TerSISO> terSISO { get; set; }
        public DbSet<Bancos> bancos { get; set; }
        public DbSet<TerCuentaBancaria> terCuentaBancaria { get; set; }
        public DbSet<TerInfoTributaria> terInfotributaria { get; set; }
        public DbSet<TerEstado> terEstado { get; set; }
        public DbSet<Terceros> terceros { get; set; }
        public DbSet<TercerosConstructora> terceroconstructora { get; set; }
        public DbSet<TerInformacionGeneral> terInfGeneral { get; set; }
        public DbSet<TerDatosContacto> terDatosContacto { get; set; }

        public DbSet<TipoContactoTercero> TipoContactoTercero { get; set; }
        public DbSet<AdjuntoTercero> adjuntoTercero { get; set; }
        public DbSet<TipoAdjuntoTercero> tipoAdjuntoTercero { get; set; }
        public DbSet<TerEspecialidad> terEspecialidad { get; set; }
        public DbSet<TerCamaraComercio> terCamaraComercio { get; set; }
        public DbSet<ProductosProveedor> productosProveedor { get; set; }

        public DbSet<Novedades> novedades { get; set; }
        public DbSet<NovedadesDet> novedadesDet { get; set; }
        public DbSet<NovedadesUsuarios> novedadesUsuarios { get; set; }

        public DbSet<AdjuntosAprobados> adjuntosAprobados { get; set; }

        #endregion

        #region Usuarios

        public DbSet<NivelesA> perfil { get; set; }
        public DbSet<Accesos> accesos { get; set; }
        public DbSet<Usuario> usuario { get; set; }

        #endregion

        public DbSet<Ciudades> ciudad { get; set; }
        public DbSet<Constructora> constructoras { get; set; }
        public DbSet<Adjuntos> adjuntos { get; set; }
        public DbSet<ActividadEconomica> actividadEconomica { get; set; }

        #region Especialdiades TERCERO

        public DbSet<EspecialidadesTercero> especialidadTercero { get; set; }
        public DbSet<GruposTercero> gruposTercero { get; set; }
        public DbSet<CategoriasTercero> categoriasTercero { get; set; }

        #endregion


        public DbSet<AuditoriaLogin> auditoria { get; set; }

        #region Schema INF
        public DbSet<ColumnasInforme> columnasInforme { get; set; }
        public DbSet<Informe> informe { get; set; }
        public DbSet<ParametrosInforme> parametrosInforme { get; set; }
        #endregion

        #region APR

        public DbSet<RangoAprobacion> rangoAprobacion { get; set; }
        public DbSet<LogAprobaciones> logAprobaciones { get; set; }
        public DbSet<MotivoRechazo> motivoRechazo { get; set; }
        public DbSet<LogAprobacionesRechazo> logAprobacionesRechazo { get; set; }


        #endregion

        #region Menu

        public DbSet<Menu> menu { get; set; }
        public DbSet<Navegacion> navegacion { get; set; }
        #endregion

        #region Licitaciones
        public DbSet<CSLicitacion> licitaciones { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Notificacion> notificaciones { get; set; }
        public DbSet<NotificacionTercero> notificacionesTercero { get; set; }
        public DbSet<LicitacionTercero> licitacionTerceros { get; set; }

        #endregion

        #region Abastecimiento

        #region Licitaciones
        public DbSet<Licitacion> licitacion { get; set; }
        public DbSet<LicitacionActividad> licitacionActividad { get; set; }

        #endregion

        #region Cotizaciones

        #endregion


        public DbSet<CSCotizaciones> cotizaciones { get; set; }
        public DbSet<CSCotizacion> cotizacion { get; set; }
        public DbSet<CotizacionActividad> cotizacionActividad { get; set; }
        public DbSet<CotizacionAdjunto> cotizacionAdjunto { get; set; }
        #endregion

        #region Auditoria
        public DbSet<AuditoriaGeneral> AuditoriaGeneral { get; set; }
        public DbSet<TipoAuditoriaGeneral> TipoAuditoriaGeneral { get; set; }
        #endregion

        #region Notificaciones
        public DbSet<Notificaciones> datoContacto { get; set; }
        #endregion

        #region TOKEN CACHE

        public DbSet<TokenERP> tokenERP { get; set; }
        #endregion

        public DbSet<Monedas> monedas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            optionsBuilder.UseSqlServer(DbConexion._cnn);
            //}
        }




        public DataTable ExecuteStoreQuery(ProcedureDTO data)
        {
            var table = new DataTable();
            using (var ctx = this.Database.GetDbConnection())
            {
                var cmd = ctx.CreateCommand();
                cmd.CommandText = data.commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var item in data.parametros)
                {
                    DbParameter _param = cmd.CreateParameter();
                    _param.ParameterName = item.Key;
                    _param.Value = item.Value;

                    cmd.Parameters.Add(_param);
                }
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                table.Load(cmd.ExecuteReader());

                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return table;
        }

    }
}
