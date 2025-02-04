using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using PreScreenWebAPI.Interfaces;

namespace PreScreenWebAPI.Services
{
    public class CsvHelperService: ICsvHelperService
    {
        //gets the records from the csv file
        public virtual List<SalesRecord> CsvToObject(string filePath)
        {
            var salesRecords = new List<SalesRecord>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            }))
            {
                var records = csv.GetRecords<SalesRecord>().ToList();
                salesRecords.AddRange(records);
            }

            return salesRecords;
        }
    }
}
