using CsvHelper.Configuration.Attributes;

namespace PreScreenWebAPI
{
    public class SalesRecord
    {
        public string Region { get; set; }
        [Name("Order Date")]
        public DateTime OrderDate { get; set; }
        [Name("Unit Cost")]
        public decimal UnitCost { get; set; }
        [Name("Total Revenue")]
        public decimal TotalRevenue { get; set; }
    }

   

   
}
