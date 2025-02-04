namespace PreScreenWebAPI.Models
{
    public class SalesDateOutput
    {
        public DateTime FirstOrderDate { get; set; }
        public DateTime LastOrderDate { get; set; }
        public int DaysBetween { get; set; }

    }
}
