using PreScreenWebAPI.Interfaces;
using PreScreenWebAPI.Models;

namespace PreScreenWebAPI.Services
{
    public class SalesCalculationService: ISalesCalculationService
    {
        //Calculates the required output of the sales Data
        public virtual SalesOutput CalculateOutput(List<SalesRecord> saleRecords)
        {
            SalesOutput output = new SalesOutput();
            output.MedianUnitCost = CalculateMedianOfUnitCost(saleRecords);
            output.TotalRevenue = saleRecords.Select(s => s.TotalRevenue).Sum();

            output.salesDateOutput = new SalesDateOutput
            {
                FirstOrderDate = saleRecords.OrderBy(s => s.OrderDate).Select(s => s.OrderDate).FirstOrDefault(),
                LastOrderDate = saleRecords.OrderByDescending(s => s.OrderDate).Select(s => s.OrderDate).FirstOrDefault()
            };

            output.salesDateOutput.DaysBetween = (output.salesDateOutput.LastOrderDate - output.salesDateOutput.FirstOrderDate).Days;

            output.MostCommonRegion = saleRecords
     .GroupBy(s => s.Region)
     .OrderByDescending(s => s.Count())
     .FirstOrDefault()?.Key ?? "";
            return output;
        }

        //calculates the median price of unit cost
        public virtual decimal CalculateMedianOfUnitCost(List<SalesRecord> saleRecords)
        {
            if (saleRecords.Count > 0)
            {


                var RecordsSortedByUnitCost = saleRecords.OrderBy(x => x.UnitCost).ToList();
                int count = saleRecords.Count;
                if (count % 2 == 0)
                {
                    var middle1 = RecordsSortedByUnitCost[count / 2 - 1].UnitCost;
                    var middle2 = RecordsSortedByUnitCost[count / 2].UnitCost;
                    return (middle1 + middle2) / 2;
                }
                else
                {
                    return RecordsSortedByUnitCost[count / 2].UnitCost;
                }
            }
            else return 0;
        }

        
    }

}
