using System;
using System.Collections.Generic;

namespace Models;

public partial class Sharedslideelement
{
    public int Id { get; set; }

    public int? Slideid { get; set; }

    public int? Elementid { get; set; } = 1;

    public int? ElementX { get; set; }

    public int? ElementY { get; set; }

    public int? ElementWidth { get; set; }

    public int? ElementHeight { get; set; }

    public string? ElementContent { get; set; }

    public virtual Element? Element { get; set; }

    public virtual Slide? Slide { get; set; }
}
