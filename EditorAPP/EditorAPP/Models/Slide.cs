using System;
using System.Collections.Generic;

namespace Models;

public partial class Slide
{
    public int Id { get; set; }

    public string? Background { get; set; }

    public virtual ICollection<Sharedpresentationslide> Sharedpresentationslides { get; set; } = new List<Sharedpresentationslide>();

    public virtual ICollection<Sharedslideelement> Sharedslideelements { get; set; } = new List<Sharedslideelement>();
}
