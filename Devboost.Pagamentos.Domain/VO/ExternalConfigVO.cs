using System;
using System.Collections.Generic;
using System.Text;

namespace Devboost.Pagamentos.Domain.VO
{
    public class ExternalConfigVO
    {
        public string GatewayUrl { get; set; }
        public string DeliveryUrl { get; set; }
    }
}
