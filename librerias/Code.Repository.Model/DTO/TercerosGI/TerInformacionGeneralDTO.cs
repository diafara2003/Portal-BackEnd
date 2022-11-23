using Code.Repository.Model.DTO.Adjuntos;
using Code.Repository.Model.DTO.Ciudades;
using Code.Repository.Model.DTO.Especialidades;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;


namespace Code.Repository.Model.DTO.TercerosIG
{

    public class TerceroDTO {

        public int id { get; set; }
        public string correo { get; set; }
        public string documento { get; set; }
        public EstadoTercero estado { get; set; }
        public bool correoConfirmado { get; set; }
    }

    public class TerceroInfoDetalladaDTO {

        public TerInformacionGeneralDTO informacionGeneral { get; set; }
        public IEnumerable<TerDatosContactosDTO> datosContacto { get; set; }
        public IEnumerable<TerCamaraComercioDTO> inscritosCamaraComercio { get; set; }
        public IEnumerable<EspecialidadDTO> especialidades { get; set; }
        public List<AdjuntosConstructoraDTO> adjuntos { get; set; }
    }


    public class AdjuntosConstructoraDTO {

        public int IdAdjuntoTercero { get; set; }
        public int IdAdjunto { get; set; }
        public int TipoAdjunto { get; set; }
    }

    public class TerceroInformacionPortalDTO {

        public TerInformacionGeneralDTO informacionGeneral { get; set; }
        public IEnumerable<TerDatosContactosDTO> datosContacto { get; set; }
        public IEnumerable<TerCamaraComercioDTO> inscritosCamaraComercio { get; set; }
        public IEnumerable<EspecialidadDTO> especialidades { get; set; }
        public IEnumerable<AdjuntoTerceroDTO> adjuntos { get; set; }

    }
    public class TerInformacionGeneralDTO
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroIdentificacion { get; set; }
        public int? DigitoVerificacion  { get; set; }
        public string Correo { get; set; }
        public CiudadesDTO Ciudad { get; set; }
        public ActividadEconomicaDTO actividadEconomica { get; set; }
        public string Telefono { get; set; }
        public string PaginaWeb { get; set; }
        public bool CertificadoISO { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class ActividadEconomicaDTO {

        public int id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }

    }



    public class ListadoEstadoTerccerosDTO {

        public int id { get; set; }
        public string nombre { get; set; }        
        public string documento { get; set; }
        public string tipoPersona { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string estado { get; set; }        
    }

    public class EstadoTerceroDTO {
        public int id { get; set; }
        public string nombre { get; set; }
    }

    public class TerInformacionGeneral_Audit_DTO
    {         
        public int TigId { get; set; }
        public string TigNombre { get; set; }
        public string TerApellido { get; set; }
        public string TigTipoPersona { get; set; }
        public string TigTipoDocumento { get; set; }
        public string TigNumeroIdentificacion { get; set; }
        public int? TerDigitoVerificacion { get; set; }
        public string TigCorreo { get; set; }
        public string TigCiudad { get; set; }
        public string TigActEconomicaPri { get; set; }
        public string TigTelefono { get; set; }
        public string TigPaginaWeb { get; set; }
        public string TigCertificadoISO { get; set; }
        public string TigDireccion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
