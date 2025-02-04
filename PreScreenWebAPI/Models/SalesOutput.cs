namespace PreScreenWebAPI.Models
{
    public class SalesOutput
    {
        public decimal MedianUnitCost { get; set; }
        public string MostCommonRegion { get; set; }
        public decimal TotalRevenue { get; set; }
        public SalesDateOutput salesDateOutput { get; set; }

    }
}
