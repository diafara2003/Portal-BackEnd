using Code.Repository.Model.DTO.Novedades;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper
{
    public static class MapNovedades
    {
        /// <summary>
        /// Metodo encargado de mapear de [TercerosDatosContactos {Entity}] --> [TercerosDatosContactos {DTO}]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static NovedadesDTO MapToDTO(this Novedades data)
        {
            return new NovedadesDTO()
            {
                tercero = data.NovIdTercero,
                comentario = data.NovObservaciones,
                fecha = data.NovFecha
            };
        }

        public static List<NovedadesDTO> MapToDTO(this List<Novedades> data, string constructora)
        {
            List<NovedadesDTO> detalles = new List<NovedadesDTO>();

            data.ForEach(c => detalles.Add(c.MapToDTO(constructora)));

            return detalles;
        }


        public static NovedadesDTO MapToDTO(this Novedades data, string constructora)
        {
            return new NovedadesDTO()
            {
                asunto = data.NovAsunto,
                comentario = data.NovObservaciones,
                tercero = data.NovIdTercero,
                numero = data.NovId,
                fecha = data.NovFecha,
                constructora = constructora,
                IsActiva = data.NovEstado == 0 ? false : true,
                IdConstructora = data.NovIdConstructora,
                Tipo = data.NovTipo,
                
            };
        }



        public static NovedadesDetDTO MapToDTO(this NovedadesDet data, TipoAdjuntoTercero doc)
        {
            return new NovedadesDetDTO()
            {
                tipoDocumento = data.NovDetIdTipoAdjujunto,
                nombre = doc.TipAdjTexto,
                tipo = data.NovDetTipo,
            };
        }

    }
}
