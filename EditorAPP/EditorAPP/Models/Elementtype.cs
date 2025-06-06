using System;
using System.Collections.Generic;

namespace Models;

public partial class Elementtype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Element> Elements { get; set; } = new List<Element>();
}
