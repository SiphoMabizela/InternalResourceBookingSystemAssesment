using System;
using System.Collections.Generic;

namespace IRBSapi.Entities.IRBSEntities;

public partial class Booking
{
    public int Id { get; set; }

    public int? ResourceId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? BookedBy { get; set; }

    public string? Purpose { get; set; }

    public virtual Resource? Resource { get; set; }
}
