﻿namespace IRBSapi.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? BookedBy { get; set; }
        public string? Purpose { get; set; }
        public List<Booking> LinkedBookings { get; set; } = new List<Booking>();
    }
}
