using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Queries
{
    public static class VeiculoQuery
    {
        public static IQueryable<Veiculo> ComId(this IQueryable<Veiculo> veiculos, int idVeiculo)
        {
            return veiculos.Where(v => v.IdVeiculo == idVeiculo);
        }

        public static IQueryable<Veiculo> ComPlaca(this IQueryable<Veiculo> veiculos, string placa)
        {
            return veiculos.Where(v => v.Placa == placa);
        }

        public static IQueryable<Veiculo> OndePlacaContem(this IQueryable<Veiculo> veiculos, string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return veiculos;
            }
            else
            {
                return veiculos.Where(v => v.Placa.Contains(placa));
            }
        }
    }
}