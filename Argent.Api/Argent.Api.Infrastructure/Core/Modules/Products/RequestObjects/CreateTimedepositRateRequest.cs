using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateTimedepositRateRequest {
        public int Period { get; set; }
        public IntervalType PeriodType { get; set; } = IntervalType.Months;
        public decimal InterestRate { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}
