namespace Rsp.IrasService.Domain.Models
{
    public enum Location
    {
        England, Scotland, Wales, NorthernIreland
    }
    public class IrasApplication
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public Location Location { get; set; }
        public DateTime StartDate { get; set; }
        public required List<string> ApplicationCategories { get; set; }
        public required List<string> ProjectCategories { get; set; }

    }
}
