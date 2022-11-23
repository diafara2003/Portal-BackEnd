using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{
	[Table("Sincronizacion", Schema = "PP")]
	public class ProductosProveedor
	{
		[Key]
		public int Id   {get;set;}
		public int IdConstructora { get; set; }
		public int IdTercero { get; set; }
		public int ErpTercero { get; set; }
		public int ErpInsumo { get; set; }
		public int ErpZona { get; set; }
		public int PPCodErp { get; set; }
		public string PPCodBim { get; set; }
		public string PPDescripcion { get; set; }
		public string PPUm { get; set; }
		public decimal PPDto { get; set; }
		public decimal PPIva { get; set; }
		public decimal PPValorSinIva { get; set; }
		public DateTime PPVigencia { get; set; }
		public DateTime PPCotizacion { get; set; }
		public string PPObs { get; set; }
		public string PPReferencia { get; set; }
		public string PPZonaNombre { get; set; }
		public int PPEntregaMax { get; set; }
		public int PPDespachoMin { get; set; }

	}
}



