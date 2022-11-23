using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.EntityFramework.Abstract
{
    //public class MigrationHistotyContext : HistoryContext
    //{
        //public MigrationHistotyContext(DbConnection dbConnection, string defaultSchema)
        //    : base(dbConnection, defaultSchema)
        //{

        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<HistoryRow>().ToTable(tableName: "MigrationHistory", schemaName: "ADP");
        //    modelBuilder.Entity<HistoryRow>().Property(c => c.MigrationId).HasColumnName("Id");
        //}
    //}
}
