
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;

namespace Code.Repository.Model.Mapper
{
    public static class MapTerCuentaBancaria
    {

        public static TerCuentaBancariaDTO MapToDTO(this TerCuentaBancaria entity, Bancos banco, Ciudades ciudad, TipoCuentaBancaria tipoCuenta)
        {
            return new TerCuentaBancariaDTO()
            {
                banco = banco.Id,
                BancoTexto = banco.Texto,
                ciudad = ciudad.CiudID,
                ciudadTexto = ciudad.CiuNombre,
                correoPagos = entity.CorreoNotificaPago== null ?string.Empty: entity.CorreoNotificaPago,
                id = entity.Id,
                numero = entity.Numero==null ? string.Empty: entity.Numero,
                tipoCuenta = entity.TipoCuenta,
                tipoCuentaTexto = tipoCuenta.Codigo

            };
        }


        public static TerCuentaBancaria MapToEntity(this TerCuentaBancariaDTO DTO, int tercero)
        {
            return new TerCuentaBancaria()
            {
                CorreoNotificaPago = DTO.correoPagos,
                Id = DTO.id,
                IdBanco = DTO.banco,
                IdCiudad = DTO.ciudad,
                IdTercero = tercero,
                Numero = DTO.numero,
                TipoCuenta = DTO.tipoCuenta
            };
        }
    }
}
