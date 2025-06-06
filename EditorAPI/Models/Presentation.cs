using System;
using System.Collections.Generic;

namespace Models;

public partial class Presentation
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string Author { get; set; } = null!;

    public virtual ICollection<Sharedpresentationslide> Sharedpresentationslides { get; set; } = new List<Sharedpresentationslide>();
}
