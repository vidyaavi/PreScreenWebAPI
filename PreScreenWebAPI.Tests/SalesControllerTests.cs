using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PreScreenWebAPI.Controllers;
using PreScreenWebAPI.Models;
using PreScreenWebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace PreScreenWebAPI.Tests
{
    public class SalesControllerTests
    {
        private readonly SalesController _controller;

        public SalesControllerTests()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "SalesRecords.csv");

            // Create service instances
            var csvHelperService = new CsvHelperService(); 
            var salesCalculationService = new SalesCalculationService();  
            var logger = new LoggerFactory().CreateLogger<SalesController>();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "FileSettings:SalesFilePath", filePath }
                })
                .Build();

            // Create controller instance with real dependencies
            _controller = new SalesController(csvHelperService, salesCalculationService, configuration, logger);
        }

        [Fact]
        public void Get_ShouldReturnCorrectSalesOutput_WhenSalesRecordsAreValid()
        {
            // calculated expected output
            var expectedOutput = new SalesOutput
            {
                MedianUnitCost = 97.44M,
                TotalRevenue = 13589195.79M,
                salesDateOutput = new SalesDateOutput
                {
                    FirstOrderDate = new DateTime(2010, 2, 4),
                    LastOrderDate = new DateTime(2015, 12, 9),
                    DaysBetween = 2134
                },
                MostCommonRegion = "Europe"
            };

            // Call the controller method
            var result = _controller.Get();

            // Verify result
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualOutput = Assert.IsType<SalesOutput>(actionResult.Value);

            Assert.Equal(expectedOutput.MedianUnitCost, actualOutput.MedianUnitCost);
            Assert.Equal(expectedOutput.TotalRevenue, actualOutput.TotalRevenue);
            Assert.Equal(expectedOutput.salesDateOutput.FirstOrderDate, actualOutput.salesDateOutput.FirstOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.LastOrderDate, actualOutput.salesDateOutput.LastOrderDate);
            Assert.Equal(expectedOutput.salesDateOutput.DaysBetween, actualOutput.salesDateOutput.DaysBetween);
            Assert.Equal(expectedOutput.MostCommonRegion, actualOutput.MostCommonRegion);
        }
    }
}
