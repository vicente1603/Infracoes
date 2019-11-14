using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Queries
{
    public static class ModeloQuery
    {
        public static IQueryable<Modelo> ComId(this IQueryable<Modelo> modelos, int idModelo)
        {
            return modelos.Where(m => m.IdModelo == idModelo);
        }

        public static IQueryable<Modelo> ComDescricao(this IQueryable<Modelo> modelos, string descricao)
        {
            return modelos.Where(m => m.Descricao == descricao);
        }

        public static IQueryable<Modelo> OndeDescricaoContem(this IQueryable<Modelo> modelos, string descricao)
        {
            if (string.IsNullOrEmpty(descricao)){
                return modelos;
            }
            return modelos.Where(m => m.Descricao.Contains(descricao));
        }

        public static IQueryable<Modelo> OrdenadosPorDescricao(this IQueryable<Modelo> modelos)
        {
            return modelos.OrderBy(m => m.Descricao);
        }
    }
}