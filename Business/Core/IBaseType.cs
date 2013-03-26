using System;
namespace Sunshine.Business.Core
{
    interface IBaseType
    {
        string Description { get; set; }
        string DisplayName { get; set; }
        string Name { get; set; }
    }
}
