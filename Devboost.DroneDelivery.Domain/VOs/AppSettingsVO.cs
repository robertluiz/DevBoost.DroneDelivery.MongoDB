using System.Diagnostics.CodeAnalysis;

namespace Devboost.DroneDelivery.Domain.VOs
{
    [ExcludeFromCodeCoverage]
    public class AppSettingsVO
    {
        public string TokenLimitMinutesExpire { get; set; }
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Emitter { get; set; }
        public string ValidOn { get; set; }
    }
}