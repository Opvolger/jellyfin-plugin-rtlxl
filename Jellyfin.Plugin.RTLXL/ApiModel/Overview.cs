using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.RTLXL.ApiModel
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    internal class Overview
    {
        public Meta? meta { get; set; }

        public List<OverviewItem>? abstracts { get; set; }
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
