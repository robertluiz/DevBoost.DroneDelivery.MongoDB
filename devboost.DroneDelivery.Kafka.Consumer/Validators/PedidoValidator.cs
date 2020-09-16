using FluentValidation;
using devboost.DroneDelivery.Kafka.Consumer.Model;

namespace devboost.DroneDelivery.Kafka.Consumer.Validators
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(c => c.Peso).NotEmpty().WithMessage("Preencha o campo 'Peso' em gramas");     
            RuleFor(c => c.Valor).NotEmpty().WithMessage("Preencha o campo 'Valor'");     
            RuleFor(c => c.BandeiraCartao).NotEmpty().WithMessage("Preencha o campo 'Bandeira do Cartão' ");     
            RuleFor(c => c.CodSegurancaCartao).NotEmpty().WithMessage("Preencha o campo 'Cod. Segurança do Cartão' ");     
            RuleFor(c => c.DataValidadeCartao).NotEmpty().WithMessage("Preencha o campo 'Data Validade Cartão' ");     
            RuleFor(c => c.NomeCartao).NotEmpty().WithMessage("Preencha o campo 'Nome' ");     
            RuleFor(c => c.NumeroCartao).NotEmpty().WithMessage("Preencha o campo 'Numero do Cartão' ");     
            RuleFor(c => c.TipoCartao).NotEmpty().WithMessage("Preencha o campo 'Tipo Cartão' ");     
        }
    }
}