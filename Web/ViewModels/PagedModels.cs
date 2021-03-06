﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunshine.ViewModels
{
    public class PagedModel
    {
        public string ActionName;
        public string ControllerName;
        public dynamic RouteValues;
        public int StartIndex { get { return Math.Max(1, CurrentIndex - ShowPageNum / 2); } }
        public int EndIndex { get { return Math.Min(TotalPage, Math.Max(CurrentIndex + ShowPageNum / 2, ShowPageNum)); } }
        public int CurrentIndex;
        public int ShowPageNum = 7;
        public int TotalPage;
        public PagedStyle PagedStype;
    }

    [Flags]
    public enum PagedStyle
    {
        Number = 1,
        PreNext = 2,
        BeginEnd = 4,
        All = 7
    }

    public class PagedRouteValues
    {
        public int PageIndex;
        public int PageSize;
    }

    public class SearchRouteValues : PagedRouteValues
    {
        public string Pattern;
        public string Field;
        public string Name;
    }
}