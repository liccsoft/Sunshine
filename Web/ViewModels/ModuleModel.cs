using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sunshine.ViewModels
{
    [Serializable]
    [XmlRoot("Module")]
    public class ModuleModel
    {
        [XmlAttribute()]
        public string ModuleName { get; set; }
        [XmlAttribute()]
        public string Title { get; set; }
        [XmlAttribute()]
        public bool IsActive { get; set; }
        [XmlAttribute()]
        public string ActionName { get; set; }
        [XmlAttribute()]
        public string ControllerName { get; set; }
        [XmlElement("Route")]
        public KeyValuePair<string,string>[] RouteAttribute { get; set; }
        [XmlAttribute()]
        public string AllowRole { get; set; }
    }
    [Serializable]
    [XmlRoot("ModuleManager")]
    public class ModuleManager
    {
        [XmlArray("AdminModules")]
        [XmlArrayItem("Module")]
        public List<ModuleModel> AdminModules;
        [XmlArray("UserModules")]
        [XmlArrayItem("Module")]
        public List<ModuleModel> UserModules;
    }
}