using System.ComponentModel;

namespace Devboost.DroneDelivery.Domain.Enums
{
    public enum PagamentoBandeiraEnum
    {
        [Description("MasterCard")]
        MasterCard,
        [Description("Visa")]
        Visa,
        [Description("Alelo")]
        Alelo,
        [Description("AmericanExpress")]
        AmericanExpress
    }
}