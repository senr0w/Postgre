using System;
using System.Collections.Generic;

namespace Posrtgre;

public partial class ModelBrand
{
    public int ModelId { get; set; }

    public int? BrandId { get; set; }

    public string? ModelName { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
