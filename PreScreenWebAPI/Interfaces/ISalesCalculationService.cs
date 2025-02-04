using PreScreenWebAPI.Models;

namespace PreScreenWebAPI.Interfaces
{
    public interface ISalesCalculationService
    {
        SalesOutput CalculateOutput(List<SalesRecord> saleRecords);
        decimal CalculateMedianOfUnitCost(List<SalesRecord> saleRecords);


    }
}
