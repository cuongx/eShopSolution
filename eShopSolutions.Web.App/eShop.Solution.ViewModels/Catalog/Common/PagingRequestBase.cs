﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.Catalog.Common
{
    public class PagingRequestBase
    {

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
