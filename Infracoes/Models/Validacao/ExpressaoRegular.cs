using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.Validacao
{
    public static class ExpressaoRegular
    {
        public const string CPF = @"([0-9]{3}\.){2}[0-9]{3}-[0-9]{2}";
        public const string CNPJ = @"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}";
        public const string CEP = @"^\d{5}-\d{3}$";
        public const string PLACA = @"^[A-z]{3}\-[0-9]{4}$";
        public const string EMAIL = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public const string REQUISITOS_MINIMOS = @"^(?=(.*\d?){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]?).{8,}$";
        public const string URL_EMPRESA = @"^[a-zA-Z0-9-]+$";
        public const string HORA = @"^[0-2][0-3]:[0-5][0-9]$";
        public const string TELEFONE = @"^\d{5}-\d{4}$";
    }
}