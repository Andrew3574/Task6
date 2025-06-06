using System;
using System.Collections.Generic;

namespace Models;

public partial class Element
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Typeid { get; set; }

    public virtual ICollection<Sharedslideelement> Sharedslideelements { get; set; } = new List<Sharedslideelement>();

    public virtual Elementtype? Type { get; set; }
}
