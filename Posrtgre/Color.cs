using System;
using System.Collections.Generic;

namespace Posrtgre;

public partial class Color
{
    public int ColorId { get; set; }

    public string? ColorName { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
