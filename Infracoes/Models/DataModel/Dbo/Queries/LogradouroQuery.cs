using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Queries
{
    public static class LogradouroQuery
    {
        public static IQueryable<Logradouro> ComId(this IQueryable<Logradouro> logradouros, int idLogradouro)
        {
            return logradouros.Where(l => l.IdLogradouro == idLogradouro);
        }

        public static IQueryable<Logradouro> ComCep(this IQueryable<Logradouro> logradouros, string cep)
        {
            return logradouros.Where(l => l.Cep == cep);
        }

        public static IQueryable<Logradouro> OndeCepContem(this IQueryable<Logradouro> logradouros, string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                return logradouros;
            }

            return logradouros.Where(l => l.Cep.Contains(cep));
        }
    }
}