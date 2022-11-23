using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;

namespace Code.Repository.Model.Mapper
{
    public static class MapTerDatosContacto
    {
        /// <summary>
        /// Metodo encargado de mapear de  [TercerosDatosContacto {DTO}] --> [TercerosDatosContacto {Entity}]
        /// </summary>
        /// <param name="dataDTO"></param>
        /// <returns></returns>
        public static TerDatosContacto MapToEntity(this TerDatosContactosDTO dataDTO)
        {
            return new TerDatosContacto()
            {
                TdcCargo = dataDTO.Cargo,
                TdcCelular = dataDTO.Celular,
                TdcTipoContactoId = dataDTO.TipoContactoId,
                TdcCiudad = dataDTO.Ciudad != null ? dataDTO.Ciudad.id : 0,
                TdcTerceroId = dataDTO.TerceroId,
                TdcCorreo = dataDTO.Correo,
                TdcDireccion = dataDTO.Direccion,
                TdcId = dataDTO.Id,
                TdcNombre = dataDTO.Nombre,
                TdcNumDocumento = dataDTO.NumeroDocumento,
                TdcTelefono = dataDTO.Telefono
            };
        }
        /// <summary>
        /// Metodo encargado de mapear de [TercerosDatosContactos {Entity}] --> [TercerosDatosContactos {DTO}]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerDatosContactosDTO MapToDTO(this TerDatosContacto data, Ciudades _ciudad)
        {
            return new TerDatosContactosDTO()
            {
                Cargo = data.TdcCargo,
                Celular = data.TdcCelular,
                Ciudad = new DTO.Ciudades.CiudadesDTO()
                {
                    id = data.TdcCiudad,
                    codigo = _ciudad != null ? _ciudad.CiuCodigo : string.Empty,
                    nombre = _ciudad != null ? _ciudad.CiuNombre : string.Empty
                },
                Correo = data.TdcCorreo,
                Direccion = data.TdcDireccion,
                Nombre = data.TdcNombre,
                NumeroDocumento = data.TdcNumDocumento,
                Telefono = data.TdcTelefono,
                TerceroId = data.TdcTerceroId,
                TipoContactoId = data.TdcTipoContactoId,
                Id = data.TdcId

            };
        }
        public static TerDatosContactos_Audit_DTO MapToDTO_Audit(this TerDatosContacto dataDTO, Ciudades ciudadNo)
        {

            if (ciudadNo == null)
                ciudadNo = new Ciudades()
                {
                    CiuCodigo = string.Empty,
                    CiudID = 0,
                    CiuNombre = string.Empty,

                };

            return new TerDatosContactos_Audit_DTO()
            {
                TdcCargo = dataDTO.TdcCargo,
                TdcCelular = dataDTO.TdcCelular,
                TdcCiudad = ciudadNo.CiuNombre,
                TdcCorreo = dataDTO.TdcCorreo,
                TdcDireccion = dataDTO.TdcDireccion,
                TdcId = dataDTO.TdcId,
                TdcNombre = dataDTO.TdcNombre,
                TdcNumDocumento = dataDTO.TdcNumDocumento,
                TdcTelefono = dataDTO.TdcTelefono,
                TdcTerceroId = dataDTO.TdcTerceroId,
                TdcTipoContactoId = dataDTO.TdcTipoContactoId

            };

        }

        public static TerDatosContactos_Audit_DTO MapToDTO(this TerDatosContactosDTO dataDTO, Ciudades ciudadNo)
        {
            return new TerDatosContactos_Audit_DTO()
            {
                TdcCargo = dataDTO.Cargo,
                TdcCelular = dataDTO.Celular,
                TdcCiudad = ciudadNo.CiuNombre,
                TdcCorreo = dataDTO.Correo,
                TdcDireccion = dataDTO.Direccion,
                TdcId = dataDTO.Id,
                TdcNombre = dataDTO.Nombre,
                TdcNumDocumento = dataDTO.NumeroDocumento,
                TdcTelefono = dataDTO.Telefono,
                TdcTerceroId = dataDTO.TerceroId,
                TdcTipoContactoId = dataDTO.TipoContactoId

            };

        }


   
      

    }
}
