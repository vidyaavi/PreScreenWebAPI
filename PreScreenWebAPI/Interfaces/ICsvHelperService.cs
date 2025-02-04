namespace PreScreenWebAPI.Interfaces
{
    public interface ICsvHelperService
    {
        List<SalesRecord> CsvToObject(string filePath);
    }
}
