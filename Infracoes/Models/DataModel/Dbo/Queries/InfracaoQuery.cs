 using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Queries
{
    public static class InfracaoQuery
    {
        public static IQueryable<Infracao> ComId(this IQueryable<Infracao> infracoes, int idInfracao)
        {
            return infracoes.Where(i => i.IdInfracao == idInfracao);
        }

        public static IQueryable<Infracao> ComDescricao(this IQueryable<Infracao> infracoes, string descricao)
        {
            return infracoes.Where(i => i.Descricao == descricao);
        }

        public static IQueryable<Infracao> OndeDescricaoContem(this IQueryable<Infracao> infracoes, string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                return infracoes;
            }
            return infracoes.Where(i => i.Descricao.Contains(descricao));
        }

        public static IQueryable<Infracao> OrdenadosPorDescricao(this IQueryable<Infracao> infracoes)
        {
            return infracoes.OrderBy(i => i.Descricao);
        }

    }
}