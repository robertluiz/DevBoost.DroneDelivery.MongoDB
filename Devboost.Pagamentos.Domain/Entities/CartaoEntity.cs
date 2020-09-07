using Devboost.Pagamentos.Domain.Enums;
using System;
using Devboost.Pagamentos.Domain.Interfaces.Entity;
using System.Collections.Generic;
using Devboost.Pagamentos.Domain.Utilitys;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class CartaoEntity : IEntity
    {
        public Guid Id { get; set; }
        public PagamentoBandeiraEnum Bandeira { get; set; }
        public string Nome { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodSeguranca { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
        public List<string> Validar()
        {
            var result = new List<string>();

            ValidaBandeira(result);
            ValidaNome(result);
            ValidaNumeroCartao(result);
            ValidaDataValidade(result);
            ValidaCodSeguranca(result);
            ValidaTipoCartao(result);            

            return result;
        }

        #region ValidaValores

        public void ValidaBandeira(List<string> listErros)
        {
            if (!Enum.IsDefined(typeof(PagamentoBandeiraEnum), Bandeira))
                listErros.Add(string.Format("Bandeira não corresponde com o valores: {0} esperados!", HelperEnum.GetStringByEnumType<PagamentoBandeiraEnum>()));
        }

        public void ValidaNome(List<string> listErros)
        {
            if (string.IsNullOrWhiteSpace(Nome))
                listErros.Add("Nome não pode ser vazio!");
        }

        public void ValidaNumeroCartao(List<string> listErros)
        {
            if (string.IsNullOrEmpty(NumeroCartao) || NumeroCartao.Length != 16)
                listErros.Add("O número do cartão deve conter 16 dígitos!");
        }

        public void ValidaDataValidade(List<string> listErros)
        {
            if (DataValidade < DateTime.Now)
                listErros.Add("A Data de validade do cartão está expirada!");
        }

        public void ValidaCodSeguranca(List<string> listErros)
        {
            if (string.IsNullOrEmpty(NumeroCartao) || NumeroCartao.Length != 3)
                listErros.Add("Código de segurança deve conter 3 dígitos!");
        }

        public void ValidaTipoCartao(List<string> listErros)
        {
            if (!Enum.IsDefined(typeof(TipoCartaoEnum), Tipo))
                listErros.Add(string.Format("Tipo de cartão não corresponde com o valores: {0} esperados!", HelperEnum.GetStringByEnumType<TipoCartaoEnum>()));
        }

        #endregion        
    }
}