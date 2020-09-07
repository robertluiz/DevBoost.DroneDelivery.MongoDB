using Devboost.Pagamentos.Domain.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Devboost.Pagamentos.Domain.Params
{
    public class CartaoParam : PagamentoParam
    {
        //[EnumDataType(typeof(PagamentoBandeiraEnum))]
        //[JsonConverter(typeof(StringEnumConverter))]
        public PagamentoBandeiraEnum Bandeira { get; set; }
        public string Nome { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodSeguranca { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
    }
}