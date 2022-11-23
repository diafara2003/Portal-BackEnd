using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Licitaciones
{
    public class LicitacionDTO
    {
        public int IdConstructora { get; set; }
        public int IdLicitacion { get; set; }
        public int Numero { get; set; }
        public string Fecha { get; set; }
        public string FechaCierre { get; set; }
        public string Asunto { get; set; }
        public int Categoria { get; set; }
        public string NombreCategoria { get; set; }
        public decimal Valor { get; set; }
        public int CantActividades { get; set; }
        public int Estado { get; set; }
        public int EstadoCotizacion { get; set; }
        public string Proyecto { get; set; }
        public string Ciudad { get; set; }
        public string NombreEmpresa { get; set; }
        public int Cotizada { get; set; }
        public decimal ValorCotizacion { get; set; }
        public int IdCotizacion { get; set; }

    }
    public class LicTipoAdjuntoDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Activo { get; set; }
    }

    public class AdjuntoDTO
    {
        public int ArchivoID { get; set; }
        //unico id
        public int OrigenID { get; set; }
        //los id van concatenados por ,
        public string OrigenIds { get; set; }
        public string Ruta { get; set; }
        public string NombreArchivo { get; set; }
        public int? Chequeo { get; set; }
        public string TipoArchivo { get; set; }
        public Byte[] CoAArchivo { get; set; }
        public int? CountOldArchivos { get; set; }
        public DateTime? Fecha { get; set; }
        public Byte[] ProAdjArchivo { get; set; }
        public string observaciones { get; set; }

    }

    public class ListadoLicitacionDTO
    {
        public int IdConstructora { get; set; }
        public string NombreConstructora { get; set; }
        public int IdLicitacion { get; set; }
        public int Numero { get; set; }
        public string Proyecto { get; set; }
        public string Ciudad { get; set; }
        public string Fecha{ get; set; }
        public string FechaCierre { get; set; }
        public string Asunto { get; set; }
        public string NombreEspecialidad { get; set; }
        public decimal Valor { get; set; }
        public int CantActividades { get; set; }
        public int EstadoLicitacion { get; set; }
        public string NombreEstadoLic { get; set; }
        public int EstadoCotizacion { get; set; }
        public string NombreEstadoCot { get; set; }
        public int Cotizada { get; set; }
        public decimal ValorCotizacion { get; set; }
        public int IdCotizacion { get; set; }

    }

}
