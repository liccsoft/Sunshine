using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunshine.Business.Core
{
    public enum EntityStatus:byte
    {
        Unknow      = 0,
        Enabled     = 1,
        Disabled    = 2,
        Deleted     = 3
    }
}
