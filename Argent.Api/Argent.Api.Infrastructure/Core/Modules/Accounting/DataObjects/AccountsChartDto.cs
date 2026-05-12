namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    
    public class AccountsChartDto {
        public long Id { get; set; }
        public string ChartName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ChartType { get; set; } = string.Empty;
        public int HeaderCount { get; set; }
        public int AccountCount { get; set; }
    }

}
