using System;
using System.Collections.Generic;

namespace Posrtgre;

public partial class Car
{
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public int? ModelId { get; set; }

    public int? ColorId { get; set; }

    public string? Year { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Color? Color { get; set; }

    public virtual ModelBrand? Model { get; set; }
}
