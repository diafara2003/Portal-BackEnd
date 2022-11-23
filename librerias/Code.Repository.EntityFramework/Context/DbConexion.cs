using Microsoft.Extensions.Configuration;
using Code.Repository.EntityFramework.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.EntityFramework.Context
{
    public class DbConexion : IDisposable
    {
        SqlConnection con;
         public static string _cnn =string.Empty;

        public SqlConnection DBConexion()
        {
         
            con = new SqlConnection(_cnn);
            con.Open();
            return con;
        }

        public DataTable ConsultarSPDT(ProcedureDTO obj)
        {
            DataTable ds = new DataTable();
            using (SqlConnection context = DBConexion())
            {
                SqlCommand cmd = new SqlCommand(obj.commandText.Trim(), context);
                if (obj.parametros != null)
                {
                    foreach (var item in obj.parametros)
                    {
                        SqlParameter objsp = new SqlParameter();
                        objsp.ParameterName = item.Key;
                        objsp.Value = item.Value;

                        cmd.Parameters.Add(objsp);
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(ds);

                }
                catch (Exception E)
                {
                    if (context.State != ConnectionState.Closed)
                    {
                        context.Close();
                    }
                    throw new ArgumentException(E.Message + E.Source + E.StackTrace + E.TargetSite + E.HelpLink + E.HelpLink);
                }
                context.Close();
            }
            return ds;
        }

        public IList<Dictionary<string, object>> ConsultarSPDR(ProcedureDTO obj)
        {
            DataTable ds = new DataTable();
            IList<Dictionary<string, object>> source = new List<Dictionary<string, object>>();
            IList<string> colums = new List<string>();
            using (SqlConnection context = DBConexion())
            {
                SqlCommand cmd = new SqlCommand(obj.commandText.Trim(), context);
                if (obj.parametros != null)
                {
                    foreach (var item in obj.parametros)
                    {
                        SqlParameter objsp = new SqlParameter(item.Key, item.Value);
                        cmd.Parameters.Add(objsp);
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        colums.Add(rdr.GetName(i));
                    }
                    while (rdr.Read())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        for (int i = 0; i < colums.Count; i++) { data.Add(colums[i], rdr[colums[i]]); }
                        source.Add(data);
                    }
                }
                context.Close();
                return source;
            }
        }

        public void Dispose()
        {
            if (this.con != null && this.con.State != ConnectionState.Closed)
                this.con.Close();

        }
    }
}
