using Code.Repository.EntityFramework.Abstract;
using Code.Repository.EntityFramework.Context;
using Code.Repository.EntityFramework.Models;
using Code.Repository.Model.DTO.ProductosProveedor;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations
{
    public class ProductosProveedorBL : OperationsEF
    {

        public ResponseDTO SincronizarProductosProveedor(int IdProveedor, int IdConstructora, string Xml)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {
                var procedureDTO = new ProcedureDTO();
                procedureDTO.commandText = "[PP].[Sincronizar]";
                procedureDTO.parametros.Add("idProveedor", IdProveedor);
                procedureDTO.parametros.Add("idConstructora", IdConstructora);
                procedureDTO.parametros.Add("Xml", Xml);
                var resultado = new DbConexion().ConsultarSPDR(procedureDTO);

                respuesta.codigo = (Convert.ToInt32(resultado.First()["COD"]));
                respuesta.mensaje = ((string)resultado.First()["MSN"]);
                respuesta.Success = true;
                if(respuesta.codigo ==-1)
                    respuesta.Success = false;

                return respuesta;

            } catch(Exception e)
            {
                respuesta.codigo = -2;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
                return respuesta;
            }
   

        }

        public ResponseDTO MigrarProductosProveedor(int IdProveedor, string Xml)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {
                var procedureDTO = new ProcedureDTO();
                procedureDTO.commandText = "[PP].[Migrar]";
                procedureDTO.parametros.Add("idProveedor", IdProveedor);
                procedureDTO.parametros.Add("idConstructora", -1);
                procedureDTO.parametros.Add("Xml", Xml);
                var resultado = new DbConexion().ConsultarSPDR(procedureDTO);

                respuesta.codigo = (Convert.ToInt32(resultado.First()["COD"]));
                respuesta.mensaje = ((string)resultado.First()["MSN"]);
                respuesta.Success = true;
                if (respuesta.codigo == -1)
                    respuesta.Success = false;

                return respuesta;

            }
            catch (Exception e)
            {
                respuesta.codigo = -2;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
                return respuesta;
            }

        }

        public IEnumerable<ProductosProveedorDTO> ConsultarListadoxConstructora(int idConstructora, int idProveedor)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            var _data = objcnn.productosProveedor.ToList();
            _data.Where(x => x.IdTercero == idProveedor && x.IdConstructora == idConstructora).OrderByDescending(x => x.PPDescripcion);
            var data = new List<ProductosProveedorDTO>();
            foreach (var x in _data)
            {
                data.Add(x.MapToProductosProveedorDTO());
            }
            return data;

        }

        public ResponseDTO ActualizarProducto(ProductosProveedorDTO data)
        {
            ResponseDTO respuesta = new();

            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                var _data = data.MapToProductosProveedor();
                bool existe = objcnn.productosProveedor.Any(x => x.Id==data.Id);

                if (existe)
                {
                    objcnn.Entry(_data).Property(x => x.PPDto).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPIva).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPValorSinIva).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPVigencia).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPObs).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPReferencia).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPEntregaMax).IsModified = true;
                    objcnn.Entry(_data).Property(x => x.PPDespachoMin).IsModified = true;

                    objcnn.SaveChanges();
                    respuesta.codigo = 0;
                    respuesta.mensaje = "Se actulizo correctamente";
                    respuesta.Success = true;
                    return respuesta;
                }
                else
                {
                    ProductosProveedor _nuevo = data.ConverToEntityProductosProveedor();
                    objcnn.productosProveedor.Add(_nuevo);
                    objcnn.SaveChanges();
                    respuesta.codigo = _nuevo.Id;
                    respuesta.mensaje = "Se guardó correctamente";
                    respuesta.Success = true;
                    return respuesta;
                }
               
                      

            }
            catch (Exception e)
            {
                respuesta.codigo =-1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
                return respuesta;

            }

        }
    }

}
