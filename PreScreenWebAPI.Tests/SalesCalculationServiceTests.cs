using PreScreenWebAPI.Models;
using PreScreenWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreScreenWebAPI.Tests
{
    public class SalesCalculationServiceTests
    {
        private readonly SalesCalculationService _salesCalculationService;

        public SalesCalculationServiceTests()
        {
            _salesCalculationService = new SalesCalculationService();
        }

        [Fact]
        public void CalculateOutput_ReturnsCorrectSalesOutput()
        {
            // Prepare sample sales data
            var saleRecords = new List<SalesRecord>
        {
            new SalesRecord { Region = "Australia", UnitCost = 10, TotalRevenue = 400, OrderDate = new DateTime(2024, 1, 1) },
            new SalesRecord { Region = "Europe", UnitCost = 20, TotalRevenue = 200, OrderDate = new DateTime(2024, 2, 1) },
            new SalesRecord { Region = "North America", UnitCost = 30, TotalRevenue = 300, OrderDate = new DateTime(2024, 3, 1) },
            new SalesRecord { Region = "Europe", UnitCost = 15, TotalRevenue = 150, OrderDate = new DateTime(2024, 4, 1) }
        };

            // Create expected output based on the sample sales data
            var expectedOutput = new SalesOutput
            {
                MedianUnitCost = 17.5m, // (15 + 20) / 2
                TotalRevenue = 1050m,   // 400 + 200 + 300 + 150
                salesDateOutput = new SalesDateOutput
                {
                    FirstOrderDate = new DateTime(2024, 1, 1),
                    LastOrderDate = new DateTime(2024, 4, 1),
                    DaysBetween = 91 // Difference between Jan 1 and Apr 1
                },
                MostCommonRegion = "Europe"
            };

            // Call the CalculateOutput method directly
            var result = _salesCalculationService.CalculateOutput(saleRecords);

            // Verify that the result matches the expected output
            Assert.Equal(expectedOutput.MedianUnitCost, result.MedianUnitCost);
            Assert.Equal(expectedOutput.TotalRevenue, result.TotalRevenue);
            Assert.Equal(expectedOutput.salesDateOutput.FirstOrderDate, result.salesDateOutput.FirstOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.LastOrderDate, result.salesDateOutput.LastOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.DaysBetween, result.salesDateOutput.DaysBetween);
            Assert.Equal(expectedOutput.MostCommonRegion, result.MostCommonRegion);
        }

        [Fact]
        public void CalculateOutput_ReturnsEmptySalesOutput_WhenNoSalesRecords()
        {
            // Prepare an empty list of sales records
            var saleRecords = new List<SalesRecord>();

            // Create expected empty output
            var expectedOutput = new SalesOutput
            {
                MedianUnitCost = 0m,  // No records, median should be 0
                TotalRevenue = 0m,    // No records, total revenue should be 0
                salesDateOutput = new SalesDateOutput
                {
                    FirstOrderDate = default(DateTime),
                    LastOrderDate = default(DateTime),
                    DaysBetween = 0
                },
                MostCommonRegion = ""  // No records, so no region
            };

            // Call the CalculateOutput method directly
            var result = _salesCalculationService.CalculateOutput(saleRecords);

            // Verify that the result matches the expected empty output
            Assert.Equal(expectedOutput.MedianUnitCost, result.MedianUnitCost);
            Assert.Equal(expectedOutput.TotalRevenue, result.TotalRevenue);
            Assert.Equal(expectedOutput.salesDateOutput.FirstOrderDate, result.salesDateOutput.FirstOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.LastOrderDate, result.salesDateOutput.LastOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.DaysBetween, result.salesDateOutput.DaysBetween);
            Assert.Equal(expectedOutput.MostCommonRegion, result.MostCommonRegion);
        }

        [Fact]
        public void CalculateOutput_ReturnsCorrectOutput_WhenOnlyOneRecord()
        {
            // Prepare a single sales record
            var saleRecords = new List<SalesRecord>
    {
        new SalesRecord { Region = "Australia", UnitCost = 10, TotalRevenue = 100, OrderDate = new DateTime(2024, 1, 1) }
    };

            // Create expected output based on this single record
            var expectedOutput = new SalesOutput
            {
                MedianUnitCost = 10m, // Only one record, so median is the unit cost
                TotalRevenue = 100m,  // Total revenue is the same as the single record
                salesDateOutput = new SalesDateOutput
                {
                    FirstOrderDate = new DateTime(2024, 1, 1),
                    LastOrderDate = new DateTime(2024, 1, 1), // Same date for first and last
                    DaysBetween = 0 // No difference since it's the same date
                },
                MostCommonRegion = "Australia" // The region with the single record is the most common
            };

            // Call the CalculateOutput method directly
            var result = _salesCalculationService.CalculateOutput(saleRecords);

            // Verify that the result matches the expected output
            Assert.Equal(expectedOutput.MedianUnitCost, result.MedianUnitCost);
            Assert.Equal(expectedOutput.TotalRevenue, result.TotalRevenue);
            Assert.Equal(expectedOutput.salesDateOutput.FirstOrderDate, result.salesDateOutput.FirstOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.LastOrderDate, result.salesDateOutput.LastOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.DaysBetween, result.salesDateOutput.DaysBetween);
            Assert.Equal(expectedOutput.MostCommonRegion, result.MostCommonRegion);
        }

    }
}
