using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Queries
{
    public static class AgenteQuery
    {
        public static IQueryable<Agente> ComId(this IQueryable<Agente> agentes, int idAgente)
        {
            return agentes.Where(a => a.IdAgente == idAgente);
        }

        public static IQueryable<Agente> ComNome(this IQueryable<Agente> agentes, string nomeAgente)
        {
            return agentes.Where(a => a.NomeAgente == nomeAgente);
        }

        public static IQueryable<Agente> OndeNomeContem(this IQueryable<Agente> agentes, string nomeAgente)
        {
            if (string.IsNullOrEmpty(nomeAgente))
            {
                return agentes;
            }

            return agentes.Where(a => a.NomeAgente.Contains(nomeAgente));
        }

        public static IQueryable<Agente> ComMatricula(this IQueryable<Agente> agentes, string matricula)
        {
            return agentes.Where(a => a.Matricula == matricula);
        }

        public static IQueryable<Agente> OndeMatriculaContem(this IQueryable<Agente> agentes, string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
            {
                return agentes;
            }

            return agentes.Where(a => a.Matricula.Contains(matricula));
        }

    }
}