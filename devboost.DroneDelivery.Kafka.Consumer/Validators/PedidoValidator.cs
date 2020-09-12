using FluentValidation;
using devboost.DroneDelivery.Kafka.Consumer.Model;

namespace devboost.DroneDelivery.Kafka.Consumer.Validators
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(c => c.Peso).NotEmpty().WithMessage("Preencha o campo 'Peso' em gramas");     
        }
    }
}