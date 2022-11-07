using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.RTLXL.ApiModel
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    internal class OverviewItem
    {
        public string? key { get; set; }

        public string? name { get; set; }

        public string? synopsis { get; set; }

        public string? station { get; set; }

        public bool? is_telekids { get; set; }

        public bool? is_sport { get; set; }

        public string? itemsurl { get; set; }

        public string? coverurl { get; set; }
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
