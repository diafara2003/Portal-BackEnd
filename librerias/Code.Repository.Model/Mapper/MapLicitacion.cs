using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.Entity;
using System;
using System.Globalization;

namespace Code.Repository.Model.Mapper
{
    public static class MapLicitacion
    {
        public static CSLicitacion MapToLicitacion(this LicitacionDTO register)
        {
            return new CSLicitacion()
            {
                LicConstructora = register.IdConstructora,
                LicLicitacion = register.IdLicitacion,
                LicNumero = register.Numero,
                LicAsunto = register.Asunto,
                LicFecha = Convert.ToDateTime(register.Fecha),
                LicFechaCierre = Convert.ToDateTime(register.FechaCierre),
                LicCategoria = register.Categoria,
                LicCantActividades = register.CantActividades,
                LicVrEstimado = register.Valor,
                LicEstado = register.Estado,
                LicCiudad = register.Ciudad,
                LicProyecto = register.Proyecto

            };
        }

        public static LicitacionDTO MapToLicitacionDTO(this CSLicitacion register)
        {
            return new LicitacionDTO()
            {
                IdConstructora = register.LicConstructora,
                IdLicitacion = register.LicLicitacion,
                Numero = register.LicNumero,
                Asunto = register.LicAsunto,
                FechaCierre = register.LicFechaCierre.ToString(),
                Categoria = register.LicCategoria,
                CantActividades = register.LicCantActividades,
                Valor = register.LicVrEstimado,
                Estado = register.LicEstado,
                Proyecto = register.LicProyecto,
                Ciudad = register.LicCiudad

            };
        }

        public static CSLicitacionDTO MapToDTO(this CSLicitacion register, Constructora constructora, Categoria categoria)
        {
            return new CSLicitacionDTO()
            {
                IdConstructora = register.LicConstructora,
                IdLicitacion = register.LicLicitacion,
                Numero = register.LicNumero,
                Asunto = register.LicAsunto,
                FechaCierre = register.LicFechaCierre.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Categoria = register.LicCategoria,
                NombreCategoria = categoria.CatDesc,
                Estado = register.LicEstado,
                Proyecto = register.LicProyecto,
                Ciudad = register.LicCiudad,
                NombreEmpresa = constructora.ConstNombre
            };
        }
    }
}
