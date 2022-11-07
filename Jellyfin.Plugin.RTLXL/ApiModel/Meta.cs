using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.RTLXL.ApiModel
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    internal class Meta
    {
        public string? thumb_base_url { get; set; }

        public string? poster_base_url { get; set; }

        public string? proglogo_base_url { get; set; }
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
