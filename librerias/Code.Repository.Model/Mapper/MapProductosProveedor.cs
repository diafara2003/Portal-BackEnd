using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.ProductosProveedor;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper
{
    public static class MapProductosProveedor
    {
        public static ProductosProveedor MapToProductosProveedor(this ProductosProveedorDTO register)
        {
            return new ProductosProveedor()
            {
                Id = register.Id,
                IdConstructora = register.IdConstructora,
                IdTercero = register.IdTercero,
                ErpTercero = register.TerceroERP,
                ErpInsumo = register.IdProducto,
                ErpZona = register.IdZona,
                PPCodErp = register.IdRegister,
                PPDescripcion = register.Descripcion,
                PPUm = register.Um,
                PPDto = register.Dto,
                PPIva= register.Iva,
                PPValorSinIva = register.ValorSinIva,
                PPVigencia = register.FechaVigencia,
                PPCotizacion= register.FechaCotizacion,
                PPObs = register.Obs,
                PPReferencia = register.Referencia,
                PPZonaNombre = register.ZonaNombre,
                PPEntregaMax = register.EntregaMax, 
                PPCodBim= register.IdBim,
                PPDespachoMin=register.CantidadMinima
               
            };
        }

        public static ProductosProveedorDTO MapToProductosProveedorDTO(this ProductosProveedor register)
        {
            return new ProductosProveedorDTO()
            {
              
                Id=register.Id,
                IdConstructora = register.IdConstructora,
                IdTercero = register.IdTercero,
                TerceroERP= register.ErpTercero,
                IdProducto = register.ErpInsumo,
                IdZona = register.ErpZona,
                IdRegister = register.PPCodErp,
                Descripcion = register.PPDescripcion,
                Um = register.PPUm,
                Dto = register.PPDto,
                Iva = register.PPIva,
                ValorSinIva = register.PPValorSinIva,
                FechaVigencia = register.PPVigencia,
                FechaCotizacion=register.PPCotizacion,
                Obs = register.PPObs,
                Referencia = register.PPReferencia,
                ZonaNombre = register.PPZonaNombre,
                EntregaMax = register.PPEntregaMax,
                IdBim = register.PPCodBim,
                CantidadMinima=register.PPDespachoMin
            };
        }

        public static ProductosProveedor ConverToEntityProductosProveedor(this ProductosProveedorDTO register)
        {
            return new ProductosProveedor()
            {
                IdConstructora = register.IdConstructora,
                IdTercero = register.IdTercero,
                ErpTercero= register.TerceroERP,
                ErpInsumo = register.IdProducto,
                ErpZona = register.IdZona,
                PPCodErp = register.IdRegister,
                PPCodBim = register.IdBim,
                PPDescripcion = register.Descripcion,
                PPUm = register.Um,
                PPDto = register.Dto,
                PPIva = register.Iva,
                PPValorSinIva = register.ValorSinIva,
                PPVigencia = register.FechaVigencia,
                PPCotizacion=register.FechaCotizacion,
                PPObs= register.Obs,
                PPReferencia = register.Referencia,
                PPZonaNombre = register.ZonaNombre,
                PPEntregaMax = register.EntregaMax,
                PPDespachoMin=register.CantidadMinima
            };
        }
    }
}

