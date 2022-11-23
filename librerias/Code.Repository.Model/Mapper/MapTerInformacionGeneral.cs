using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;

namespace Code.Repository.Model.Mapper
{
    public static class MapTerInformacionGeneral
    {


        public static ListadoEstadoTerccerosDTO MapToDTO(this TerInformacionGeneral data, TerEstado estado)
        {

            return new ListadoEstadoTerccerosDTO()
            {
                documento = data.TigNumeroIdentificacion,
                id = data.TigTerceroId,
                nombre = data.TigNombre.Trim(),
                correo = data.TigCorreo,
                telefono = data.TigTelefono,
                tipoPersona = data.TigTipoPersona.ToLower() == "n" ? "Natural" : "Juridica",
                estado = estado.TerNombre
                //estado = new EstadoTerceroDTO()
                //{
                //    id = estado.Id,
                //    nombre = estado.TerNombre
                //}
            };
        }


        /// <summary>
        /// Metodo encargado de mapear de [TercerosInformacionGeneral {Entity}] --> [TercerosInformacionGeneral {DTO}]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerInformacionGeneralDTO MapToDTO(this TerInformacionGeneral data, string nombreCiudad = "")
        {

            return new TerInformacionGeneralDTO()
            {
                id = data.TigTerceroId,

                actividadEconomica = new ActividadEconomicaDTO()
                {
                    id = data.TigActEconomicaPri
                },
                CertificadoISO = data.TigCertificadoISO,
                Ciudad = new DTO.Ciudades.CiudadesDTO()
                {
                    id = data.TigCiudad,
                    nombre = nombreCiudad

                },
                Correo = data.TigCorreo,
                Nombre = data.TigNombre,
                Apellido = data.TerApellido,
                NumeroIdentificacion = data.TigNumeroIdentificacion,
                PaginaWeb = data.TigPaginaWeb,
                Telefono = data.TigTelefono,
                TipoDocumento = data.TigTipoDocumento,
                TipoPersona = (data.TigTipoPersona == "N" ? "Natural" : "Juridica"),
                Direccion = data.TigDireccion,
                DigitoVerificacion = data.TerDigitoVerificacion


            };
        }
        /// <summary>
        /// Metodo encargado de mapear un entity para realizar un UPDATE
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="dataDTO"></param>
        /// <returns></returns>
        public static TerInformacionGeneral MapToEntity(this TerInformacionGeneralDTO dataDTO)
        {
            string tipoPersona = dataDTO.TipoPersona;

            if (string.IsNullOrEmpty(dataDTO.TipoPersona))
                tipoPersona = dataDTO.TipoDocumento.Contains("NIT") ? "J" : "N";
            else tipoPersona = dataDTO.TipoPersona.Substring(0, 1).ToUpper();

            return new TerInformacionGeneral()
            {
                TigActEconomicaPri = dataDTO.actividadEconomica != null ? dataDTO.actividadEconomica.id : 0,
                TigCiudad = dataDTO.Ciudad != null ? dataDTO.Ciudad.id : 0,
                TigTipoPersona = tipoPersona,
                TigTipoDocumento = dataDTO.TipoDocumento,
                TigCertificadoISO = dataDTO.CertificadoISO,
                TigCorreo = dataDTO.Correo,
                TigNombre = dataDTO.Nombre,
                TerApellido = dataDTO.Apellido,
                TigNumeroIdentificacion = dataDTO.NumeroIdentificacion,
                TigPaginaWeb = dataDTO.PaginaWeb,
                TigTelefono = dataDTO.Telefono,
                TigDireccion = dataDTO.Direccion,
                TerDigitoVerificacion = dataDTO.DigitoVerificacion

            };

        }

        public static TerInformacionGeneral_Audit_DTO MapToDTO(this TerInformacionGeneral dataDTO, Ciudades ciudadNo, ActividadEconomica _ActEco)
        {

            if (ciudadNo == null)
                ciudadNo = new Ciudades()
                {
                    CiuCodigo = string.Empty,
                    CiudID = 0,
                    CiuNombre = string.Empty,

                };

            if (_ActEco == null)
                _ActEco = new ActividadEconomica()
                {
                    ActEcId = 0,
                    ActEcCodigo = string.Empty,
                    ActECtexto = string.Empty,
                };

            return new TerInformacionGeneral_Audit_DTO()
            {
                TigActEconomicaPri = _ActEco.ActECtexto,
                TigCiudad = ciudadNo.CiuNombre,
                TigPaginaWeb = dataDTO.TigPaginaWeb,
                TerApellido = dataDTO.TerApellido,
                TigCertificadoISO = dataDTO.TigCertificadoISO == true ? "Si" : "No",
                TigCorreo = dataDTO.TigCorreo,
                TerDigitoVerificacion = dataDTO.TerDigitoVerificacion,
                TigDireccion = dataDTO.TigDireccion,
                TigNombre = dataDTO.TigNombre,
                TigTelefono = dataDTO.TigTelefono,
                TigNumeroIdentificacion = dataDTO.TigNumeroIdentificacion,
                TigTipoDocumento = dataDTO.TigTipoDocumento,
                TigTipoPersona = dataDTO.TigTipoPersona,
                TigId = dataDTO.TigId

            };

        }

        public static TerInformacionGeneral_Audit_DTO MapToDTO(this TerInformacionGeneralDTO dataDTO, Ciudades ciudadNo, ActividadEconomica _ActEco)
        {
            return new TerInformacionGeneral_Audit_DTO()
            {
                TigActEconomicaPri = _ActEco.ActECtexto,
                TigCiudad = ciudadNo.CiuNombre,
                TigPaginaWeb = dataDTO.PaginaWeb,
                TerApellido = dataDTO.Apellido,
                TigCertificadoISO = dataDTO.CertificadoISO == true ? "Si" : "No",
                TigCorreo = dataDTO.Correo,
                TerDigitoVerificacion = dataDTO.DigitoVerificacion,
                TigDireccion = dataDTO.Direccion,
                TigNombre = dataDTO.Nombre,
                TigTelefono = dataDTO.Telefono,
                TigNumeroIdentificacion = dataDTO.NumeroIdentificacion,
                TigTipoDocumento = dataDTO.TipoDocumento,
                TigTipoPersona = dataDTO.TipoPersona,
                TigId = dataDTO.id

            };

        }
    }
}
