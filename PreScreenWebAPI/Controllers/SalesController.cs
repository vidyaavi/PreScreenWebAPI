using Microsoft.AspNetCore.Mvc;
using PreScreenWebAPI.Interfaces;
using PreScreenWebAPI.Models;
using PreScreenWebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreScreenWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ICsvHelperService _csvHelperService;
        private readonly ISalesCalculationService _salesCalculationService;
        private readonly string _salesFilePath;
        private readonly ILogger<SalesController> _logger;


        public SalesController(ICsvHelperService csvHelperService, ISalesCalculationService salesCalculationService, IConfiguration configuration, ILogger<SalesController> logger)
        {
            _csvHelperService = csvHelperService;
            _salesCalculationService = salesCalculationService;
            _salesFilePath = configuration["FileSettings:SalesFilePath"];
            _logger = logger;

        }

        [HttpGet]
        public ActionResult<SalesOutput> Get()
        {
            try
            {
                _logger.LogInformation("Fetching sales data from file: {FilePath}", _salesFilePath);

                //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "SalesRecords.csv");

                // Gets the data from CSV
                var saleRecords = _csvHelperService.CsvToObject(_salesFilePath);

                _logger.LogInformation("Found {RecordCount} records in the file.", saleRecords.Count);

                // If no records are found, return an empty output
                if (saleRecords.Count == 0)
                {
                    _logger.LogWarning("No sales records found in the file.");
                    return Ok(new SalesOutput());  // Return empty output
                }
                // Calculate the output
                SalesOutput output = _salesCalculationService.CalculateOutput(saleRecords);

                return Ok(output); // Return the calculated result
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "An error occurred while processing the sales data.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
      

    }
}
