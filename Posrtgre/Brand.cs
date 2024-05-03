using System;
using System.Collections.Generic;

namespace Posrtgre;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<ModelBrand> ModelBrands { get; set; } = new List<ModelBrand>();
}
