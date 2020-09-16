using Devboost.Pagamentos.Kafka.Consumer.Model;
using FluentValidation;

namespace Devboost.Pagamentos.Kafka.Consumer.Validators
{
    public class PagamentoStatusValidator: AbstractValidator<PagamentoStatus>
    {
        public PagamentoStatusValidator()
        {
            RuleFor(c => c.IdPedido).NotEmpty().WithMessage("Preencha o campo 'IdPedido' ");
            RuleFor(c => c.StatusPagamento).NotEmpty().WithMessage("Preencha o campo 'StatusPagamento' ");
        }
        
    }
}