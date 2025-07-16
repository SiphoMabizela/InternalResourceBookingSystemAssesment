using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IRBSui.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string BookedBy { get; set; }
        public string Purpose { get; set; }
    }
}