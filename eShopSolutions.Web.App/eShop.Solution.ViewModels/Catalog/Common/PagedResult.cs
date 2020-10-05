using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.Catalog.Common
{
    public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalRecord { get; set; }
}
}
