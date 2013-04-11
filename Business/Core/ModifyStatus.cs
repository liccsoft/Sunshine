using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunshine.Business.Core
{
    public enum ModifyStatus : int
    {
        None = 0,
        /// <summary>
        /// 允许更改
        /// </summary>
        Allowed,
        /// <summary>
        /// 不允许更改
        /// </summary>
        Forbidden,
        /// <summary>
        /// 更改正在处理中
        /// </summary>
        Submitted,
        /// <summary>
        /// 更改已经处理
        /// </summary>
        Processed,
    }
}
