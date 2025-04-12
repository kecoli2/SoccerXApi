using SoccerX.Domain.Enums;

namespace SoccerX.Domain.Entities
{
    public partial class Payment
    {
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
