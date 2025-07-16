namespace IRBSapi.Models
{
    public class ResourceBooking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsAvailable { get; set; }

        public bool? IsUnderMaintenance { get; set; }
    }
}
