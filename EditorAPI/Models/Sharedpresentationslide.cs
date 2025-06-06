using System;
using System.Collections.Generic;

namespace Models;

public partial class Sharedpresentationslide
{
    public int Id { get; set; }

    public int? Presentationid { get; set; }

    public int? Slideid { get; set; }

    public virtual Presentation? Presentation { get; set; }

    public virtual Slide? Slide { get; set; }
}
