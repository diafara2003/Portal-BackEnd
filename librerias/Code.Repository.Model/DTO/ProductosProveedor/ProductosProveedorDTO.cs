using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.ProductosProveedor
{
    public class ProductosProveedorDTO
    {
		public int Id { get; set; }
		public int IdTercero { get; set; }
		public int IdConstructora { get; set; }
		public int TerceroERP { get; set; }
		public int IdProducto { get; set; }
		public int IdZona { get; set; }
		public int IdRegister { get; set; }
		public string Descripcion { get; set; }
		public string Um { get; set; }
		public decimal Dto { get; set; }
		public decimal Iva { get; set; }
		public decimal ValorSinIva { get; set; }
		public DateTime FechaVigencia { get; set; }
		public string Obs { get; set; }
		public string Referencia { get; set; }
		public string ZonaNombre { get; set; }
		public int EntregaMax { get; set; }

		public string IdBim { get; set; }
		public DateTime FechaCotizacion { get; set; }
		public int CantidadMinima { get; set; }

	}

	public class SolicitudProductosProveedorDTO
    {

		public int Id { get; set; }
		public int IdTercero { get; set; }
		public DateTime Fecha { get; set; }
		public int Estado { get; set; }
		public int UsuAprob { get; set; }
		public bool Sincronizado { get; set; }

	}

	public class SolicitudProductosProveedorDetDTO
	{

		public int DetId { get; set; }
		public int Id { get; set; }
		public int IdPortal { get; set; }
		public int IdProdProveedor { get; set; }
		public string DetIdBim { get; set; }
		public int IdProducto { get; set; }
		public string DetDescripcion { get; set; }
		public string DetUm { get; set; }
		public string DetReferencia { get; set; }
		public decimal DetDto { get; set; }
		public decimal DetIva { get; set; }
		public decimal DetValorSinIva { get; set; }
		public DateTime DetFechaVigencia { get; set; }
		public DateTime DetFechaCotizacion { get; set; }
		public int DetEntregaMax { get; set; }
		public int DetIdZona { get; set; }
		public string DetNombreZona { get; set; }
		public int DetEstado { get; set; }
		public int DetUsuario { get; set; }

	}

	public class MigracionProductosProveedorDTO
	{

		public string Ciudad { get; set; }
		public int Codigo { get; set; }
		public string Descripcion { get; set; }
		public decimal Dto { get; set; }
		public DateTime FechaVigencia { get; set; }
		public int Id { get; set; }
		public decimal Iva { get; set; }
		public string Observacion { get; set; }
		public string Referencia { get; set; }
		public string UM { get; set; }
		public decimal Valor { get; set; }
		public int DiasMaxEntrega { get; set; }
		public int CantMinDespacho { get; set; }
		public int IdConstructora { get; set; }
    }
}
