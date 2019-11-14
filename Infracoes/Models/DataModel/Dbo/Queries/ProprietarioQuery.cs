using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Queries
{
    public static class ProprietarioQuery
    {
        public static IQueryable<Proprietario> ComId(this IQueryable<Proprietario> proprietarios, int idProprietario)
        {
            return proprietarios.Where(p => p.IdProprietario == idProprietario);
        }

        public static IQueryable<Proprietario> ComNomeProprietario(this IQueryable<Proprietario> proprietarios, string nomeProprietario)
        {
            return proprietarios.Where(p => p.NomeProprietario == nomeProprietario);
        }

        public static IQueryable<Proprietario> OndeNomePContem(this IQueryable<Proprietario> proprietarios, string nomeProprietario)
        {
            if (string.IsNullOrEmpty(nomeProprietario))
            {
                return proprietarios;
            }

            return proprietarios.Where(p => p.NomeProprietario.Contains(nomeProprietario));
        }

        public static IQueryable<Proprietario> ComCpf(this IQueryable<Proprietario> proprietarios, string cpfProprietario)
        {
            return proprietarios.Where(p => p.CpfProprietario == cpfProprietario);
        }

        public static IQueryable<Proprietario> ContemCpf(this IQueryable<Proprietario> proprietarios, string cpfProprietario)
        {
            if (string.IsNullOrEmpty(cpfProprietario))
            {
                return proprietarios;
            }

            return proprietarios.Where(p => p.CpfProprietario.Contains(cpfProprietario));
        }

        public static IQueryable<Proprietario> OrdenadosPorNomeP(this IQueryable<Proprietario> proprietarios)
        {
            return proprietarios.OrderBy(p => p.NomeProprietario);
        }

        public static IQueryable<Proprietario> ComDataNascimento(this IQueryable<Proprietario> proprietarios, DateTime dataNascimento)
        {
            if (!dataNascimento.Equals(""))
            {
                return proprietarios.Where(p => p.DataNascimento == dataNascimento);
            }

            return proprietarios;
        }
    }
}