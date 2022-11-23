using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.MappAuditoria
{
    public static class MapTerInfoBancariaAuditoria
    {

        public static TerInfoBancariaDTOAuditoria MapToAuditoria(this TerCuentaBancariaDTO data)
        {
            return new TerInfoBancariaDTOAuditoria()
            {

                bancoTexto = data.BancoTexto,
                ciudadTexto = data.ciudadTexto,
                correoPagos = data.correoPagos,
                numero = data.numero,
                tipoCuentaTexto = data.tipoCuentaTexto
            };
        }


        public static TerInfoBancariaDTOAuditoria MapToAuditoria(this TerCuentaBancaria data, Bancos banco, TipoCuentaBancaria tipocuenta, Ciudades ciudad)
        {

            return new TerInfoBancariaDTOAuditoria()
            {

                bancoTexto = banco.Texto,
                ciudadTexto = ciudad.CiuNombre,
                correoPagos = data.CorreoNotificaPago,
                numero = data.Numero,
                tipoCuentaTexto = tipocuenta.Codigo
            };
        }
    }
}
