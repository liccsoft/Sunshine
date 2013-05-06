using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunshine.ViewModels
{
    public class ModuleModel
    {
        public string ModuleName { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public object RouteAttribute { get; set; }
    }
}